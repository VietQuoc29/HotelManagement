using Dapper;
using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Request;
using HotelManagerFull.Share.Response;
using HotelManagerFull.Share.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// IServicesBusiness
    /// </summary>
    public interface IServicesBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<ServicesViewModel>> GetAllAsync(PagingRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(ServicesRequest request, bool isDeleted);
    }

    /// <summary>
    /// ServicesBusiness
    /// </summary>
    public class ServicesBusiness : IServicesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;
        private readonly IUploadFileBusiness _uploadFileBusiness;
        private readonly AppSetting _appSettings;

        /// <summary>
        /// ServicesBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="dapperRepository"></param>
        /// <param name="uploadFileBusiness"></param>
        /// <param name="options"></param>
        public ServicesBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness,
            IDapperRepository dapperRepository, IUploadFileBusiness uploadFileBusiness, IOptions<AppSetting> options)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
            _dapperRepository = dapperRepository;
            _uploadFileBusiness = uploadFileBusiness;
            _appSettings = options.Value;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<ServicesViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ServicesViewModel>("uspServices_GetAll", param);

                if (list.Any())
                {
                    list = list.Select(x => new ServicesViewModel
                    {
                        STT = x.STT,
                        TotalRow = x.TotalRow,
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Unit = x.Unit,
                        Status = x.Status,
                        Note = x.Note,
                        Image = !string.IsNullOrEmpty(x.Image) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_SERVICES + Constants.Source + x.Image : _appSettings.ImageBase,
                        ServiceCategoriesName = x.ServiceCategoriesName,
                        ServiceCategoriesId = x.ServiceCategoriesId,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDate = x.ModifiedDate,
                    });
                }

                if (null == list)
                {
                    return new ResponseDataList<ServicesViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<ServicesViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.Message.Successfully,
                    Data = list,
                    TotalRecords = list.FirstOrDefault()?.TotalRow ?? 0
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> SaveAsync(ServicesRequest request, bool isDeleted)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.ServicesRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name, request.ServiceCategoriesId))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }

                        if (isDeleted)
                        {
                            update.DeletedBy = _loginBusiness.UserName;
                            update.DeletedDate = DateTime.Now;
                            update.IsDeleted = true;
                        }
                        else
                        {
                            update.Name = request.Name;
                            update.Price = request.Price;
                            update.Unit = request.Unit;
                            update.Status = request.Status;
                            update.Note = request.Note;
                            if (request.File != null)
                            {
                                update.Image = request.File != null ? request.File.FileName : string.Empty;
                            }
                            update.ServiceCategoriesId = request.ServiceCategoriesId;
                            update.ModifiedBy = _loginBusiness.UserName;
                            update.ModifiedDate = DateTime.Now;
                        }
                        _unitOfWork.ServicesRepository.Update(update);
                        result = _unitOfWork.ServicesRepository.Commit();
                    }
                    else
                    {
                        if (isDeleted && request.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.IdNotFound,
                                Data = false
                            };
                        }

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name, request.ServiceCategoriesId))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }
                        var model = new Services
                        {
                            Name = request.Name,
                            Price = request.Price,
                            Unit = request.Unit,
                            Status = request.Status,
                            Note = request.Note,
                            Image = request.File != null ? request.File.FileName : string.Empty,
                            ServiceCategoriesId = request.ServiceCategoriesId,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                        _unitOfWork.ServicesRepository.Add(model);
                        result = _unitOfWork.ServicesRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {
                        if (!isDeleted && request.File != null)
                        {
                            await _uploadFileBusiness.UploadSingleFileAsync(request.File, Constants.FileType.SERVICES);
                        }
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                            ResponseMessage = isDeleted ? Constants.Message.DeleteSuccess : Constants.Message.SaveSuccess,
                            Data = true
                        };
                    }
                    else
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                            ResponseMessage = isDeleted ? Constants.Message.DeleteFail : Constants.Message.SaveFail,
                            Data = true
                        };
                    }
                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// CheckDuplicate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="serviceCategoriesId"></param>
        /// <returns></returns>
        private async Task<bool> CheckDuplicate(long id, string name, long? serviceCategoriesId)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.ServicesRepository.GetSingleAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower() && x.ServiceCategoriesId == serviceCategoriesId && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.ServicesRepository.GetSingleAsync(x => x.Name.ToLower() == name.ToLower() && x.ServiceCategoriesId == serviceCategoriesId && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

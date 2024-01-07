using Dapper;
using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Request;
using HotelManagerFull.Share.Response;
using HotelManagerFull.Share.ViewModel;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// IServiceCategoriesBusiness
    /// </summary>
    public interface IServiceCategoriesBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<ServiceCategoriesViewModel>> GetAllAsync(PagingRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(ServiceCategoriesRequest request, bool isDeleted);

        /// <summary>
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        ResponseDataList<ServiceCategories> Suggestion();

        /// <summary>
        /// GetAllSerrviceByServiceCategoriesAsync
        /// </summary>
        /// <returns></returns>
        ResponseDataList<ServicesInfoViewModel> GetAllSerrviceByServiceCategoriesAsync();
    }

    /// <summary>
    /// ServiceCategoriesBusiness
    /// </summary>
    public class ServiceCategoriesBusiness : IServiceCategoriesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;
        private readonly AppSetting _appSettings;

        /// <summary>
        /// ServiceCategoriesBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="dapperRepository"></param>
        /// <param name="options"></param>
        public ServiceCategoriesBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness, IOptions<AppSetting> options,
            IDapperRepository dapperRepository)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
            _dapperRepository = dapperRepository;
            _appSettings = options.Value;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<ServiceCategoriesViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ServiceCategoriesViewModel>("uspServiceCategories_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<ServiceCategoriesViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<ServiceCategoriesViewModel>
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
        /// GetAllSerrviceByServiceCategoriesAsync
        /// </summary>
        /// <returns></returns>
        public ResponseDataList<ServicesInfoViewModel> GetAllSerrviceByServiceCategoriesAsync()
        {
            try
            {
                var listService = _unitOfWork.ServicesRepository.GetAll().Where(d => d.Status && !d.IsDeleted);

                var listServiceCategories = _unitOfWork.ServiceCategoriesRepository.GetAll().Where(x => !x.IsDeleted).Select(d => new ServicesInfoViewModel
                {
                    Id = d.Id,
                    Name = d.Name,
                    ListService = listService != null ? listService.Where(w => w.ServiceCategoriesId == d.Id).Select(e =>
                    new Services
                    {
                        Id = e.Id,
                        Name = e.Name,
                        Price = e.Price,
                        Unit = e.Unit,
                        Status = e.Status,
                        Note = e.Note,
                        ServiceCategoriesId = e.ServiceCategoriesId,
                        Image = !string.IsNullOrEmpty(e.Image) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_SERVICES + Constants.Source + e.Image : _appSettings.ImageBase
                    }).ToList() : new List<Services>()
                });

                if (null == listServiceCategories)
                {
                    return new ResponseDataList<ServicesInfoViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<ServicesInfoViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.Message.Successfully,
                    Data = listServiceCategories,
                    TotalRecords = listServiceCategories.Count()
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
        public async Task<ResponseData<bool>> SaveAsync(ServiceCategoriesRequest request, bool isDeleted)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.ServiceCategoriesRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name))
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
                            update.ModifiedBy = _loginBusiness.UserName;
                            update.ModifiedDate = DateTime.Now;
                        }
                        _unitOfWork.ServiceCategoriesRepository.Update(update);
                        result = _unitOfWork.ServiceCategoriesRepository.Commit();
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

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }
                        var model = new ServiceCategories
                        {
                            Name = request.Name,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                        _unitOfWork.ServiceCategoriesRepository.Add(model);
                        result = _unitOfWork.ServiceCategoriesRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {
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
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        public ResponseDataList<ServiceCategories> Suggestion()
        {
            try
            {
                var list = _unitOfWork.ServiceCategoriesRepository.GetAll().Where(x => !x.IsDeleted);

                if (null == list)
                {
                    return new ResponseDataList<ServiceCategories>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<ServiceCategories>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.Message.Successfully,
                    Data = list,
                    TotalRecords = list.Count()
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// CheckDuplicate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<bool> CheckDuplicate(long id, string name)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.ServiceCategoriesRepository.GetSingleAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.ServiceCategoriesRepository.GetSingleAsync(x => x.Name.ToLower() == name.ToLower() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

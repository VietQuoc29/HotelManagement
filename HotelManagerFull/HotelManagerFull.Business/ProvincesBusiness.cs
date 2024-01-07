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
    /// IProvincesBusiness
    /// </summary>
    public interface IProvincesBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<ProvincesViewModel>> GetAllAsync(PagingRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(ProvincesRequest request, bool isDeleted);

        /// <summary>
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        ResponseDataList<Provinces> Suggestion();

        /// <summary>
        /// GetAllProvinces
        /// </summary>
        /// <returns></returns>
        ResponseDataList<Provinces> GetAllProvinces();
    }

    /// <summary>
    /// ProvincesBusiness
    /// </summary>
    public class ProvincesBusiness : IProvincesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;
        private readonly IUploadFileBusiness _uploadFileBusiness;
        private readonly AppSetting _appSettings;

        /// <summary>
        /// ProvincesBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="uploadFileBusiness"></param>
        /// <param name="dapperRepository"></param>
        /// <param name="options"></param>
        public ProvincesBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness,
            IUploadFileBusiness uploadFileBusiness, IDapperRepository dapperRepository,
            IOptions<AppSetting> options)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
            _uploadFileBusiness = uploadFileBusiness;
            _dapperRepository = dapperRepository;
            _appSettings = options.Value;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<ProvincesViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ProvincesViewModel>("uspProvinces_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<ProvincesViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                if (list.Any())
                {
                    list = list.Select(x => new ProvincesViewModel
                    {
                        STT = x.STT,
                        TotalRow = x.TotalRow,
                        Id = x.Id,
                        Name = x.Name,
                        ImageLink = !string.IsNullOrEmpty(x.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_PROVINCES + Constants.Source + x.ImageLink : _appSettings.ImageBase,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDate = x.ModifiedDate,
                    });
                }

                return new ResponseDataList<ProvincesViewModel>
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
        public async Task<ResponseData<bool>> SaveAsync(ProvincesRequest request, bool isDeleted)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.ProvincesRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
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
                            if (request.File != null)
                            {
                                update.ImageLink = request.File != null ? request.File.FileName : string.Empty;
                            }
                            update.ModifiedBy = _loginBusiness.UserName;
                            update.ModifiedDate = DateTime.Now;
                        }
                        _unitOfWork.ProvincesRepository.Update(update);
                        result = _unitOfWork.ProvincesRepository.Commit();
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
                        var model = new Provinces
                        {
                            Name = request.Name,
                            ImageLink = request.File != null ? request.File.FileName : string.Empty,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                        _unitOfWork.ProvincesRepository.Add(model);
                        result = _unitOfWork.ProvincesRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {
                        if (!isDeleted && request.File != null)
                        {
                            await _uploadFileBusiness.UploadSingleFileAsync(request.File, Constants.FileType.PROVINCES);
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
        /// <returns></returns>
        private async Task<bool> CheckDuplicate(long id, string name)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.ProvincesRepository.GetSingleAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.ProvincesRepository.GetSingleAsync(x => x.Name.ToLower() == name.ToLower() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        public ResponseDataList<Provinces> Suggestion()
        {
            try
            {
                var list = _unitOfWork.ProvincesRepository.GetAll().Where(x => !x.IsDeleted);

                if (null == list)
                {
                    return new ResponseDataList<Provinces>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<Provinces>
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
        /// GetAllProvinces
        /// </summary>
        /// <returns></returns>
        public ResponseDataList<Provinces> GetAllProvinces()
        {
            try
            {
                var list = _unitOfWork.ProvincesRepository.GetAll().Where(x => !x.IsDeleted).Select(d => new Provinces
                {
                    Id = d.Id,
                    Name = d.Name,
                    ImageLink = !string.IsNullOrEmpty(d.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_PROVINCES + Constants.Source + d.ImageLink : _appSettings.ImageBase
                }).OrderBy(x => x.Name);

                if (null == list)
                {
                    return new ResponseDataList<Provinces>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<Provinces>
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
    }
}

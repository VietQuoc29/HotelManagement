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
    /// IHotelsBusiness
    /// </summary>
    public interface IHotelsBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<HotelsViewModel>> GetAllAsync(PagingRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(HotelsRequest request, bool isDeleted);

        /// <summary>
        /// GetAllHotelByProvinces
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        ResponseData<HotelByProvincesViewModel> GetAllHotelByProvinces(long provinceId);
    }

    /// <summary>
    /// HotelsBusiness
    /// </summary>
    public class HotelsBusiness : IHotelsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;
        private readonly IUploadFileBusiness _uploadFileBusiness;
        private readonly IProvincesBusiness _provincesBusiness;
        private readonly AppSetting _appSettings;

        /// <summary>
        /// HotelsBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="dapperRepository"></param>
        /// <param name="uploadFileBusiness"></param>
        /// <param name="provincesBusiness"></param>
        /// <param name="options"></param>
        public HotelsBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness,
            IDapperRepository dapperRepository, IUploadFileBusiness uploadFileBusiness,
            IProvincesBusiness provincesBusiness, IOptions<AppSetting> options)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
            _dapperRepository = dapperRepository;
            _uploadFileBusiness = uploadFileBusiness;
            _provincesBusiness = provincesBusiness;
            _appSettings = options.Value;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<HotelsViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<HotelsViewModel>("uspHotels_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<HotelsViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                if (list.Any())
                {
                    list = list.Select(x => new HotelsViewModel
                    {
                        STT = x.STT,
                        TotalRow = x.TotalRow,
                        Id = x.Id,
                        Name = x.Name,
                        Image = !string.IsNullOrEmpty(x.Image) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_HOTELS + Constants.Source + x.Image : _appSettings.ImageBase,
                        Address = x.Address,
                        Title = x.Title,
                        Introduce = x.Introduce,
                        Star = x.Star,
                        Note = x.Note,
                        ProvinceId = x.ProvinceId,
                        ProvinceName = x.ProvinceName,
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDate = x.ModifiedDate,
                    });
                }

                return new ResponseDataList<HotelsViewModel>
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
        public async Task<ResponseData<bool>> SaveAsync(HotelsRequest request, bool isDeleted)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    var model = new Hotels();
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.HotelsRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name, request.Address))
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
                            update.Address = request.Address;
                            update.Title = request.Title;
                            update.Introduce = request.Introduce;
                            update.Star = request.Star;
                            update.Note = request.Note;
                            update.ProvinceId = request.ProvinceId;
                            update.ModifiedBy = _loginBusiness.UserName;
                            update.ModifiedDate = DateTime.Now;
                            if (request.File != null)
                            {
                                update.Image = request.File != null ? request.File.FileName : string.Empty;
                            }
                        }
                        _unitOfWork.HotelsRepository.Update(update);
                        result = _unitOfWork.HotelsRepository.Commit();
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

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name, request.Address))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }
                        model = new Hotels
                        {
                            Name = request.Name,
                            Image = request.File != null ? request.File.FileName : string.Empty,
                            Address = request.Address,
                            Title = request.Title,
                            Introduce = request.Introduce,
                            Star = request.Star,
                            Note = request.Note,
                            ProvinceId = request.ProvinceId,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                        _unitOfWork.HotelsRepository.Add(model);
                        result = _unitOfWork.HotelsRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {
                        if (!isDeleted && request.File != null)
                        {
                            await _uploadFileBusiness.UploadSingleFileAsync(request.File, Constants.FileType.HOTELS);
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
        /// <param name="provinceId"></param>
        /// <returns></returns>
        private async Task<bool> CheckDuplicate(long id, string name, string address)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.HotelsRepository.GetSingleAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower() && x.Address.ToLower().Trim() == address.ToLower().Trim() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.HotelsRepository.GetSingleAsync(x => x.Name.ToLower() == name.ToLower() && x.Address.ToLower().Trim() == address.ToLower().Trim() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// GetAllHotelByProvinces
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public ResponseData<HotelByProvincesViewModel> GetAllHotelByProvinces(long provinceId)
        {
            try
            {
                if (provinceId <= 0)
                {
                    return new ResponseData<HotelByProvincesViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.IdNotFound,
                        Data = null
                    };
                }

                var dataProvinces = _unitOfWork.ProvincesRepository.GetSingle(x => x.Id == provinceId && !x.IsDeleted);
                var listHotel = _unitOfWork.HotelsRepository.GetAll().Where(x => x.ProvinceId == provinceId && !x.IsDeleted).Select(d => new Hotels
                {
                    Id = d.Id,
                    Name = d.Name,
                    Image = !string.IsNullOrEmpty(d.Image) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_HOTELS + Constants.Source + d.Image : _appSettings.ImageBase,
                    Address = d.Address,
                    Title = d.Title
                });

                var listProvinces = _provincesBusiness.GetAllProvinces().Data;

                var data = new HotelByProvincesViewModel
                {
                    ProvinceName = dataProvinces?.Name ?? string.Empty,
                    ListHotel = listHotel.ToList()
                };

                return new ResponseData<HotelByProvincesViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.Message.Successfully,
                    Data = data
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

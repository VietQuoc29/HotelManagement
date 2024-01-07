using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Request;
using HotelManagerFull.Share.Response;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// IHotelImagesBusiness
    /// </summary>
    public interface IHotelImagesBusiness
    {
        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(List<HotelImages> request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        Task<ResponseData<bool>> DeleteAsync(long id);

        /// <summary>
        /// GetAllHotelImage
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        ResponseDataList<HotelImages> GetAllHotelImage(long? roomId);

        /// <summary>
        /// UploadImageAsync
        /// </summary>
        /// <returns></returns>
        Task<ResponseData<bool>> UploadImageAsync(HotelImageRequest request);

        /// <summary>
        /// GetAllGallery
        /// </summary>
        /// <returns></returns>
        ResponseDataList<HotelImages> GetAllGallery();
    }

    /// <summary>
    /// HotelImagesBusiness
    /// </summary>
    public class HotelImagesBusiness : IHotelImagesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSetting _appSettings;
        private readonly IUploadFileBusiness _uploadFileBusiness;

        /// <summary>
        /// HotelImagesBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="options"></param>
        /// <param name="uploadFileBusiness"></param>
        public HotelImagesBusiness(IUnitOfWork unitOfWork, IUploadFileBusiness uploadFileBusiness, IOptions<AppSetting> options)
        {
            _unitOfWork = unitOfWork;
            _appSettings = options.Value;
            _uploadFileBusiness = uploadFileBusiness;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> DeleteAsync(long id)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var delete = await _unitOfWork.HotelImagesRepository.GetSingleAsync(x => x.Id == id && !x.IsDeleted);
                    if (delete == null)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                            ResponseMessage = Constants.Message.RecordNotFoundMessage,
                            Data = false
                        };
                    }

                    delete.IsDeleted = true;

                    _unitOfWork.HotelImagesRepository.Update(delete);
                    var result = _unitOfWork.HotelImagesRepository.Commit();

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {

                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                            ResponseMessage = Constants.Message.DeleteSuccess,
                            Data = true
                        };
                    }
                    else
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                            ResponseMessage = Constants.Message.DeleteFail,
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
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData<bool>> SaveAsync(List<HotelImages> request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (request.Count > 0)
                    {
                        _unitOfWork.HotelImagesRepository.BulkInsert(request);
                        _unitOfWork.HotelImagesRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    return new ResponseData<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.Message.SaveSuccess,
                        Data = true
                    };
                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// GetAllHotelImage
        /// </summary>
        /// <returns></returns>
        public ResponseDataList<HotelImages> GetAllHotelImage(long? roomId)
        {
            try
            {
                var list = _unitOfWork.HotelImagesRepository.GetAll()
                    .Where(x => x.RoomId == roomId && !x.IsDeleted)
                    .Select(x => new HotelImages
                    {
                        Id = x.Id,
                        ImageLink = !string.IsNullOrEmpty(x.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_ROOMS + Constants.Source + x.ImageLink : _appSettings.ImageBase,
                        RoomId = x.RoomId
                    });

                if (null == list)
                {
                    return new ResponseDataList<HotelImages>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<HotelImages>
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
        /// UploadImageAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> UploadImageAsync(HotelImageRequest request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (request.ListFile != null)
                    {
                        var listHotelImage = request.ListFile.ToList().Select(x => new HotelImages
                        {
                            ImageLink = x.FileName,
                            RoomId = request.RoomId,
                            IsDeleted = false
                        }).ToList();

                        if (listHotelImage.Count() > 0)
                        {
                            _unitOfWork.HotelImagesRepository.BulkInsert(listHotelImage);
                            _unitOfWork.HotelImagesRepository.Commit();
                            await _uploadFileBusiness.UploadMultipleFileAsync(request.ListFile, Constants.FileType.ROOMS);
                        }
                    }

                    _unitOfWork.CommitTransaction();
                    return new ResponseData<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.Message.SaveSuccess,
                        Data = true
                    };
                }
                catch (Exception)
                {
                    _unitOfWork.RollbackTransaction();
                    throw;
                }
            }
        }

        /// <summary>
        /// GetAllGallery
        /// </summary>
        /// <returns></returns>
        public ResponseDataList<HotelImages> GetAllGallery()
        {
            try
            {
                var list = _unitOfWork.HotelImagesRepository.GetAll()
                    .Select(x => new HotelImages
                    {
                        Id = x.Id,
                        ImageLink = !string.IsNullOrEmpty(x.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_ROOMS + Constants.Source + x.ImageLink : _appSettings.ImageBase
                    });

                if (null == list)
                {
                    return new ResponseDataList<HotelImages>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<HotelImages>
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

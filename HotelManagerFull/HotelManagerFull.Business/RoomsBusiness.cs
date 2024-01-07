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
    /// IRoomsBusiness
    /// </summary>
    public interface IRoomsBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<RoomsViewModel>> GetAllAsync(RoomSearchRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(RoomsRequest request, bool isDeleted);

        /// <summary>
        /// GetInfoRoomAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<RoomInfoViewModel>> GetInfoRoomAsync(RoomInfoRequest request);

        /// <summary>
        /// UpdateRoomStatusAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> UpdateRoomStatusAsync(RoomsRequest request);

        /// <summary>
        /// ReturnRoomAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ResponseDataList<ReturnRoomViewModel>> GetInfoReturnRoomAsync(long id, bool isPayment, long orderRoomId);

        /// <summary>
        /// PaymentRoomAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> PaymentRoomAsync(PaymentRoomRequest request);

        /// <summary>
        /// GetAllRoomByHotel
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        Task<ResponseData<RoomByHotelViewModel>> GetAllRoomByHotel(RoomByHotelRequest request);

        /// <summary>
        /// GetAllRoomDetail
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<ResponseData<RoomDetailViewModel>> GetAllRoomDetail(long roomId);
    }

    /// <summary>
    /// RoomsBusiness
    /// </summary>
    public class RoomsBusiness : IRoomsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;
        private readonly IUploadFileBusiness _uploadFileBusiness;
        private readonly AppSetting _appSettings;
        private readonly IHotelImagesBusiness _hotelImagesBusiness;
        private readonly IRoomCategoriesBusiness _roomCategoriesBusiness;
        private readonly IRoomStatusBusiness _roomStatusBusiness;

        /// <summary>
        /// RoomsBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="roomCategoriesBusiness"></param>
        /// <param name="dapperRepository"></param>
        /// <param name="uploadFileBusiness"></param>
        /// <param name="roomStatusBusiness"></param>
        /// <param name="options"></param>
        /// <param name="hotelImagesBusiness"></param>
        public RoomsBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness, IRoomCategoriesBusiness roomCategoriesBusiness,
            IDapperRepository dapperRepository, IUploadFileBusiness uploadFileBusiness, IRoomStatusBusiness roomStatusBusiness,
            IOptions<AppSetting> options, IHotelImagesBusiness hotelImagesBusiness)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
            _dapperRepository = dapperRepository;
            _appSettings = options.Value;
            _hotelImagesBusiness = hotelImagesBusiness;
            _uploadFileBusiness = uploadFileBusiness;
            _roomCategoriesBusiness = roomCategoriesBusiness;
            _roomStatusBusiness = roomStatusBusiness;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<RoomsViewModel>> GetAllAsync(RoomSearchRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add("@HotelId", request.HotelId);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<RoomsViewModel>("uspRooms_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<RoomsViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                if (list.Any())
                {
                    var listHotelImage = _unitOfWork.HotelImagesRepository.GetAll().Where(d => !d.IsDeleted).Select(x => new HotelImages { Id = x.Id, ImageLink = !string.IsNullOrEmpty(x.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_ROOMS + Constants.Source + x.ImageLink : _appSettings.ImageBase, RoomId = x.RoomId });

                    list = list.Select(x => new RoomsViewModel
                    {
                        STT = x.STT,
                        TotalRow = x.TotalRow,
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        PromotionalPrice = x.PromotionalPrice,
                        Star = x.Star,
                        IsActive = x.IsActive,
                        RoomStatusId = x.RoomStatusId,
                        RoomStatusName = x.RoomStatusName,
                        RoomCategoriesId = x.RoomCategoriesId,
                        RoomCategoriesName = x.RoomCategoriesName,
                        HotelId = x.HotelId,
                        HotelName = x.HotelName,
                        FloorId = x.FloorId,
                        FloorName = x.FloorName,
                        ListHotelImages = listHotelImage != null ? listHotelImage.Where(d => d.RoomId == x.Id).ToList() : new List<HotelImages>(),
                        CreatedBy = x.CreatedBy,
                        CreatedDate = x.CreatedDate,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedDate = x.ModifiedDate,
                    });
                }

                return new ResponseDataList<RoomsViewModel>
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
        /// GetInfoRoomAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<RoomInfoViewModel>> GetInfoRoomAsync(RoomInfoRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@HotelId", request.HotelId);
                param.Add("@RoomStatusId", request.RoomStatusId);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<RoomInfoViewModel>("uspRooms_GetInfo", param);

                if (null == list)
                {
                    return new ResponseDataList<RoomInfoViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                if (list.Any())
                {
                    var query = "SELECT Id, ImageLink, RoomId FROM HotelImages WHERE IsDeleted = 0 ORDER BY Id DESC";
                    var listHotelImage = _dapperRepository.QueryMultiple<HotelImages>(query);

                    list = list.Select(x => new RoomInfoViewModel
                    {
                        TotalRow = x.TotalRow,
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        PromotionalPrice = x.PromotionalPrice,
                        Star = x.Star,
                        IsActive = x.IsActive,
                        RoomStatusId = x.RoomStatusId,
                        RoomStatusName = x.RoomStatusName,
                        RoomCategoriesId = x.RoomCategoriesId,
                        RoomCategoriesName = x.RoomCategoriesName,
                        HotelId = x.HotelId,
                        HotelName = x.HotelName,
                        FloorId = x.FloorId,
                        FloorName = x.FloorName,
                        Image = !string.IsNullOrEmpty(listHotelImage.FirstOrDefault(d => d.RoomId == x.Id)?.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_ROOMS + Constants.Source + listHotelImage.FirstOrDefault(d => d.RoomId == x.Id)?.ImageLink : _appSettings.ImageBase,
                        OrderRoomId = x.OrderRoomId,
                        StartTime = x.StartTime,
                        FullName = x.FullName,
                        IDCard = x.IDCard,
                        PhoneNumber = x.PhoneNumber
                    });

                }

                return new ResponseDataList<RoomInfoViewModel>
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
        /// GetInfoReturnRoomAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<ReturnRoomViewModel>> GetInfoReturnRoomAsync(long id, bool isPayment, long orderRoomId)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add("@Id", id);
                param.Add("@IsPayment", isPayment);
                param.Add("@OrderRoomId", orderRoomId);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<ReturnRoomViewModel>("uspRooms_Return", param);

                if (null == list)
                {
                    return new ResponseDataList<ReturnRoomViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                if (list.Any())
                {
                    var data = list.FirstOrDefault();

                    var isPromotional = await _unitOfWork.RegisterRoomsRepository.GetSingleAsync(x => x.PhoneNumber == data.PhoneNumber && !x.Status) != null;
                    var price = isPromotional ? (data.PromotionalPrice ?? 0) : (data.Price ?? 0);
                    var listTransactions = new List<TransactionsViewModel> {
                        new TransactionsViewModel {
                            ServiceName = "Phòng",
                            Price = price,
                            Quantity = data.TotalHour + ":" + data.TotalMinutes + " phút",
                            Unit = "giờ",
                            IntoMoney = price * (data.TotalHour + data.TotalMinutes/(decimal)60)
                        }
                    };

                    var query = $"SELECT t.Quantity, s.Price, s.Unit, s.[Name] AS ServiceName, s.Price * t.Quantity AS IntoMoney FROM Transactions AS t LEFT JOIN Services AS s ON s.Id = t.ServiceId WHERE OrderRoomId = { data.OrderRoomId } ORDER BY t.Id ASC";
                    var listDataTransactions = _dapperRepository.QueryMultiple<TransactionsViewModel>(query);

                    if (listDataTransactions.Count() > 0)
                    {
                        listTransactions.AddRange(listDataTransactions);
                    }

                    list = list.Select(x => new ReturnRoomViewModel
                    {
                        Id = x.Id,
                        ProvinceName = x.ProvinceName,
                        HotelName = x.HotelName,
                        Name = x.Name,
                        FloorName = x.FloorName,
                        FullName = x.FullName,
                        PhoneNumber = x.PhoneNumber,
                        IDCard = x.IDCard,
                        Price = x.Price,
                        PromotionalPrice = x.PromotionalPrice,
                        OrderRoomId = x.OrderRoomId,
                        StartTime = x.StartTime,
                        EndTime = x.EndTime,
                        TotalHour = x.TotalHour,
                        TotalMinutes = x.TotalMinutes,
                        CreatedBy = x.CreatedBy,
                        ListTransactions = listTransactions,
                        Status = x.Status,
                        TotalPaymentTemp = listTransactions.Sum(item => item.IntoMoney)
                    });
                }

                return new ResponseDataList<ReturnRoomViewModel>
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
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> SaveAsync(RoomsRequest request, bool isDeleted)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    var model = new Rooms();
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.RoomsRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name, request.HotelId, request.FloorId))
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
                            update.PromotionalPrice = request.PromotionalPrice;
                            update.Star = request.Star;
                            update.IsActive = request.IsActive;
                            update.RoomStatusId = request.RoomStatusId;
                            update.RoomCategoriesId = request.RoomCategoriesId;
                            update.HotelId = request.HotelId;
                            update.FloorId = request.FloorId;
                            update.ModifiedBy = _loginBusiness.UserName;
                            update.ModifiedDate = DateTime.Now;
                        }
                        _unitOfWork.RoomsRepository.Update(update);
                        result = _unitOfWork.RoomsRepository.Commit();
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

                        if (!isDeleted && await CheckDuplicate(request.Id, request.Name, request.HotelId, request.FloorId))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }
                        model = new Rooms
                        {
                            Name = request.Name,
                            Price = request.Price,
                            PromotionalPrice = request.PromotionalPrice,
                            Star = request.Star,
                            IsActive = request.IsActive,
                            RoomStatusId = request.RoomStatusId,
                            RoomCategoriesId = request.RoomCategoriesId,
                            HotelId = request.HotelId,
                            FloorId = request.FloorId,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                        _unitOfWork.RoomsRepository.Add(model);
                        result = _unitOfWork.RoomsRepository.Commit();

                        if (!isDeleted && result && request.ListFile != null)
                        {
                            var listHotelImage = request.ListFile.ToList().Select(x => new HotelImages
                            {
                                ImageLink = x.FileName,
                                RoomId = model.Id,
                                IsDeleted = false
                            }).ToList();
                            var resultSaveImage = await _hotelImagesBusiness.SaveAsync(listHotelImage);
                            if (resultSaveImage.Data)
                            {
                                await _uploadFileBusiness.UploadMultipleFileAsync(request.ListFile, Constants.FileType.ROOMS);
                            }
                        }
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
        /// UpdateRoomStatusAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> UpdateRoomStatusAsync(RoomsRequest request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.RoomsRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        update.RoomStatusId = request.RoomStatusId;
                        update.ModifiedBy = _loginBusiness.UserName;
                        update.ModifiedDate = DateTime.Now;

                        _unitOfWork.RoomsRepository.Update(update);
                        result = _unitOfWork.RoomsRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                            ResponseMessage = "Thay đổi trạng thái thành công",
                            Data = true
                        };
                    }
                    else
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                            ResponseMessage = Constants.Message.SaveFail,
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
        private async Task<bool> CheckDuplicate(long id, string name, long? hotelId, long? floorId)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.RoomsRepository.GetSingleAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower() && x.HotelId == hotelId && x.FloorId == floorId && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.RoomsRepository.GetSingleAsync(x => x.Name.ToLower() == name.ToLower() && x.HotelId == hotelId && x.FloorId == floorId && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// PaymentRoomAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> PaymentRoomAsync(PaymentRoomRequest request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    var updateOrderRoom = await _unitOfWork.OrderRoomRepository.GetSingleAsync(x => x.Id == request.OrderRoomId);
                    if (updateOrderRoom == null || updateOrderRoom.Id <= 0)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                            ResponseMessage = Constants.Message.RecordNotFoundMessage,
                            Data = false
                        };
                    }

                    updateOrderRoom.EndTime = request.EndTime;
                    updateOrderRoom.TotalPayment = request.TotalPayment;
                    updateOrderRoom.Status = true;
                    updateOrderRoom.ModifiedBy = _loginBusiness.UserName;
                    updateOrderRoom.ModifiedDate = DateTime.Now;

                    _unitOfWork.OrderRoomRepository.Update(updateOrderRoom);
                    result = _unitOfWork.OrderRoomRepository.Commit();

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {
                        var updateRoom = await _unitOfWork.RoomsRepository.GetSingleAsync(x => x.Id == updateOrderRoom.RoomId && !x.IsDeleted);
                        if (updateRoom == null || updateRoom.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        updateRoom.RoomStatusId = 3;
                        updateRoom.ModifiedBy = _loginBusiness.UserName;
                        updateRoom.ModifiedDate = DateTime.Now;

                        _unitOfWork.RoomsRepository.Update(updateRoom);
                        var resultRoom = _unitOfWork.RoomsRepository.Commit();
                        if (resultRoom)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                                ResponseMessage = Constants.Message.PaymentSuccess,
                                Data = true
                            };
                        }
                    }
                    return new ResponseData<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                        ResponseMessage = Constants.Message.PaymentFail,
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
        /// GetAllRoomByHotel
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<RoomByHotelViewModel>> GetAllRoomByHotel(RoomByHotelRequest request)
        {
            try
            {
                if (request.HotelId <= 0)
                {
                    return new ResponseData<RoomByHotelViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.IdNotFound,
                        Data = null
                    };
                }

                var dataHotel = _unitOfWork.HotelsRepository.GetSingle(x => x.Id == request.HotelId && !x.IsDeleted);

                var param = new DynamicParameters();
                param.Add("@HotelId", request.HotelId);
                param.Add("@RoomStatusId", request.RoomStatusId);
                param.Add("@RoomCategoriesId", request.RoomCategoriesId);
                param.Add("@Star", request.Star);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var listRoom = await _dapperRepository.QueryMultipleUsingStoreProcAsync<RoomClientViewModel>("uspRooms_GetAllRoomByHotel", param);

                var query = "SELECT Id, ImageLink, RoomId FROM HotelImages WHERE IsDeleted = 0 ORDER BY Id DESC";
                var listHotelImage = _dapperRepository.QueryMultiple<HotelImages>(query);

                listRoom = listRoom.Select(x => new RoomClientViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    PromotionalPrice = x.PromotionalPrice,
                    Star = x.Star,
                    IsActive = x.IsActive,
                    RoomStatusId = x.RoomStatusId,
                    RoomStatusName = x.RoomStatusName,
                    RoomCategoriesId = x.RoomCategoriesId,
                    RoomCategoriesName = x.RoomCategoriesName,
                    HotelId = x.HotelId,
                    HotelName = x.HotelName,
                    FloorId = x.FloorId,
                    FloorName = x.FloorName,
                    TotalRow = x.TotalRow,
                    Image = !string.IsNullOrEmpty(listHotelImage.FirstOrDefault(d => d.RoomId == x.Id)?.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_ROOMS + Constants.Source + listHotelImage.FirstOrDefault(d => d.RoomId == x.Id)?.ImageLink : _appSettings.ImageBase,
                });

                var listRoomStatus = new List<RoomStatus> { new RoomStatus { Name = "Tất cả" } };
                listRoomStatus.AddRange(_roomStatusBusiness.Suggestion().Data);

                var listRoomCategories = new List<RoomCategories> { new RoomCategories { Name = "Tất cả" } };
                listRoomCategories.AddRange(_roomCategoriesBusiness.Suggestion().Data);

                var data = new RoomByHotelViewModel
                {
                    HotelName = dataHotel?.Name ?? string.Empty,
                    ListRoom = listRoom.ToList(),
                    ListRoomCategories = listRoomCategories.ToList(),
                    ListRoomStatus = listRoomStatus.ToList(),
                    TotalRecords = listRoom.FirstOrDefault()?.TotalRow ?? 0
                };

                return new ResponseData<RoomByHotelViewModel>
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

        /// <summary>
        /// GetAllRoomDetail
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        public async Task<ResponseData<RoomDetailViewModel>> GetAllRoomDetail(long roomId)
        {
            try
            {
                if (roomId <= 0)
                {
                    return new ResponseData<RoomDetailViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.IdNotFound,
                        Data = null
                    };
                }

                var param = new DynamicParameters();
                param.Add("@RoomId", roomId);
                var listRoom = await _dapperRepository.QueryMultipleUsingStoreProcAsync<RoomDetailViewModel>("uspRooms_GetAllRoomDetail", param);

                var query = $"SELECT ImageLink FROM HotelImages WHERE RoomId = {roomId} AND IsDeleted = 0 ORDER BY Id DESC";
                var listHotelImage = _dapperRepository.QueryMultiple<HotelImages>(query);

                var room = listRoom.FirstOrDefault();
                room.ListHotelImage = listHotelImage.Select(x => new HotelImages
                {
                    ImageLink = !string.IsNullOrEmpty(x.ImageLink) ? _appSettings.DomainFile + Constants.UploadUrl.UPLOAD_ROOMS + Constants.Source + x.ImageLink : _appSettings.ImageBase
                }).ToList();

                return new ResponseData<RoomDetailViewModel>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.Message.Successfully,
                    Data = room
                };
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

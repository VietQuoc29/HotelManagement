using Dapper;
using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Request;
using HotelManagerFull.Share.Response;
using HotelManagerFull.Share.ViewModel;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// IOrderRoomBusiness
    /// </summary>
    public interface IOrderRoomBusiness
    {
        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(OrderRoom request);

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<OrderRoomViewModel>> GetAllAsync(PagingRequest request);
    }

    /// <summary>
    /// OrderRoomBusiness
    /// </summary>
    public class OrderRoomBusiness : IOrderRoomBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginBusiness _loginBusiness;
        private readonly IDapperRepository _dapperRepository;

        /// <summary>
        /// OrderRoomBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="dapperRepository"></param>
        public OrderRoomBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness, IDapperRepository dapperRepository)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
            _dapperRepository = dapperRepository;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<OrderRoomViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<OrderRoomViewModel>("uspOrderRoom_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<OrderRoomViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<OrderRoomViewModel>
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
        public async Task<ResponseData<bool>> SaveAsync(OrderRoom request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.RoomId > 0)
                    {
                        var updateRoom = await _unitOfWork.RoomsRepository.GetSingleAsync(x => x.Id == request.RoomId && !x.IsDeleted);
                        if (updateRoom == null || updateRoom.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        updateRoom.RoomStatusId = 2;
                        updateRoom.ModifiedBy = _loginBusiness.UserName;
                        updateRoom.ModifiedDate = DateTime.Now;

                        _unitOfWork.RoomsRepository.Update(updateRoom);
                        var resultRoom = _unitOfWork.RoomsRepository.Commit();

                        if (resultRoom)
                        {

                            if (request.Id > 0)
                            {
                                var update = await _unitOfWork.OrderRoomRepository.GetSingleAsync(x => x.Id == request.Id && !x.Status);
                                if (update == null || update.Id <= 0)
                                {
                                    return new ResponseData<bool>
                                    {
                                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                        Data = false
                                    };
                                }

                                update.Status = true;
                                update.ModifiedBy = _loginBusiness.UserName;
                                update.ModifiedDate = DateTime.Now;

                                _unitOfWork.OrderRoomRepository.Update(update);
                                result = _unitOfWork.OrderRoomRepository.Commit();
                            }
                            else
                            {
                                request.StartTime = DateTime.Now;
                                request.Status = false;
                                request.CreatedBy = _loginBusiness.UserName;
                                request.CreatedDate = DateTime.Now;

                                _unitOfWork.OrderRoomRepository.Add(request);
                                result = _unitOfWork.OrderRoomRepository.Commit();
                            }
                        }

                        _unitOfWork.CommitTransaction();
                        if (result)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                                ResponseMessage = Constants.Message.SaveSuccess,
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
                    else
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                            ResponseMessage = Constants.Message.RecordNotFoundMessage,
                            Data = false
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
    }
}

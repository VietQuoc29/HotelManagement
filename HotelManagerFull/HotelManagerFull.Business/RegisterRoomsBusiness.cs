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
    /// IRegisterRoomsBusiness
    /// </summary>
    public interface IRegisterRoomsBusiness
    {
        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(RegisterRoomsRequest request);

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<RegisterRoomsViewModel>> GetAllAsync(PagingRequest request);
    }

    /// <summary>
    /// RegisterRoomsBusiness
    /// </summary>
    public class RegisterRoomsBusiness : IRegisterRoomsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginBusiness _loginBusiness;
        private readonly IDapperRepository _dapperRepository;

        /// <summary>
        /// RegisterRoomsBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="dapperRepository"></param>
        public RegisterRoomsBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness, IDapperRepository dapperRepository)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
            _dapperRepository = dapperRepository;
        }

        public async Task<ResponseDataList<RegisterRoomsViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<RegisterRoomsViewModel>("uspRegisterRooms_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<RegisterRoomsViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<RegisterRoomsViewModel>
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
        public async Task<ResponseData<bool>> SaveAsync(RegisterRoomsRequest request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.RegisterRoomsRepository.GetSingleAsync(x => x.Id == request.Id);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        update.Message = request.Message;
                        update.Status = request.Status;
                        update.ModifiedBy = _loginBusiness.UserName;
                        update.ModifiedDate = DateTime.Now;

                        _unitOfWork.RegisterRoomsRepository.Update(update);
                        result = _unitOfWork.RegisterRoomsRepository.Commit();
                    }
                    else
                    {
                        var model = new RegisterRooms
                        {
                            FullName = request.FullName,
                            Email = request.Email,
                            PhoneNumber = request.PhoneNumber,
                            RoomId = request.RoomId,
                            TimeFrom = request.TimeFrom,
                            TimeTo = request.TimeTo,
                            Note = request.Note,
                            Status = false,
                            CreatedDate = DateTime.Now
                        };
                        _unitOfWork.RegisterRoomsRepository.Add(model);
                        result = _unitOfWork.RegisterRoomsRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    if (result)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                            ResponseMessage = request.Id > 0 ? Constants.Message.SaveSuccess : "Chúc mùng bạn đã gửi yêu cầu thành công",
                            Data = true
                        };
                    }
                    else
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                            ResponseMessage = request.Id > 0 ? Constants.Message.SaveFail : "Đã có lỗi trong quá trình gửi yêu cầu. Vui lòng liên hệ với chúng tôi",
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
    }
}

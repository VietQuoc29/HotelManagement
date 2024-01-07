using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// IRoomStatusBusiness
    /// </summary>
    public interface IRoomStatusBusiness
    {
        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync();

        /// <summary>
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        ResponseDataList<RoomStatus> Suggestion();
    }

    /// <summary>
    /// RoomStatusBusiness
    /// </summary>
    public class RoomStatusBusiness : IRoomStatusBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// SexBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RoomStatusBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData<bool>> SaveAsync()
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    //insert master data sex
                    var listDataRoomStatus = new List<RoomStatus>();
                    var listRoomStatus = new List<RoomStatus>
                    {
                        new RoomStatus {Name = "Sẵn sàng sử dụng"},
                        new RoomStatus {Name = "Đang sử dụng"},
                        new RoomStatus {Name = "Đang dọn dẹp"},
                        new RoomStatus {Name = "Tạm dừng sửa chữa"},
                        new RoomStatus {Name = "Khác"}
                    };

                    foreach (var item in listRoomStatus)
                    {
                        var checkExists = await _unitOfWork.RoomStatusRepository.GetSingleAsync(x => x.Name.ToLower() == item.Name.ToLower());
                        if (checkExists == null)
                        {
                            listDataRoomStatus.Add(item);
                        }
                    }

                    if (listDataRoomStatus.Count > 0)
                    {
                        _unitOfWork.RoomStatusRepository.BulkInsert(listDataRoomStatus);
                        _unitOfWork.RoomStatusRepository.Commit();
                    }

                    _unitOfWork.CommitTransaction();
                    return new ResponseData<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.Message.GenerateDataSuccess,
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
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        public ResponseDataList<RoomStatus> Suggestion()
        {
            try
            {
                var list = _unitOfWork.RoomStatusRepository.GetAll();

                if (null == list)
                {
                    return new ResponseDataList<RoomStatus>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<RoomStatus>
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

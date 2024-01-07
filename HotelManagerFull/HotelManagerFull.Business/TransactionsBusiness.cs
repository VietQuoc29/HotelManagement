using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Request;
using HotelManagerFull.Share.Response;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// ITransactionsBusiness
    /// </summary>
    public interface ITransactionsBusiness
    {
        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(TransactionsRequest request);
    }

    /// <summary>
    /// TransactionsBusiness
    /// </summary>
    public class TransactionsBusiness : ITransactionsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginBusiness _loginBusiness;

        /// <summary>
        /// TransactionsBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        public TransactionsBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness)
        {
            _unitOfWork = unitOfWork;
            _loginBusiness = loginBusiness;
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> SaveAsync(TransactionsRequest request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var listData = new List<Transactions>();

                    foreach (var item in request.ListTransactions)
                    {
                        var data = new Transactions
                        {
                            OrderRoomId = item.OrderRoomId,
                            ServiceId = item.ServiceId,
                            Quantity = item.Quantity,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now
                        };

                        listData.Add(data);
                    }

                    if (listData.Count > 0)
                    {
                        _unitOfWork.TransactionsRepository.BulkInsert(listData);
                        _unitOfWork.TransactionsRepository.Commit();
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
    }
}

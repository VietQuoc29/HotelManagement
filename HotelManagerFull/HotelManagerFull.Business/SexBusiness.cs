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
    /// ISexBusiness
    /// </summary>
    public interface ISexBusiness
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
        ResponseDataList<Sex> Suggestion();
    }

    /// <summary>
    /// SexBusiness
    /// </summary>
    public class SexBusiness : ISexBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// SexBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        public SexBusiness(IUnitOfWork unitOfWork)
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
                    var listDataSex = new List<Sex>();
                    var listSex = new List<Sex>
                    {
                        new Sex {Name = "Nam"},
                        new Sex {Name = "Nữ"},
                        new Sex {Name = "Không xác định"}
                    };

                    foreach (var item in listSex)
                    {
                        var checkExists = await _unitOfWork.SexRepository.GetSingleAsync(x => x.Name.ToLower() == item.Name.ToLower());
                        if (checkExists == null)
                        {
                            listDataSex.Add(item);
                        }
                    }

                    if (listDataSex.Count > 0)
                    {
                        _unitOfWork.SexRepository.BulkInsert(listDataSex);
                        _unitOfWork.SexRepository.Commit();
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
        public ResponseDataList<Sex> Suggestion()
        {
            try
            {
                var list = _unitOfWork.SexRepository.GetAll();

                if (null == list)
                {
                    return new ResponseDataList<Sex>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<Sex>
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

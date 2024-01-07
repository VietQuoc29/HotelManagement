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
    /// IRolesBusiness
    /// </summary>
    public interface IRolesBusiness
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
        ResponseDataList<Roles> Suggestion();
    }

    /// <summary>
    /// RolesBusiness
    /// </summary>
    public class RolesBusiness : IRolesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// SexBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public RolesBusiness(IUnitOfWork unitOfWork)
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
                    //insert master data Roles
                    var listDataRoles = new List<Roles>();
                    var listRoles = new List<Roles>
                    {
                        new Roles {Name = "Administrator"},
                        new Roles {Name = "Nhân Viên"}
                    };

                    foreach (var item in listRoles)
                    {
                        var checkExists = await _unitOfWork.RolesRepository.GetSingleAsync(x => x.Name.ToLower() == item.Name.ToLower());
                        if (checkExists == null)
                        {
                            listDataRoles.Add(item);
                        }
                    }

                    if (listDataRoles.Count > 0)
                    {
                        _unitOfWork.RolesRepository.BulkInsert(listDataRoles);
                        _unitOfWork.RolesRepository.Commit();
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
        public ResponseDataList<Roles> Suggestion()
        {
            try
            {
                var list = _unitOfWork.RolesRepository.GetAll();

                if (null == list)
                {
                    return new ResponseDataList<Roles>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<Roles>
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

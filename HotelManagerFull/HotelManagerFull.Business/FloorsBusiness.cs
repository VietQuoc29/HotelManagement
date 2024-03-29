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
    /// IFloorsBusiness
    /// </summary>
    public interface IFloorsBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<FloorsViewModel>> GetAllAsync(PagingRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(FloorsRequest request, bool isDeleted);

        /// <summary>
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        ResponseDataList<Floors> Suggestion();
    }

    /// <summary>
    /// FloorsBusiness
    /// </summary>
    public class FloorsBusiness : IFloorsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;

        /// <summary>
        /// FloorsBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="dapperRepository"></param>
        public FloorsBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness,
            IDapperRepository dapperRepository)
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
        public async Task<ResponseDataList<FloorsViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<FloorsViewModel>("uspFloors_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<FloorsViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<FloorsViewModel>
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
        public async Task<ResponseData<bool>> SaveAsync(FloorsRequest request, bool isDeleted)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.FloorsRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
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
                            update.ModifiedBy = _loginBusiness.UserName;
                            update.ModifiedDate = DateTime.Now;
                        }
                        _unitOfWork.FloorsRepository.Update(update);
                        result = _unitOfWork.FloorsRepository.Commit();
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
                        var model = new Floors
                        {
                            Name = request.Name,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                        _unitOfWork.FloorsRepository.Add(model);
                        result = _unitOfWork.FloorsRepository.Commit();
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
        /// CheckDuplicate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private async Task<bool> CheckDuplicate(long id, string name)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.FloorsRepository.GetSingleAsync(x => x.Id != id && x.Name.ToLower() == name.ToLower() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.FloorsRepository.GetSingleAsync(x => x.Name.ToLower() == name.ToLower() && !x.IsDeleted);
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
        public ResponseDataList<Floors> Suggestion()
        {
            try
            {
                var list = _unitOfWork.FloorsRepository.GetAll().Where(x => !x.IsDeleted);

                if (null == list)
                {
                    return new ResponseDataList<Floors>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<Floors>
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

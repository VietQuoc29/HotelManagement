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
    /// ICustomersBusiness
    /// </summary>
    public interface ICustomersBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<CustomersViewModel>> GetAllAsync(PagingRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(CustomersRequest request, bool isDeleted);

        /// <summary>
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        ResponseDataList<Customers> Suggestion(string textSearch);

        /// <summary>
        /// GetCustomerInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Customers GetCustomerInfo(long id);
    }

    /// <summary>
    /// CustomersBusiness
    /// </summary>
    public class CustomersBusiness : ICustomersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;

        /// <summary>
        /// ProvincesBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="loginBusiness"></param>
        /// <param name="dapperRepository"></param>
        /// <param name="options"></param>
        public CustomersBusiness(IUnitOfWork unitOfWork, ILoginBusiness loginBusiness,
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
        public async Task<ResponseDataList<CustomersViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<CustomersViewModel>("uspCustomers_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<CustomersViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<CustomersViewModel>
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
        /// GetCustomerInfo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customers GetCustomerInfo(long id)
        {
            return _unitOfWork.CustomersRepository.GetSingle(x => x.Id == id && !x.IsDeleted);
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> SaveAsync(CustomersRequest request, bool isDeleted)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.CustomersRepository.GetSingleAsync(x => x.Id == request.Id && !x.IsDeleted);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        if (!isDeleted && await CheckDuplicate(request.Id, request.IDCard))
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
                            update.FullName = request.FullName;
                            update.IDCard = request.IDCard;
                            update.PhoneNumber = request.PhoneNumber;
                            update.Address = request.Address;
                            update.DateOfBirth = request.DateOfBirth;
                            update.Note = request.Note;
                            update.SexId = request.SexId;
                            update.ModifiedBy = _loginBusiness.UserName;
                            update.ModifiedDate = DateTime.Now;
                        }
                        _unitOfWork.CustomersRepository.Update(update);
                        result = _unitOfWork.CustomersRepository.Commit();
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

                        if (!isDeleted && await CheckDuplicate(request.Id, request.IDCard))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }
                        var model = new Customers
                        {
                            FullName = request.FullName,
                            IDCard = request.IDCard,
                            PhoneNumber = request.PhoneNumber,
                            Address = request.Address,
                            DateOfBirth = request.DateOfBirth,
                            Note = request.Note,
                            SexId = request.SexId,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now,
                            IsDeleted = false
                        };
                        _unitOfWork.CustomersRepository.Add(model);
                        result = _unitOfWork.CustomersRepository.Commit();
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
        /// Suggestion
        /// </summary>
        /// <param name="textSearch"></param>
        /// <returns></returns>
        public ResponseDataList<Customers> Suggestion(string textSearch)
        {
            try
            {
                var list = _unitOfWork.CustomersRepository.GetAll().Where(x => x.IDCard.Contains(textSearch) && !x.IsDeleted);

                if (null == list)
                {
                    return new ResponseDataList<Customers>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<Customers>
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
        /// CheckDuplicate
        /// </summary>
        /// <param name="id"></param>
        /// <param name="iDCard"></param>
        /// <returns></returns>
        private async Task<bool> CheckDuplicate(long id, string iDCard)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.CustomersRepository.GetSingleAsync(x => x.Id != id && x.IDCard.ToLower() == iDCard.ToLower() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.CustomersRepository.GetSingleAsync(x => x.IDCard.ToLower() == iDCard.ToLower() && !x.IsDeleted);
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

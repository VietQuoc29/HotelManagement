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
    /// IUserProfilesBusiness
    /// </summary>
    public interface IUserProfilesBusiness
    {
        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseDataList<UserProfilesViewModel>> GetAllAsync(PagingRequest request);

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> SaveAsync(UserProfilesRequest request);

        /// <summary>
        /// ActiveUserAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        Task<ResponseData<bool>> ActiveUserAsync(long id, bool isActive);
    }

    /// <summary>
    /// UserProfilesBusiness
    /// </summary>
    public class UserProfilesBusiness : IUserProfilesBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDapperRepository _dapperRepository;
        private readonly ILoginBusiness _loginBusiness;

        /// <summary>
        /// UserProfilesBusiness
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="dapperRepository"></param>
        public UserProfilesBusiness(IUnitOfWork unitOfWork, IDapperRepository dapperRepository, ILoginBusiness loginBusiness)
        {
            _unitOfWork = unitOfWork;
            _dapperRepository = dapperRepository;
            _loginBusiness = loginBusiness;
        }

        /// <summary>
        /// ActiveUserAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public async Task<ResponseData<bool>> ActiveUserAsync(long id, bool isActive)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    if (id <= 0)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                            ResponseMessage = Constants.Message.IdNotFound,
                            Data = false
                        };
                    }

                    var active = await _unitOfWork.UserProfilesRepository.GetSingleAsync(x => x.Id == id);
                    if (active == null || active.Id <= 0)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                            ResponseMessage = Constants.Message.RecordNotFoundMessage,
                            Data = false
                        };
                    }
                    active.Active = isActive;
                    active.ModifiedBy = _loginBusiness.UserName;
                    active.ModifiedDate = DateTime.Now;

                    _unitOfWork.UserProfilesRepository.Update(active);
                    _unitOfWork.UserProfilesRepository.Commit();
                    _unitOfWork.CommitTransaction();

                    if (isActive)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                            ResponseMessage = Constants.Message.ActiveMessage,
                            Data = true
                        };
                    }
                    return new ResponseData<bool>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                        ResponseMessage = Constants.Message.InActiveMessage,
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
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseDataList<UserProfilesViewModel>> GetAllAsync(PagingRequest request)
        {
            try
            {
                var param = new DynamicParameters();
                param.Add(Constants.Key, request.SearchText);
                param.Add(Constants.Page, request.Page);
                param.Add(Constants.PageSize, request.PageSize);
                var list = await _dapperRepository.QueryMultipleUsingStoreProcAsync<UserProfilesViewModel>("uspUserProfiles_GetAll", param);

                if (null == list)
                {
                    return new ResponseDataList<UserProfilesViewModel>
                    {
                        ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                        ResponseMessage = Constants.Message.RecordNotFoundMessage,
                        Data = null,
                        TotalRecords = 0
                    };
                }

                return new ResponseDataList<UserProfilesViewModel>
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
        public async Task<ResponseData<bool>> SaveAsync(UserProfilesRequest request)
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    var result = false;
                    if (request.Id > 0)
                    {
                        var update = await _unitOfWork.UserProfilesRepository.GetSingleAsync(x => x.Id == request.Id);
                        if (update == null || update.Id <= 0)
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.NotFound),
                                ResponseMessage = Constants.Message.RecordNotFoundMessage,
                                Data = false
                            };
                        }

                        if (await CheckDuplicate(request.Id, request.UserName))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }

                        update.FullName = request.FullName;
                        update.DateOfBirth = request.DateOfBirth;
                        update.PhoneNumber = request.PhoneNumber;
                        update.Address = request.Address;
                        update.Email = request.Email;
                        update.Facebook = request.Facebook;
                        update.Zalo = request.Zalo;
                        update.SexId = request.SexId;
                        update.RoleId = request.RoleId;
                        update.ModifiedBy = _loginBusiness.UserName;
                        update.ModifiedDate = DateTime.Now;
                        _unitOfWork.UserProfilesRepository.Update(update);
                        result = _unitOfWork.UserProfilesRepository.Commit();
                    }
                    else
                    {
                        if (await CheckDuplicate(request.Id, request.UserName))
                        {
                            return new ResponseData<bool>
                            {
                                ResponseCode = Convert.ToInt32(HttpStatusCode.Conflict),
                                ResponseMessage = Constants.Message.DuplicateMessage,
                                Data = false
                            };
                        }
                        var model = new UserProfiles
                        {
                            UserName = request.UserName,
                            FullName = request.FullName,
                            PassHash = EncriptFunctions.GeneratePassword(request.UserName),
                            DateOfBirth = request.DateOfBirth,
                            PhoneNumber = request.PhoneNumber,
                            Address = request.Address,
                            Email = request.Email,
                            Facebook = request.Facebook,
                            Zalo = request.Zalo,
                            Active = true,
                            SexId = request.SexId,
                            RoleId = request.RoleId,
                            CreatedBy = _loginBusiness.UserName,
                            CreatedDate = DateTime.Now
                        };
                        _unitOfWork.UserProfilesRepository.Add(model);
                        result = _unitOfWork.UserProfilesRepository.Commit();
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
        /// <param name="userName"></param>
        /// <returns></returns>
        private async Task<bool> CheckDuplicate(long id, string userName)
        {
            if (id > 0)
            {
                var checkDuplicate = await _unitOfWork.UserProfilesRepository.GetSingleAsync(x => x.Id != id && x.UserName.ToLower() == userName.ToLower());
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            if (id <= 0)
            {
                var checkDuplicate = await _unitOfWork.UserProfilesRepository.GetSingleAsync(x => x.UserName.ToLower() == userName.ToLower());
                if (checkDuplicate != null)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

using HotelManagerFull.Repository;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Factory;
using HotelManagerFull.Share.Request;
using HotelManagerFull.Share.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelManagerFull.Business
{
    /// <summary>
    /// ILoginBusiness
    /// </summary>
    public interface ILoginBusiness
    {
        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResponseData<ResponseAuthenticate>> Authenticate(LoginRequest request);

        /// <summary>
        /// GenerateDataAsync
        /// </summary>
        /// <returns></returns>
        Task<ResponseData<bool>> GenerateDataAsync();

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName { get; }
    }

    /// <summary>
    /// LoginBusiness
    /// </summary>
    public class LoginBusiness : ILoginBusiness
    {
        private readonly ITokenFactory _tokenFactory;
        private readonly IJwtFactory _jwtFactory;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDapperRepository _dapperRepository;
        private readonly ISexBusiness _sexBusiness;
        private readonly IRoomStatusBusiness _roomStatusBusiness;
        private readonly IRolesBusiness _rolesBusiness;

        /// <summary>
        /// LoginBusiness
        /// </summary>
        /// <param name="tokenFactory"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="unitOfWork"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="dapperRepository"></param>
        /// <param name="sexBusiness"></param>
        /// <param name="roomStatusBusiness"></param>
        /// <param name="rolesBusiness"></param>
        public LoginBusiness(ITokenFactory tokenFactory, IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions, IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor, IDapperRepository dapperRepository,
            ISexBusiness sexBusiness, IRoomStatusBusiness roomStatusBusiness,
            IRolesBusiness rolesBusiness)
        {
            _tokenFactory = tokenFactory;
            _jwtFactory = jwtFactory;
            _jwtOptions = jwtOptions.Value;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _dapperRepository = dapperRepository;
            _sexBusiness = sexBusiness;
            _roomStatusBusiness = roomStatusBusiness;
            _rolesBusiness = rolesBusiness;
        }

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ResponseData<ResponseAuthenticate>> Authenticate(LoginRequest request)
        {
            var claims = new List<Claim>();

            var user = await _unitOfWork.UserProfilesRepository.GetSingleAsync(x => x.UserName.ToLower() == request.UserName.ToLower() && x.PassHash == EncriptFunctions.GeneratePassword(request.Password) && x.Active);
            if (user == null)
            {
                return new ResponseData<ResponseAuthenticate>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.Message.UserInActiveMessage
                };
            }

            var fullname = user.FullName;

            var userRole = user.RoleId;
            if (userRole == null)
            {
                return new ResponseData<ResponseAuthenticate>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.Message.UserRoleMessage
                };
            }

            var role = await _unitOfWork.RolesRepository.GetSingleAsync(x => x.Id == userRole);
            if (role == null)
            {
                return new ResponseData<ResponseAuthenticate>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                    ResponseMessage = Constants.Message.RoleMessage
                };
            }

            var roleName = role.Name;
            var identity = await GetClaimsIdentity(request);
            claims.Add(new Claim(ClaimTypes.Role, roleName));
            var email = user.Email;
            var jwt = await _tokenFactory.GenerateJwt(identity,
                                                        _jwtFactory,
                                                       request.UserName,
                                                       _jwtOptions,
                                                       new JsonSerializerSettings { Formatting = Formatting.Indented },
                                                       fullname,
                                                       roleName,
                                                       email,
                                                       claims);
            var jwtObject = JsonConvert.DeserializeObject<ResponseAuthenticate>(jwt);
            return new ResponseData<ResponseAuthenticate>
            {
                ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                ResponseMessage = Constants.Message.Successfully,
                Data = jwtObject
            };
        }

        /// <summary>
        /// GenerateDataAsync
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseData<bool>> GenerateDataAsync()
        {
            using (var tran = await _unitOfWork.OpenTransaction())
            {
                try
                {
                    //insert master data sex
                    var resultSex = await _sexBusiness.SaveAsync();

                    //insert master data RoomStatus
                    var resultRoomStatus = await _roomStatusBusiness.SaveAsync();

                    //insert master data Roles
                    var resultRoles = await _rolesBusiness.SaveAsync();
                    var resultUserProfiles = false;
                    if (resultRoles.Data)
                    {
                        //insert master data UserProfiles
                        resultUserProfiles = (await SaveDataAsync()).Data;
                    }

                    if (resultSex.Data && resultRoomStatus.Data && resultRoles.Data && resultUserProfiles)
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                            ResponseMessage = Constants.Message.GenerateDataSuccess,
                            Data = true
                        };
                    }
                    else
                    {
                        return new ResponseData<bool>
                        {
                            ResponseCode = Convert.ToInt32(HttpStatusCode.InternalServerError),
                            ResponseMessage = Constants.Message.GenerateDataFail,
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

        /// <summary>
        /// GetClaimsIdentity
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private async Task<ClaimsIdentity> GetClaimsIdentity(LoginRequest request)
        {
            if (string.IsNullOrEmpty(request?.UserName) || string.IsNullOrEmpty(request?.Password))
                return await Task.FromResult<ClaimsIdentity>(null);

            // get the user to verifty
            var userToVerify = _unitOfWork.UserProfilesRepository.FindBy(x => x.UserName == request.UserName);

            if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (EncriptFunctions.GeneratePassword(request.Password) == userToVerify.FirstOrDefault().PassHash)
            {
                return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(request.UserName, userToVerify.FirstOrDefault().Id));
            }

            // Credentials are invalid, or account doesn't exist
            return await Task.FromResult<ClaimsIdentity>(null);
        }

        /// <summary>
        /// UserId
        /// </summary>
        public string UserId
        {
            get
            {
                var firstUser = _dapperRepository.QueryFirstOrDefault<string>("SELECT TOP 1 UserName FROM UserProfiles") ?? string.Empty;
                var result = _httpContextAccessor.HttpContext?.User?.FindFirstValue("id");
                return result ?? firstUser;
            }
        }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName => _dapperRepository.QueryFirstOrDefault<string>($"SELECT TOP 1 UserName FROM UserProfiles WHERE Id = '{UserId}'") ?? string.Empty;

        /// <summary>
        /// SaveDataAsync
        /// </summary>
        /// <returns></returns>
        private async Task<ResponseData<bool>> SaveDataAsync()
        {
            try
            {
                //insert master data UserProfiles
                var userProfiles = new UserProfiles
                {
                    UserName = "admin",
                    PassHash = EncriptFunctions.GeneratePassword("admin"),
                    FullName = "Administrator",
                    Active = true,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                };
                var role = await _unitOfWork.RolesRepository.GetSingleAsync(x => x.Name.ToLower() == "Administrator".ToLower());
                var checkExistsUserProfiles = await _unitOfWork.UserProfilesRepository.GetSingleAsync(x => x.UserName.ToLower() == userProfiles.UserName.ToLower());
                if (checkExistsUserProfiles == null)
                {
                    userProfiles.RoleId = role.Id;
                    _unitOfWork.UserProfilesRepository.Add(userProfiles);
                    _unitOfWork.UserProfilesRepository.Commit();
                }

                return new ResponseData<bool>
                {
                    ResponseCode = Convert.ToInt32(HttpStatusCode.OK),
                    ResponseMessage = Constants.Message.GenerateDataSuccess,
                    Data = true
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

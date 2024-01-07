using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Response;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelManagerFull.Share.Factory
{
    /// <summary>
    /// ITokenFactory
    /// </summary>
    public interface ITokenFactory
    {
        Task<string> GenerateJwt(ClaimsIdentity identity,
                                    IJwtFactory jwtFactory,
                                   string userName,
                                   JwtIssuerOptions jwtOptions,
                                   JsonSerializerSettings serializerSettings,
                                   string fullName,
                                   string roleName,
                                   string email,
                                   List<Claim> claims = null
                                   );
    }

    /// <summary>
    /// TokenFactory
    /// </summary>
    public class TokenFactory : ITokenFactory
    {
        /// <summary>
        /// GenerateJwt
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="jwtFactory"></param>
        /// <param name="userName"></param>
        /// <param name="jwtOptions"></param>
        /// <param name="serializerSettings"></param>
        /// <param name="fullName"></param>
        /// <param name="roleName"></param>
        /// <param name="email"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory,
             string userName, JwtIssuerOptions jwtOptions,
             JsonSerializerSettings serializerSettings,
             string fullName, string roleName, string email,
              List<Claim> claims = null)
        {
            var response = new ResponseAuthenticate
            {
                Id = identity.Claims.Single(c => c.Type == "id").Value,
                Token = await jwtFactory.GenerateEncodedToken(userName, identity, claims),
                ExpireTime = (int)jwtOptions.ValidFor.TotalSeconds,
                FullName = fullName,
                RoleName = roleName,
                Email = email,
                Username = userName
            };
            return JsonConvert.SerializeObject(response, serializerSettings);
        }
    }
}

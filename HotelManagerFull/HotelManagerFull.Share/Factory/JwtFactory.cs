using HotelManagerFull.Share.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HotelManagerFull.Share.Factory
{
    /// <summary>
    /// IJwtFactory
    /// </summary>
    public interface IJwtFactory
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, List<Claim> additionalClaims);
        ClaimsIdentity GenerateClaimsIdentity(string userName, long id);
    }

    /// <summary>
    /// JwtFactory
    /// </summary>
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        /// <summary>
        /// JwtFactory
        /// </summary>
        /// <param name="jwtOptions"></param>
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <summary>
        /// GenerateClaimsIdentity
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClaimsIdentity GenerateClaimsIdentity(string userName, long id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
           {
                new Claim(Constants.JwtClaimIdentifiers.Id, id.ToString()),
                new Claim(Constants.JwtClaimIdentifiers.Rol, Constants.JwtClaims.ApiAccess)
            });
        }

        /// <summary>
        /// GenerateEncodedToken
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="identity"></param>
        /// <param name="additionalClaims"></param>
        /// <returns></returns>
        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, List<Claim> additionalClaims)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                identity.FindFirst(Constants.JwtClaimIdentifiers.Rol),
                identity.FindFirst(Constants.JwtClaimIdentifiers.Id)
            };
            if (additionalClaims != null && additionalClaims.Count > 0)
            {
                claims.AddRange(additionalClaims);
            }
            var jwt = new JwtSecurityToken(
               issuer: _jwtOptions.Issuer,
               audience: _jwtOptions.Audience,
               claims: claims,
               notBefore: _jwtOptions.NotBefore,
               expires: _jwtOptions.Expiration,
               signingCredentials: _jwtOptions.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// ToUnixEpochDate
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() -
                             new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
            .TotalSeconds);
    }
}

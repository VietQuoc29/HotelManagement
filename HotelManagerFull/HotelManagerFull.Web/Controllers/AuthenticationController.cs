using System;
using System.Net;
using System.Threading.Tasks;
using HotelManagerFull.Business;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Request;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// AuthenticationController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILoginBusiness _loginBusiness;

        /// <summary>
        /// AuthenticationController
        /// </summary>
        /// <param name="loginBusiness"></param>
        public AuthenticationController(ILoginBusiness loginBusiness)
        {
            _loginBusiness = loginBusiness;
        }

        /// <summary>
        /// LoginAsync
        /// </summary>
        /// /// <remarks>
        /// {
        ///  "userName": "admin",
        ///  "password": "admin"
        /// }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _loginBusiness.Authenticate(request));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// GenerateDataAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet("GenerateData")]
        public async Task<IActionResult> GenerateDataAsync()
        {
            try
            {
                return Ok(await _loginBusiness.GenerateDataAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

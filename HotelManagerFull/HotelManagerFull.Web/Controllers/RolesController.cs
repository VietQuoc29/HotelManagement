using HotelManagerFull.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// RolesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : ControllerBase
    {
        private readonly IRolesBusiness _rolesBusiness;

        /// <summary>
        /// RolesController
        /// </summary>
        /// <param name="rolesBusiness"></param>
        public RolesController(IRolesBusiness rolesBusiness)
        {
            _rolesBusiness = rolesBusiness;
        }

        /// <summary>
        /// Suggestion
        /// </summary>
        /// <returns></returns>
        [HttpGet("Suggestion")]
        public IActionResult Suggestion()
        {
            try
            {
                var result = _rolesBusiness.Suggestion();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

    }
}

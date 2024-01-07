using HotelManagerFull.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// SexController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SexController : ControllerBase
    {
        private readonly ISexBusiness _sexBusiness;

        /// <summary>
        /// SexController
        /// </summary>
        /// <param name="sexBusiness"></param>
        public SexController(ISexBusiness sexBusiness)
        {
            _sexBusiness = sexBusiness;
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
                var result = _sexBusiness.Suggestion();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

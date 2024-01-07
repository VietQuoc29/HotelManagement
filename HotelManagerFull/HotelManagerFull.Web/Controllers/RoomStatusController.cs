using HotelManagerFull.Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// RoomStatusController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoomStatusController : ControllerBase
    {
        private readonly IRoomStatusBusiness _roomStatusBusiness;

        /// <summary>
        /// RoomStatusController
        /// </summary>
        /// <param name="roomStatusBusiness"></param>
        public RoomStatusController(IRoomStatusBusiness roomStatusBusiness)
        {
            _roomStatusBusiness = roomStatusBusiness;
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
                var result = _roomStatusBusiness.Suggestion();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

    }
}

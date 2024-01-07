using HotelManagerFull.Business;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Entities;
using HotelManagerFull.Share.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// OrderRoomController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderRoomController : ControllerBase
    {
        private readonly IOrderRoomBusiness _orderRoomBusiness;

        /// <summary>
        /// FloorsController
        /// </summary>
        /// <param name="orderRoomBusiness"></param>
        public OrderRoomController(IOrderRoomBusiness orderRoomBusiness)
        {
            _orderRoomBusiness = orderRoomBusiness;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PagingRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                var result = await _orderRoomBusiness.GetAllAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync([FromForm] OrderRoom request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _orderRoomBusiness.SaveAsync(request));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

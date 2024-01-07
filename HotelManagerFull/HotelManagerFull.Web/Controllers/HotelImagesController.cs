using HotelManagerFull.Business;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// HotelImagesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class HotelImagesController : ControllerBase
    {
        private readonly IHotelImagesBusiness _hotelImagesBusiness;

        /// <summary>
        /// HotelImagesController
        /// </summary>
        /// <param name="hotelImagesBusiness"></param>
        public HotelImagesController(IHotelImagesBusiness hotelImagesBusiness)
        {
            _hotelImagesBusiness = hotelImagesBusiness;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(Constants.Message.IdNotFound);
                }
                return Ok(await _hotelImagesBusiness.DeleteAsync(id));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// GetAllHotelImage
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("GetAllHotelImage")]
        public IActionResult GetAllHotelImage([FromQuery] long? roomId)
        {
            try
            {
                var result = _hotelImagesBusiness.GetAllHotelImage(roomId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// UploadImageAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UploadImage")]
        public async Task<IActionResult> UploadImageAsync([FromForm] HotelImageRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _hotelImagesBusiness.UploadImageAsync(request));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

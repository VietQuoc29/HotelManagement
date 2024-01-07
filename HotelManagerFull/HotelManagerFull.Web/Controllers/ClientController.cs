using HotelManagerFull.Business;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// ClientController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IProvincesBusiness _provincesBusiness;
        private readonly IHotelImagesBusiness _hotelImagesBusiness;
        private readonly IHotelsBusiness _hotelsBusiness;
        private readonly IRoomsBusiness _roomsBusiness;
        private readonly IRegisterRoomsBusiness _registerRoomsBusiness;

        /// <summary>
        /// ClientController
        /// </summary>
        /// <param name="hotelImagesBusiness"></param>
        /// <param name="provincesBusiness"></param>
        /// <param name="hotelsBusiness"></param>
        /// <param name="roomsBusiness"></param>
        /// <param name="registerRoomsBusiness"></param>
        public ClientController(IHotelImagesBusiness hotelImagesBusiness, IProvincesBusiness provincesBusiness,
            IHotelsBusiness hotelsBusiness, IRoomsBusiness roomsBusiness, IRegisterRoomsBusiness registerRoomsBusiness)
        {
            _provincesBusiness = provincesBusiness;
            _hotelImagesBusiness = hotelImagesBusiness;
            _hotelsBusiness = hotelsBusiness;
            _roomsBusiness = roomsBusiness;
            _registerRoomsBusiness = registerRoomsBusiness;
        }

        /// <summary>
        /// GetAllProvinces
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllProvinces")]
        public IActionResult GetAllProvinces()
        {
            try
            {
                var result = _provincesBusiness.GetAllProvinces();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// GetAllGallery
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllGallery")]
        public IActionResult GetAllGallery()
        {
            try
            {
                var result = _hotelImagesBusiness.GetAllGallery();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// GetAllHotelByProvinces
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        [HttpGet("GetAllHotelByProvinces")]
        public IActionResult GetAllHotelByProvinces(long provinceId)
        {
            try
            {
                var result = _hotelsBusiness.GetAllHotelByProvinces(provinceId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// GetAllRoomByHotel
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("GetAllRoomByHotel")]
        public async Task<IActionResult> GetAllRoomByHotel([FromQuery] RoomByHotelRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                var result = await _roomsBusiness.GetAllRoomByHotel(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// GetAllRoomDetail
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        [HttpGet("GetAllRoomDetail")]
        public async Task<IActionResult> GetAllRoomDetail(long roomId)
        {
            try
            {
                var result = await _roomsBusiness.GetAllRoomDetail(roomId);
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
        [HttpPost("OrderRoom")]
        public async Task<IActionResult> OrderRoomAsync([FromForm] RegisterRoomsRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _registerRoomsBusiness.SaveAsync(request));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

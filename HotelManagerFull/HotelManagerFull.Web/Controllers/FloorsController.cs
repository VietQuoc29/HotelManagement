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
    /// FloorsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FloorsController : ControllerBase
    {
        private readonly IFloorsBusiness _floorsBusiness;

        /// <summary>
        /// FloorsController
        /// </summary>
        /// <param name="floorsBusiness"></param>
        public FloorsController(IFloorsBusiness floorsBusiness)
        {
            _floorsBusiness = floorsBusiness;
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
                var result = await _floorsBusiness.GetAllAsync(request);
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
        public async Task<IActionResult> SaveAsync([FromForm] FloorsRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _floorsBusiness.SaveAsync(request, false));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
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
                var request = new FloorsRequest
                {
                    Id = id
                };

                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _floorsBusiness.SaveAsync(request, true));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
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
                var result = _floorsBusiness.Suggestion();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

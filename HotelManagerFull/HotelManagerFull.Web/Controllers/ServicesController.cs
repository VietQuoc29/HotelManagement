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
    /// ServicesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesBusiness _servicesBusiness;

        /// <summary>
        /// ServicesController
        /// </summary>
        /// <param name="servicesBusiness"></param>
        public ServicesController(IServicesBusiness servicesBusiness)
        {
            _servicesBusiness = servicesBusiness;
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
                var result = await _servicesBusiness.GetAllAsync(request);
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
        public async Task<IActionResult> SaveAsync([FromForm] ServicesRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _servicesBusiness.SaveAsync(request, false));
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
                var request = new ServicesRequest
                {
                    Id = id
                };

                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _servicesBusiness.SaveAsync(request, true));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

    }
}

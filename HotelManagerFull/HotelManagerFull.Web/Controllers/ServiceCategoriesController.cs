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
    /// ServiceCategoriesController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ServiceCategoriesController : ControllerBase
    {
        private readonly IServiceCategoriesBusiness _serviceCategoriesBusiness;

        /// <summary>
        /// ServiceCategoriesController
        /// </summary>
        /// <param name="serviceCategoriesBusiness"></param>
        public ServiceCategoriesController(IServiceCategoriesBusiness serviceCategoriesBusiness)
        {
            _serviceCategoriesBusiness = serviceCategoriesBusiness;
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
                var result = await _serviceCategoriesBusiness.GetAllAsync(request);
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
        public async Task<IActionResult> SaveAsync([FromForm] ServiceCategoriesRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _serviceCategoriesBusiness.SaveAsync(request, false));
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
                var request = new ServiceCategoriesRequest
                {
                    Id = id
                };

                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _serviceCategoriesBusiness.SaveAsync(request, true));
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
                var result = _serviceCategoriesBusiness.Suggestion();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }

        /// <summary>
        /// GetAllSerrviceByServiceCategoriesAsync
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllSerrviceByServiceCategories")]
        public IActionResult GetAllSerrviceByServiceCategoriesAsync()
        {
            try
            {
                var result = _serviceCategoriesBusiness.GetAllSerrviceByServiceCategoriesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

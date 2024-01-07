using HotelManagerFull.Business;
using HotelManagerFull.Share.Common;
using HotelManagerFull.Share.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelManagerFull.Web.Controllers
{
    /// <summary>
    /// TransactionsController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsBusiness _transactionsBusiness;

        /// <summary>
        /// CustomersController
        /// </summary>
        /// <param name="transactionsBusiness"></param>
        public TransactionsController(ITransactionsBusiness transactionsBusiness)
        {
            _transactionsBusiness = transactionsBusiness;
        }

        /// <summary>
        /// SaveAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync([FromForm] TransactionsRequest request)
        {
            try
            {
                if (!ModelState.IsValid || null == request)
                {
                    return BadRequest(Constants.Message.ModelStateMessage);
                }
                return Ok(await _transactionsBusiness.SaveAsync(request));
            }
            catch (Exception ex)
            {
                return StatusCode(Convert.ToInt32(HttpStatusCode.InternalServerError), ex.ToString());
            }
        }
    }
}

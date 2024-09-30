using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using Service.Models.Transaction;
using System.Security.Claims;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _commissionTransactionService;

        public TransactionController(ITransactionService commissionTransactionService)
        {
            _commissionTransactionService = commissionTransactionService;
        }

        [HttpPost]
        [Authorize]
        [Route("commission")]
        public async Task<IActionResult> GetPaymentLink(TransactionRequestModel requestModel)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _commissionTransactionService.CreatePaymentUrl(requestModel, email));
        }

        [HttpGet("Callback")]
        public async Task<IActionResult> PaymentCallback([FromQuery] ZaloPayCallbackModel model)
        {
            return Ok(await _commissionTransactionService.PaymentUrlCallbackProcessing(model));
        }
    }
}

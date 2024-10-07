using EXE201_Workshopista.Middlewares;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net.payOS.Types;
using Newtonsoft.Json;
using Service.Interfaces;
using Service.Models.Transaction;
using System.Security.Claims;

namespace EXE201_Workshopista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;


        public TransactionController(ITransactionService commissionTransactionService, ILogger<TransactionController> logger)
        {
            _transactionService = commissionTransactionService;
            _logger = logger;
        }

        [HttpPost]
        [Authorize]
        [Route("commission")]
        public async Task<IActionResult> GetPaymentLink(TransactionRequestModel requestModel)
        {
            var email = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value.ToString();
            return Ok(await _transactionService.CreatePaymentUrl(requestModel, email));
        }

        [HttpPost("Callback")]
        public async Task<IActionResult> PaymentCallback([FromBody] WebhookType model)
        {
            var result = await _transactionService.PaymentUrlCallbackProcessing(model);
            return result.Success ? Ok(result) : BadRequest(result);
        }
    }
}

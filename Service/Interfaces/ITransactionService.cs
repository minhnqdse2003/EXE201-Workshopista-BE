using Service.Models;
using Service.Models.Momo;
using Service.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Service.Interfaces
{
    public interface ITransactionService
    {
        Task<ApiResponse<object>> CreatePaymentUrl(TransactionRequestModel requestModel,string email);
        Task<ApiResponse<string>> PaymentUrlCallbackProcessing(Net.payOS.Types.WebhookType model);
        Task<ApiResponse<TransactionDto>> Get();
        Task<ApiResponse<List<SubscriptionDto>>> GetSubscription(string email);
    }
}

﻿using Service.Models;
using Service.Models.Momo;
using Service.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITransactionService
    {
        Task<ApiResponse<object>> CreatePaymentUrl(TransactionRequestModel requestModel,string email);
        Task<ApiResponse<string>> PaymentUrlCallbackProcessing(ZaloPayCallbackModel model);
    }
}
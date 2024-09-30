using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Transaction
{
    public class ZaloPayCallbackModel
    {
        public decimal Amount { get; set; }
        public string AppId { get; set; }
        public string AppTransId { get; set; }
        public string? BankCode { get; set; }
        public string Checksum { get; set; }
        public decimal DiscountAmount { get; set; }
        public int PmcId { get; set; }
        public int Status { get; set; }
    }
}

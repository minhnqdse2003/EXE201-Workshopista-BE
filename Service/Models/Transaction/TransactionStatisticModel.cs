using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Transaction
{
    public class TransactionStatisticModel
    {
        public decimal TotalAmount { get; set; }
        public decimal MonthAmount { get; set; }
        public decimal SevenDaysAmount { get; set; }
    }
}

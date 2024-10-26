using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Transaction
{
    public class PayosCallbackModel
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public ICollection<object>? Data { get; set; }
    }
}

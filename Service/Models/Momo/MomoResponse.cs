using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Momo
{
    public class MomoResponse
    {
        public string partnerCode { get; set; }
        public string orderId { get; set; }
        public string requestId { get; set; }
        public int amount { get; set; }
        public long responseTime { get; set; }
        public string message { get; set; }
        public int resultCode { get; set; }
        public string payUrl { get; set; }
        public string shortLink { get; set; }
    }
}

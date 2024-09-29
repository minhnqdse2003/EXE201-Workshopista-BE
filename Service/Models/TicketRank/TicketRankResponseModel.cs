using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.TicketRank
{
    public class TicketRankResponseModel
    {
        public Guid TicketRankId { get; set; }

        public Guid? WorkshopId { get; set; }

        public string RankName { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public int Capacity { get; set; }
    }
}

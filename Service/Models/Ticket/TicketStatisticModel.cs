using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Ticket
{
    public class TicketStatisticModel
    {
        public int TodayTicket { get; set; }
        public decimal TodayAmount { get; set; }
        public int AllTicket { get; set; }
        public decimal AllAmount { get; set; }
    }
}

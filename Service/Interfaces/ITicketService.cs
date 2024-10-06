using Service.Models.Ticket;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ITicketService
    {
        string GenerateQRCode(Guid ticketId);
        bool VerifyQRCode(string scannedHash, Guid ticketId);
        string GenerateTicketPrivateKey(Guid ticketId);
        Task<bool> Verify(string hashData);
        Task<ApiResponse<List<TicketDto>>> GetUserTicket(string userName);

    }
}

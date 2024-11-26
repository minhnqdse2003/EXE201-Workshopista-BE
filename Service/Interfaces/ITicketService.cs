using Service.Models.Ticket;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Service.Interfaces
{
    public interface ITicketService
    {
        string GenerateQRCode(Guid ticketId);
        bool VerifyQRCode(string scannedHash, Guid ticketId);
        string GenerateTicketPrivateKey(Guid ticketId);
        Task<bool> Verify(string hashData);
        Task<ApiResponse<List<ListTicketDto>>> GetUserTicket(string userName);
        Task<ApiResponse<Ticket>> UpdateTicket(TicketUpdateModel updateModel);
        Task<ApiResponse<TicketDetailsDto?>> GetTicketDetails(string ticketId);
        
    }
}

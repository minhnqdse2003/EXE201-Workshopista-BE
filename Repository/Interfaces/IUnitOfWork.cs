using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        IOrderDetailsRepository OrderDetails { get; }
        ITicketRepository Tickets { get; }
        IOrganizerRepository Organizers { get; }
        IWorkshopRepository Workshops { get; }
        ITransactionRepository Transactions { get; }
        ISubscriptionRepository Subscriptions { get; }
        ICommissionRepository Commissions { get; }
        IPromotionRepository Promotions { get; }
        ISubscriptionTransactionRepository SubscriptionTransactions { get; }
        ICommissionTransactionRepository CommissionTransactions { get; }
        IPromotionTransactionRepository PromotionTransactions { get; }
        ICategoryRepository Categories { get; }
        IOTPRepository OTPs { get; }
        ITicketRankRepository TicketRanks { get; }
        IWorkshopImageRepository WorkshopImage { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}

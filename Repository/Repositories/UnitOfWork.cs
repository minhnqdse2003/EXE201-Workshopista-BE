using Repository.Interfaces;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Exe201WorkshopistaContext _context;

        public UnitOfWork(Exe201WorkshopistaContext context)
        {
            _context = context;
            Users = new UserRepository(_context);
            Workshops = new WorkshopRepository(_context);
            Organizers = new OrganizerRepository(_context);
            Categories = new CategoryRepository(_context);
            OTPs = new OTPRepository(_context);
            TicketRanks = new TicketRankRepository(_context);
            Orders = new OrderRepository(_context);
            OrderDetails = new OrderDetailsRepository(_context);
            Tickets = new TicketRepository(_context);
            Transactions = new TransactionRepository(_context);
            Subscriptions = new SubscriptionRepository(_context);
            Commissions = new CommissionRepository(_context);
            Promotions = new PromotionRepository(_context);
            SubscriptionTransactions = new SubscriptionTransactionRepository(_context);
            CommissionTransactions = new CommissionTransactionRepository(_context);
            PromotionTransactions = new PromotionTransactionRepository(_context);
            Reviews = new ReviewRepository(_context);
            WorkshopImage = new WorkshopImageRepository(_context);
        }

        public IUserRepository Users { get; private set; }
        public IOrderRepository Orders { get; private set; }
        public IWorkshopRepository Workshops { get; private set; }

        public IOrganizerRepository Organizers { get; private set; }

        public ICategoryRepository Categories { get; private set; }

        public IOTPRepository OTPs { get; private set; }

        public ITicketRankRepository TicketRanks { get; private set; }
        public IOrderDetailsRepository OrderDetails { get; private set; }
        public ITicketRepository Tickets { get; private set; }

        public ITransactionRepository Transactions { get; private set; }

        public ISubscriptionRepository Subscriptions { get; private set; }

        public ICommissionRepository Commissions { get; private set; }

        public IPromotionRepository Promotions { get; private set; }

        public ISubscriptionTransactionRepository SubscriptionTransactions { get; private set; }

        public ICommissionTransactionRepository CommissionTransactions { get; private set; }

        public IPromotionTransactionRepository PromotionTransactions { get; private set; }
        public IWorkshopImageRepository WorkshopImage { get; private set; }

        public IReviewRepository Reviews { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

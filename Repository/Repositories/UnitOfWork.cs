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
        }

        public IUserRepository Users { get; private set; }

        public IWorkshopRepository Workshops { get; private set; }
        public IOrganizerRepository Organizers { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

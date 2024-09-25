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

                IOrganizerRepository Organizers { get; }

                IWorkshopRepository Workshops { get; }
                ICategoryRepository Categories { get; }
                int Complete();
        }
}

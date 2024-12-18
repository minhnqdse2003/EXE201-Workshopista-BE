﻿using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IPromotionTransactionRepository : IGenericRepository<PromotionTransaction>
    {
        IQueryable<PromotionTransaction> GetQuery();
    }
}

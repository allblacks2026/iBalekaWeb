using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using iBalekaWeb.Models;
using iBalekaWeb.Data.Configurations;

namespace iBalekaWeb.Data.Infastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private iBalekaDBContext DbContext;

        public UnitOfWork(iBalekaDBContext dbContext)
        {
            DbContext = dbContext;
        }


        public void Commit()
        {
            DbContext.Commit();
        }
    }
}

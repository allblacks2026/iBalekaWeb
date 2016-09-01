﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore.Infrastructure;
using iBalekaWeb.Data.Configurations;

namespace iBalekaWeb.Data.Infastructure
{
    public class DbFactory:Disposable,IDbFactory, IDbContextFactory<iBalekaDBContext>
    {
        iBalekaDBContext dbContext;
        
  
        public iBalekaDBContext Create(DbContextFactoryOptions opt)
        {
            var builder = new DbContextOptionsBuilder<iBalekaDBContext>();
            builder.UseSqlServer("Data Source = tcp:ibaleka.database.windows.net, 1433; Initial Catalog = iBalekaDB; User Id = iBalekaAdmin@ibaleka; Password = AllBlacks2026; MultipleActiveResultSets = true;");
            return new iBalekaDBContext(builder.Options);
        }
        public iBalekaDBContext Init(DbContextFactoryOptions opt)
        {
            return dbContext ?? (dbContext = Create(opt));
        }
        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();           
        }
    }
}

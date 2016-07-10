using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using iBalekaWeb.Data.Configurations;

namespace iBalekaWeb.Data.Infastructure
{
    public interface IDbFactory:IDisposable
    {
        iBalekaDBContext Init(DbContextFactoryOptions opt);
    }
}

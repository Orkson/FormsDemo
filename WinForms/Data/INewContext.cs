using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.Data
{
    public interface INewContext
    {
        public DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }
    }
}

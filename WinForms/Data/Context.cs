using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.Data
{
    internal class Context : DbContext
    {
        public Context() : base("AdventureWorks2019")
        {

        }
        public DbSet<SalesOrderHeader> SalesOrderHeader { get; set; }
    }

}

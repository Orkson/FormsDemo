using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms.Data
{
    public class NewContext : DbContext, INewContext
    {
        public NewContext() : base("AdventureWorks2019")
        {
            
        }
        public DbSet<SalesOrderHeader>? SalesOrderHeaders { get; set; }
    }

}

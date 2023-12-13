using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinForms
{
    [Table("SalesOrderHeader", Schema = "Sales")]
    public class SalesOrderHeader
    {
        [Key]
        public int SalesPersonID { get; set; }
        public decimal TotalDue { get; set; }
        public int? CurrencyRateID { get; set; }
        public DateTime OrderDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyStore.BLL
{
    class BillBLL
    {
        public int id { get; set; }
        public string invoice_no { get; set; }
        public decimal grandTotal { get; set; }
        public DateTime transaction_date { get; set; }
        public string name { get; set; }
        public string Pname { get; set; }
        public string description { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
        public decimal rate { get; set; }
        public decimal qty { get; set; }
    }
}

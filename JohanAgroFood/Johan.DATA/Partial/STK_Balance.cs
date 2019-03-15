using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.DATA
{
    public partial class STK_Balance
    {
        public DateTime date { get; set; }
        public string productName { get; set; }
        public string stockName { get; set; }
        public int targetStockId { set; get; }
        //public double? bagWeight { get; set; }
        public int? serial { get; set; }
        public double? millCost { get; set; }
    }             
}

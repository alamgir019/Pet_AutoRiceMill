//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Johan.DATA
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblBuy
    {
        public tblBuy()
        {
            this.tblCostingSources = new HashSet<tblCostingSource>();
            this.tblPayments = new HashSet<tblPayment>();
            this.STK_tblPaddy = new HashSet<STK_tblPaddy>();
            this.BagTransactions = new HashSet<BagTransaction>();
            this.tblDues = new HashSet<tblDue>();
        }
    
        public long ID { get; set; }
        public int partyId { get; set; }
        public int stockId { get; set; }
        public int productId { get; set; }
        public System.DateTime date { get; set; }
        public int noOfBag { get; set; }
        public double quantityPerBag { get; set; }
        public byte unit { get; set; }
        public double price { get; set; }
        public string truckNumber { get; set; }
    
        public virtual tblParty tblParty { get; set; }
        public virtual tblProduct tblProduct { get; set; }
        public virtual ICollection<tblCostingSource> tblCostingSources { get; set; }
        public virtual ICollection<tblPayment> tblPayments { get; set; }
        public virtual ICollection<STK_tblPaddy> STK_tblPaddy { get; set; }
        public virtual ICollection<BagTransaction> BagTransactions { get; set; }
        public virtual ICollection<tblDue> tblDues { get; set; }
    }
}

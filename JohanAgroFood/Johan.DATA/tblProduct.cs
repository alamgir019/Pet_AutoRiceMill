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
    
    public partial class tblProduct
    {
        public tblProduct()
        {
            this.STK_Balance = new HashSet<STK_Balance>();
            this.tblBuys = new HashSet<tblBuy>();
            this.tblParties = new HashSet<tblParty>();
            this.tblSells = new HashSet<tblSell>();
            this.tblProductPrices = new HashSet<tblProductPrice>();
            this.STK_Transaction = new HashSet<STK_Transaction>();
            this.STK_tblPaddy = new HashSet<STK_tblPaddy>();
        }
    
        public int ID { get; set; }
        public string productName { get; set; }
        public Nullable<int> parentId { get; set; }
    
        public virtual ICollection<STK_Balance> STK_Balance { get; set; }
        public virtual ICollection<tblBuy> tblBuys { get; set; }
        public virtual ICollection<tblParty> tblParties { get; set; }
        public virtual ICollection<tblSell> tblSells { get; set; }
        public virtual ICollection<tblProductPrice> tblProductPrices { get; set; }
        public virtual ICollection<STK_Transaction> STK_Transaction { get; set; }
        public virtual ICollection<STK_tblPaddy> STK_tblPaddy { get; set; }
    }
}

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
    
    public partial class tblIncomeSource
    {
        public tblIncomeSource()
        {
            this.tblPayables = new HashSet<tblPayable>();
            this.tblSells = new HashSet<tblSell>();
        }
    
        public long ID { get; set; }
        public Nullable<int> partyId { get; set; }
        public double amount { get; set; }
        public string sourceName { get; set; }
        public string description { get; set; }
        public System.DateTime date { get; set; }
        public int srcDescId { get; set; }
        public Nullable<int> isDue { get; set; }
    
        public virtual tblCommonElement tblCommonElement { get; set; }
        public virtual tblParty tblParty { get; set; }
        public virtual ICollection<tblPayable> tblPayables { get; set; }
        public virtual ICollection<tblSell> tblSells { get; set; }
    }
}

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
    
    public partial class tblCommonElement
    {
        public tblCommonElement()
        {
            this.tblCostingSources = new HashSet<tblCostingSource>();
            this.tblIncomeSources = new HashSet<tblIncomeSource>();
            this.tblParties = new HashSet<tblParty>();
        }
    
        public int ID { get; set; }
        public string elementName { get; set; }
        public byte elementCode { get; set; }
    
        public virtual ICollection<tblCostingSource> tblCostingSources { get; set; }
        public virtual ICollection<tblIncomeSource> tblIncomeSources { get; set; }
        public virtual ICollection<tblParty> tblParties { get; set; }
    }
}
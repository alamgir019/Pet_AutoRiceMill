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
    
    public partial class STK_tblPaddy
    {
        public int ID { get; set; }
        public int stockId { get; set; }
        public int productId { get; set; }
        public long paddRcvId { get; set; }
        public double sackWeight { get; set; }
        public double sackQuantity { get; set; }
        public System.DateTime createDate { get; set; }
    
        public virtual STK_tblStock STK_tblStock { get; set; }
        public virtual tblProduct tblProduct { get; set; }
        public virtual tblBuy tblBuy { get; set; }
    }
}

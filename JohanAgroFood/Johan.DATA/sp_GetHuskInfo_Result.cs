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
    
    public partial class sp_GetHuskInfo_Result
    {
        public System.DateTime date { get; set; }
        public string productName { get; set; }
        public string stockName { get; set; }
        public int noOfBag { get; set; }
        public double quantityPerBag { get; set; }
        public Nullable<double> paidAmount { get; set; }
        public string unit { get; set; }
        public Nullable<double> unitPrice { get; set; }
        public Nullable<double> transportCost { get; set; }
        public Nullable<double> totalPrice { get; set; }
    }
}

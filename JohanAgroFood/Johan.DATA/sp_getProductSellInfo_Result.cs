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
    
    public partial class sp_getProductSellInfo_Result
    {
        public long ID { get; set; }
        public int partyId { get; set; }
        public int productId { get; set; }
        public System.DateTime date { get; set; }
        public int noOfBag { get; set; }
        public double quantity { get; set; }
        public int unit { get; set; }
        public Nullable<double> unitPrice { get; set; }
        public string partyName { get; set; }
        public string productName { get; set; }
        public Nullable<int> stockId { get; set; }
        public string stockName { get; set; }
        public Nullable<double> amount { get; set; }
        public Nullable<double> transportCost { get; set; }
        public string description { get; set; }
        public string truckNumber { get; set; }
        public Nullable<long> incSrcId { get; set; }
    }
}

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
    
    public partial class tblLoanar
    {
        public int ID { get; set; }
        public string name { get; set; }
        public Nullable<long> contactNo { get; set; }
        public string area { get; set; }
        public string district { get; set; }
        public double amount { get; set; }
        public double interest { get; set; }
        public System.DateTime createDate { get; set; }
        public Nullable<System.DateTime> lastUpdate { get; set; }
    }
}

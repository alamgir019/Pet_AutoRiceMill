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
    
    public partial class OpeningBalance
    {
        public long ID { get; set; }
        public int partyId { get; set; }
        public double balance { get; set; }
        public System.DateTime date { get; set; }
    
        public virtual tblParty tblParty { get; set; }
    }
}

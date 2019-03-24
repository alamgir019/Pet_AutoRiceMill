using System;
using OcodyAutoRiceMill.Data.Model;

namespace OcodyAutoRiceMill.Queries.Models
{
    public class UserWithToken
    {
        public string Token { get; set; }
        public User User { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}
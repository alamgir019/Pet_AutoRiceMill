﻿using System.Collections.Generic;

namespace OcodyAutoRiceMill.Api.Models.Users
{
    public class UserModel
    {
        public UserModel()
        {
            Roles = new string[0];
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string[] Roles { get; set; }
    }
}
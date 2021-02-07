using System;
using System.Collections.Generic;
using System.Text;

namespace Practice4
{
    class AdminUser : User
    {
        public AdminUser(string username, string password) : base (username, password)
        {

        }

        public override string Privilege()
        {
            return "Admin";
        }
    }
}

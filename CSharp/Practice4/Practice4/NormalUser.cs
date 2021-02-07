using System;
using System.Collections.Generic;
using System.Text;

namespace Practice4
{
    class NormalUser : User
    {
        public NormalUser(string username, string password) : base(username, password)
        {

        }

        public override string Privilege()
        {
            return "Normal";
        }
    }
}

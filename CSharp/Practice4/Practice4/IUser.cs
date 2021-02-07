using System;
using System.Collections.Generic;
using System.Text;

namespace Practice4
{
    interface IUser
    {
        public string Username { get; }
        private string Password { get { return null; } }

        public void ChangePassword(string oldPassword, string newPassword);
        public string Privilege();
    }
}

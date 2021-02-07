using System;
using System.Collections.Generic;
using System.Text;

namespace Practice4
{
    abstract class User
    {
        private string username;
        private string password;

        public string Username { get { return username; } }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public void ChangePassword(string oldPassword, string newPassword)
        {
            if (oldPassword == password)
            {
                password = newPassword;
            }
        }

        public override string ToString()
        {
            return $"{username} {password}";
        }

        public abstract string Privilege();
    }

    enum UserPrivileges
    {
        Normal,
        Restricted,
        Moderator,
        Administrator
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace Practice4
{
    struct NormalUserStruct : IUser
    {
        private string username;
        private string password;
        public string Username { get { return username; } }

        public NormalUserStruct(string username, string password)
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

        public string Privilege()
        {
            return "Normal";
        }

        public override string ToString()
        {
            return username + " " + password;
        }
    }

    struct AdminUserStruct : IUser
    {
        private string username;
        private string password;
        public string Username { get { return username; } }


        public AdminUserStruct(string username, string password)
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

        public string Privilege()
        {
            return "Admin";
        }

        public override string ToString()
        {
            return username + " " + password;
        }
    }
}


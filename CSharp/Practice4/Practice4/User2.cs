using System;
using System.Collections.Generic;
using System.Text;

namespace Practice4
{
    struct User2
    {
        private string username;
        private string password;
        private UserPrivileges privileges;

        public string Username { get { return username; } }
        public UserPrivileges Privileges { get { return privileges; } }

        public User2(string username, string password, UserPrivileges privileges)
        {
            this.username = username;
            this.password = password;
            this.privileges = privileges;
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
    }
}

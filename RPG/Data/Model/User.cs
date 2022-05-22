using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Data.Model
{
    internal class User
    {
        string Name = "";
        string Password = ""; //Encrypted with EncryptDecrypt
        int? Id = -1;
        int? RoleId = -1;

        public User(int Id,string Name, string Password, int RoleId)
        {
            this.Name = Name;
            this.Password = Password;
            this.Id = Id;
            this.RoleId = RoleId;
        }

        public void SetName(string name)
        {
            this.Name = name;
        }

        public void SetPassword(string Password)
        {
            this.Password = Password;
        }

        public void setId(int id)
        {
            this.Id = id;
        }

        public void setRoleid(int roleid)
        {
            this.RoleId = roleid;
        }

        public string getName()
        {
            return this.Name;
        }

        public string getPassword()
        {
            return this.Password;
        }

        public int? getId()
        {
            return this.Id;
        }

        public int? getRoleId()
        {
            return this.RoleId;
        }

    }
}

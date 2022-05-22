using RPG.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG.Data
{
    internal class RegLogMgr
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");
        public static  User currentUser;

        public void Register(string name, string password)
        {
            EncryptDecrypt encrypt = new EncryptDecrypt();
            if (name != null && password != null)
            {
                conn.Open();

                string query = "SELECT * FROM USERS WHERE Name = @name";
                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@name", name);
                    var reader = command.ExecuteReader();

                    

                    if (reader.HasRows)
                    {
                        MessageBox.Show("Select different name please!");
                        return;
                    }

                    reader.Close();
                    conn.Close();

                }
                password = encrypt.Encrypt(password);

                SqlCommand cmd = new SqlCommand("AddUser", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Password", password);
                cmd.Parameters.AddWithValue("@RoleId", "3");
                conn.Open();
                int i = cmd.ExecuteNonQuery();

                conn.Close();

                if (i != 0)
                {
                    MessageBox.Show(i + "Data Saved");
                }

            }

        }

        public bool login(string name, string password)
        {
            conn.Open();
            if (name == null || password == null)
            {
                MessageBox.Show("Fill all data");
                return false;
            }
            string query = "SELECT Password, Id, Roleid FROM USERS WHERE Name = @name";
            string data_pass = "";
            int data_id = -1;
            int data_role = -1;
            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@name", name);
                var reader = command.ExecuteReader();


                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        data_pass = reader.GetString(0);
                        data_id = reader.GetInt32(1);
                        data_role = reader.GetInt32(2);
                    }
                    EncryptDecrypt encrypt = new EncryptDecrypt();
                    string s2 = encrypt.Encrypt(password);

                    if (s2 == data_pass)
                    {
                        MessageBox.Show("Logged succesfully");
                        currentUser = new User(data_id,name, data_pass,data_role);

                        string[] cuData = { name, data_pass, data_id.ToString(), data_role.ToString() };

                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Wrong password or name");
                    }
                }
                else
                {
                    MessageBox.Show("Nope XD");
                }

                reader.Close();
                conn.Close();
                return false;
            }

        }

    }   
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG.Data
{
    internal class MyAccountMgr
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");

        public MyAccountMgr() {}

        public void printName(Label name_label)
        {
            name_label.Text = RegLogMgr.currentUser.getName();
        }

        public void newPassword(TextBox pass)
        {
            conn.Open();
            EncryptDecrypt encrypt = new EncryptDecrypt();
            if (pass.Text == null)
            {
                MessageBox.Show("Please insert new password!");
                return;
            }
            else
            {
                string newpass = encrypt.Encrypt(pass.Text);
                string query = "UPDATE USERS SET Password = @passw WHERE Name = @name";
                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@passw", newpass);
                    command.Parameters.AddWithValue("@name", RegLogMgr.currentUser.getName());
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Password changed successfully");
                    }
                    reader.Close();
                    conn.Close();
                }
            }
        }
    }
}

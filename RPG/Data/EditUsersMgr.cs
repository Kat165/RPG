using RPG.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace RPG.Data
{
    internal class EditUsersMgr
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");
        List<User> users = new List<User>();
        EncryptDecrypt decrypt = new EncryptDecrypt();
        public void fillData(DataGridView view)
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Name");
            dt.Columns.Add("Password Decrypted");
            dt.Columns.Add("RoleId");
            conn.Open();
            string querry = "SELECT * FROM Users";
            using (SqlCommand cmd = new SqlCommand(querry, conn))
            {
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        users.Add(new User(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                        string password = reader.GetString(2);
                        password = decrypt.Decrypt(password);
                        dt.Rows.Add(reader.GetInt32(0), reader.GetString(1), password, reader.GetInt32(3));
                    }
                }
                view.DataSource = dt;  
                reader.Close();
                conn.Close();
            }
            view.Columns["Id"].ReadOnly = true;
        }

        public void updateUsers(DataGridView view)
        {
            List<User> users2 = new List<User>();

            string[] xx = new string[4];

            for (int rows = 0; rows < users.Count; rows++)
            {
                int i = 0;
                for (int col = 0; col < 4; col++)
                {
                    xx[i] = view.Rows[rows].Cells[col].Value.ToString();
                    i++;
                }
                users2.Add(new User(int.Parse(xx[0]), xx[1], xx[2], int.Parse(xx[3])));
            }

            foreach (User user in users2)
            {
                string pass = decrypt.Encrypt(user.getPassword());
                conn.Open();
                string querry = "UPDATE Users SET Name = @Name, Password = @Pass , RoleId = @Role WHERE Id = @Id";
                using(var cmd = new SqlCommand(querry, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", user.getName());
                    cmd.Parameters.AddWithValue("@Pass", pass);
                    cmd.Parameters.AddWithValue("@Role", user.getRoleId());
                    cmd.Parameters.AddWithValue("@Id", user.getId());
                    int x = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }
        
        public void deleteRole(DataGridView view)
        {
            //Dopisać usuwanie creatures
            List<int> roles = new List<int>();

            foreach (User user in users)
            {
                roles.Add((int)user.getRoleId());
            }

            roles = roles.Distinct().ToList();

            string c = Interaction.InputBox("Which role delete?", " ", "There is no way back");

            int toDel = int.Parse(c);

            if (!roles.Contains(toDel))
            {
                MessageBox.Show("We don't have this role");
                return;
            }

            string querry = "DELETE FROM Users WHERE RoleId = @RId";
            conn.Open();
            using (var cmd = new SqlCommand(querry, conn))
            {
                cmd.Parameters.AddWithValue("@RId", toDel);
                
                int x = cmd.ExecuteNonQuery();
                if (x > 0)
                    MessageBox.Show("Deleted!");
                conn.Close();
            }
            fillData(view);
        }

    }
}

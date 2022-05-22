using RPG.Data.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG.Data
{
    internal class EditCreaturesMgr
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");
        Creature selectedCreature;

        public void showBtn(Label label, ComboBox comboBox, Label label1, ComboBox comboBox1, Label label2, ComboBox comboBox2)
        {
            if(RegLogMgr.currentUser.getId() == 3)
            {
                label.Enabled = false;
                label.Visible = false;

                comboBox.Enabled = false;
                comboBox.Visible = false;

                label1.Enabled = false;
                label1.Visible = false;

                comboBox1.Enabled = false;
                comboBox1.Visible = false;

                label2.Enabled = false;
                label2.Visible = false;

                comboBox2.Enabled = false;
                comboBox2.Visible = false;

            }
        }

        public void ShowData(DataGridView data)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Archclass");
            dt.Columns.Add("Class");
            dt.Columns.Add("Race");
            dt.Columns.Add("Name");
            dt.Columns.Add("UserId");
            dt.Columns.Add("Mana");
            if (RegLogMgr.currentUser.getRoleId() == 3)
            {
                string querry = "SELECT * FROM Creatures WHERE UserId = @Id";
                conn.Open();
                using(var command = new SqlCommand(querry, conn))
                {
                    command.Parameters.AddWithValue("@Id", RegLogMgr.currentUser.getId());
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dt.Rows.Add(reader.GetInt32(0),reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                        }
                    }

                    reader.Close();
                    conn.Close();
                }
                data.DataSource = dt;

            }
            else
            {

            }
        }

        public void showSpecData(int selectedId, DataGridView view)
        {
            string querry0 = "SELECT Class FROM Creatures WHERE Id = @ID";
            conn.Open();
            string selClass = "";
            using (var command = new SqlCommand(querry0, conn))
            {
                command.Parameters.AddWithValue("@ID", selectedId);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        selClass = reader.GetString(0);
                    }
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                    return;
                }
                reader.Close();
                conn.Close();
            }



            DataTable dataTable = new DataTable();
            conn.Open();
            int i = 0;
            string querry2 = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @class";
            using (var command = new SqlCommand(querry2, conn))
            {
                command.Parameters.AddWithValue("@class", selClass);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        i++;
                        dataTable.Columns.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                conn.Close();
            }
            dataTable.Rows.Add();
            view.DataSource = dataTable;

            var querry3 = string.Format("SELECT * FROM {0} WHERE Id = @ID", selClass);
            conn.Open();
            using (var command = new SqlCommand(querry3, conn))
            {
                command.Parameters.AddWithValue("@ID", selectedId);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for(int j = 0; j < i; j++)
                        {
                            view.Rows[0].Cells[j].Value = reader.GetValue(j);
                        }
                    }
                }
                reader.Close();
                conn.Close();
            }
        }
    }
}

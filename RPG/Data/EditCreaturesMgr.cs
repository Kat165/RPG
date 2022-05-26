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
    internal class EditCreaturesMgr
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..")) + "\\Database.mdf" + "; Integrated Security=True");
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
            string querry = "SELECT * FROM Creatures WHERE UserId = @Id";
            conn.Open();
            using (var command = new SqlCommand(querry, conn))
            {
                command.Parameters.AddWithValue("@Id", RegLogMgr.currentUser.getId());
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt32(6));
                    }
                }

                reader.Close();
                conn.Close();
            }
            data.DataSource = dt;
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

        public void EditAll(DataGridView view, string class_)
        {
            DataTable dataTable = new DataTable();
            conn.Open();
            int i = 0;
            string querry2 = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @class";
            using (var command = new SqlCommand(querry2, conn))
            {
                command.Parameters.AddWithValue("@class", class_);
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


            conn.Open();
            var querry = string.Format("SELECT COUNT(Id) FROM {0}",class_);
            int count = 0;
            using (var command = new SqlCommand(querry, conn))
            {
                command.Parameters.AddWithValue("@class", class_);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        count = reader.GetInt32(0);
                    }
                }
                reader.Close();
                conn.Close();
            }
            
            StringBuilder sb = new StringBuilder();
            var querry3 = string.Format("SELECT * FROM {0}", class_);
            conn.Open();
            var dataAdapter = new SqlDataAdapter(querry3, conn);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            conn.Close();
            view.DataSource = ds.Tables[0];

        }

        public void fillClassComboBox(ComboBox box, ComboBox box1, ComboBox box2)
        {
            conn.Open();
            string arcc = "SELECT Class FROM Creatures";

            List<string> list = new List<string>();

            using (var command = new SqlCommand(arcc, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetString(0));
                    }
                }

                reader.Close();
                conn.Close();
            }



            list = list.Distinct().ToList();
            foreach (string f in list)
            {
                box.Items.Add(f);
            }

            conn.Open();
            string arcarc = "SELECT Archclass FROM Creatures";

            List<string> alist = new List<string>();

            using (var command = new SqlCommand(arcarc, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        alist.Add(reader.GetString(0));
                    }
                }

                reader.Close();
                conn.Close();
            }



            alist = alist.Distinct().ToList();
            foreach (string f in alist)
            {
                box1.Items.Add(f);
            }

            conn.Open();
            string race = "SELECT Race FROM Creatures";

            List<string> rlist = new List<string>();

            using (var command = new SqlCommand(race, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        rlist.Add(reader.GetString(0));
                    }
                }

                reader.Close();
                conn.Close();
            }



            rlist = rlist.Distinct().ToList();
            foreach (string f in rlist)
            {
                box2.Items.Add(f);
            }
        }

        public void saveData(string Name, string value, DataGridView view)
        {
            if(EditCreatures.selcid == 0)
            {
                MessageBox.Show("Pick first!");
                return;
            }
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < view.ColumnCount; i++)
            {
                builder.Append(view.Columns[i].Name + ",");
            }
            builder.Length--;

            if(string.Equals(Name,"Id") || string.Equals(Name, "UserId"))
            {
                MessageBox.Show("This can't be changed");
                return;
            }

            string[] values = builder.ToString().Split(',');
            foreach (string xx in values)
            {
                if (Name.Contains(xx))
                {
                    if (string.Equals(Name, "Name"))
                    {
                        conn.Open();
                        string querry = "UPDATE Creatures SET Name = @Name WHERE Id = @ID";
                        using (var cmd = new SqlCommand(querry, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", value);
                            cmd.Parameters.AddWithValue("@ID", EditCreatures.selcid);
                            int x = cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        conn.Open();
                        string class_ = "";
                        string querry2 = "SELECT Class FROM Creatures WHERE Id = @id";
                        using (var cmd = new SqlCommand(querry2, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", EditCreatures.selcid);
                            var reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    class_ = reader.GetString(0);
                                }
                            }
                            conn.Close();
                        }

                        conn.Open();
                        var querry4 = string.Format("UPDATE {0} SET Name = @Name WHERE Id = @ID", class_);
                        using (var cmd = new SqlCommand(querry4, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", value);
                            cmd.Parameters.AddWithValue("@ID", EditCreatures.selcid);
                            int x = cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        return;
                    }
                    else if (string.Equals(Name, "Mana"))
                    {
                        conn.Open();
                        string querry3 = "UPDATE Creatures SET Mana = @Mana";
                        using (var cmd = new SqlCommand(querry3, conn))
                        {
                            cmd.Parameters.AddWithValue("@Mana", value);
                            int x = cmd.ExecuteNonQuery();
                            conn.Close();
                        }

                        conn.Open();
                        string class_ = "";
                        string querry2 = "SELECT Class FROM Creatures WHERE Id = @id";
                        using (var cmd = new SqlCommand(querry2, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", EditCreatures.selcid);
                            var reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    class_ = reader.GetString(0);
                                }
                            }
                            conn.Close();
                        }

                        conn.Open();
                        var querry4 = string.Format("UPDATE {0} SET Mana = @Mana", class_);
                        using (var cmd = new SqlCommand(querry4, conn))
                        {
                            cmd.Parameters.AddWithValue("@Mana", value);
                            int x = cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        return;
                    }
                    else
                    {
                        conn.Open();
                        string class_ = "";
                        string querry2 = "SELECT Class FROM Creatures WHERE Id = @id";
                        using (var cmd = new SqlCommand(querry2, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", EditCreatures.selcid);
                            var reader = cmd.ExecuteReader();
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    class_ = reader.GetString(0);
                                }
                            }
                            conn.Close();
                        }

                        conn.Open();
                        var querry4 = string.Format("UPDATE {0} SET @col = @val", class_);
                        using (var cmd = new SqlCommand(querry4, conn))
                        {
                            cmd.Parameters.AddWithValue("@col", Name);
                            cmd.Parameters.AddWithValue("@val", value);
                            int x = cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        return;
                    }

                    MessageBox.Show("We don't have column " + Name);
                    return;
                }
            }


            
        }
    }
}

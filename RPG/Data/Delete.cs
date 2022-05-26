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
    internal class Delete
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..")) + "\\Database.mdf" + "; Integrated Security=True");
        public void deleteClass(string klassa)
        {
            var querry = string.Format("DELETE FROM {0}", klassa);
            conn.Open();
            using (var command = new SqlCommand(querry, conn))
            {
                var reader = command.ExecuteReader();

                reader.Close();
                conn.Close();
            }

            var querryd = string.Format("DROP TABLE IF EXISTS {0}", klassa);
            conn.Open();
            using (var command = new SqlCommand(querryd, conn))
            {
                var reader = command.ExecuteReader();

                reader.Close();
                conn.Close();
            }

            string querry2 = "DELETE FROM Creatures WHERE Class = @class";
            conn.Open();
            using (var command = new SqlCommand(querry2, conn))
            {
                command.Parameters.AddWithValue("@class", klassa);
                var reader = command.ExecuteReader();

                reader.Close();
                conn.Close();
            }

            MessageBox.Show("Data deleted");
        }

        public void deleteRace(string klassa, string race)
        {
            var querry = string.Format("DELETE FROM {0} WHERE Race = @race", klassa);
            conn.Open();
            using (var command = new SqlCommand(querry, conn))
            {
                command.Parameters.AddWithValue("@race", race);
                var reader = command.ExecuteReader();

                reader.Close();
                conn.Close();
            }

            string querry2 = "DELETE FROM Creatures WHERE Race = @race";
            conn.Open();
            using (var command = new SqlCommand(querry2, conn))
            {
                command.Parameters.AddWithValue("@race", race);
                var reader = command.ExecuteReader();

                reader.Close();
                conn.Close();
            }
            MessageBox.Show("Data deleted");
        }

        public void deleteArchclass(string arc)
        {
            List<string> classes = new List<string>();

            string querry = "SELECT Class FROM Creatures WHERE Archclass = @arc";

            conn.Open();
            using (var command = new SqlCommand(querry, conn))
            {
                command.Parameters.AddWithValue("@arc", arc);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        classes.Add(reader.GetString(0));
                    }
                }

                reader.Close();
                conn.Close();
            }

            classes = classes.Distinct().ToList();

            foreach (string c in classes)
            {
                deleteClass(c);
            }
        }

        //public void checkCombobox(ComboBox )

        public void delSth(ComboBox arc, ComboBox cl, ComboBox ra)
        {
            if (arc.SelectedIndex == -1 && cl.SelectedIndex == -1 && ra.SelectedIndex == -1)
            {
                MessageBox.Show("Pick sth to delete!");
                return;
            }
            if (arc.SelectedIndex != -1 && cl.SelectedIndex == -1 && ra.SelectedIndex == -1)
            {
                deleteArchclass(arc.SelectedItem.ToString());
                return;
            }
            if (arc.SelectedIndex == -1 && cl.SelectedIndex != -1 && ra.SelectedIndex == -1)
            {
                deleteClass(cl.SelectedItem.ToString());
                return;
            }
            if (arc.SelectedIndex == -1 && cl.SelectedIndex == -1 && ra.SelectedIndex != -1)
            {
                string c = "";
                conn.Open();
                string querry = "SELECT Class FROM Creatures WHERE Race = @r";
                using (var command = new SqlCommand(querry, conn))
                {
                    command.Parameters.AddWithValue("@r", ra.SelectedItem.ToString());
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            c = reader.GetString(0);
                        }
                    }

                    reader.Close();
                    conn.Close();
                }
                deleteRace(c,ra.SelectedItem.ToString());
                return;
            }
        }

        public void delCtr(int id)
        {
            if(id == 0)
            {
                MessageBox.Show("Pick sth to delete!");
                return;
            }
            conn.Open();
            string querry = "SELECT Class FROM Creatures WHERE Id = @id";
            string c = "";
            using (var command = new SqlCommand(querry, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        c = reader.GetString(0);
                    }
                }

                reader.Close();
                conn.Close();
            }

            conn.Open();
            var q = string.Format("DELETE FROM Creatures WHERE ID = @id");
            using (var command = new SqlCommand(q, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                

                reader.Close();
                conn.Close();
            }

            conn.Open();
            int count = -1;
            var q3 = string.Format("SELECT COUNT(Id) FROM {0}", c);
            using (var command = new SqlCommand(q3, conn))
            {
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

            conn.Open();
            var q2 = string.Format("DELETE FROM {0} WHERE ID = @id",c);
            using (var command = new SqlCommand(q2, conn))
            {
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();


                reader.Close();
                conn.Close();
            }

            if(count < 2)
            {
                var querryd = string.Format("DROP TABLE IF EXISTS {0}", c);
                conn.Open();
                using (var command = new SqlCommand(querryd, conn))
                {
                    var reader = command.ExecuteReader();

                    reader.Close();
                    conn.Close();
                }
            }

            MessageBox.Show("Data deleted");
        }
    }
}

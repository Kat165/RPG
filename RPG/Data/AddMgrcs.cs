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
    internal class AddMgrcs
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");
        Nerf nerf = new Nerf();

        public static List<string> arclist = new List<string>();
        public static List<string> clist = new List<string>();
        public static List<string> racelist = new List<string>();

        public static string selArch;
        public static string selClass;
        public static string selRace;

        List<List<string>> nerfList = new List<List<string>>();

        int maxId = new int();
        public AddMgrcs() {         }


        public void fillComboBox(ComboBox archclass, ComboBox class_, ComboBox race)
        {
            conn.Open();
            string arcc = "SELECT Archclass, Class, Race FROM Creatures";


            using (var command = new SqlCommand(arcc, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        arclist.Add(reader.GetString(0));
                        clist.Add(reader.GetString(1));
                        racelist.Add(reader.GetString(2));
                    }
                }

                reader.Close();
                conn.Close();
            }

            arclist = arclist.Distinct().ToList();
            foreach (string f in arclist)
            {
                archclass.Items.Add(f);
            }


            clist = clist.Distinct().ToList();
            foreach (string f in clist)
            {
                class_.Items.Add(f);
            }


            racelist = racelist.Distinct().ToList();
            foreach (string f in racelist)
            {
                race.Items.Add(f);
            }
        }

        public void saveStuff(string archclass, string class_, string race)
        {
            selArch = archclass;
            selRace = race;
            selClass = class_;
        }
        public void FillCollumnNames(DataGridView view)
        {
            DataTable dataTable = new DataTable();
            conn.Open();
            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @class";
            using (var command = new SqlCommand(query, conn))
            {
                command.Parameters.AddWithValue("@class", selClass);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dataTable.Columns.Add(reader.GetString(0));
                    }
                }
                reader.Close();
                conn.Close();
            }

            conn.Open();
            var query2 = string.Format("SELECT max(Id) FROM Creatures");
            using (var command2 = new SqlCommand(query2, conn))
            {
                var reader = command2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        maxId = reader.GetInt32(0)+1;
                    }
                }
                reader.Close();
                conn.Close();
            }

            //dataTable.Columns["Id"].ColumnMapping = MappingType.Hidden;
            //dataTable.Columns["UserId"].ColumnMapping = MappingType.Hidden;
            dataTable.Rows.Add();
            dataTable.Rows[0]["Id"] = maxId;
            dataTable.Rows[0]["Race"] = selRace;
            dataTable.Rows[0]["UserId"] = RegLogMgr.currentUser.getId();
            view.DataSource = dataTable;
            view.Columns["Id"].Visible = false;
            view.Columns["UserId"].Visible = false;
        }

        public void saveOldNewCreature(DataGridView dataGridView1)
        {
            foreach (DataGridViewRow rw in dataGridView1.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        MessageBox.Show("Fill all cells");
                        return;
                    }
                }
            }

            if(!int.TryParse(dataGridView1.Rows[0].Cells["Mana"].Value.ToString(),out int r))
            {
                MessageBox.Show("Mana must be an integer!");
                return;
            }

            if(int.Parse(dataGridView1.Rows[0].Cells["Mana"].Value.ToString()) < 0)
            {
                MessageBox.Show("Mana must be greater or equal 0");
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    sb.Append(cell.Value);
                    sb.Append(",");
                }
            }
            sb.Length--;

            string[] values = sb.ToString().Split(',');


            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            int k = 0;
            foreach (string value in values)
            {
                stringBuilder1.Append(value);
                stringBuilder.Append("@v" + k + ",");
                k++;
            }
            stringBuilder.Length--;

            //int d = 0;
            //conn.Open();
            //string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = @class";
            //using (var command = new SqlCommand(query, conn))
            //{
            //    command.Parameters.AddWithValue("@class", selClass);
            //    var reader = command.ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            nerfList.Add(new List<string> { reader.GetString(0),values[d]});
            //            d++;
            //        }
            //    }
            //    reader.Close();
            //    conn.Close();
            //}

            //foreach (List<string> subList in nerfList)
            //{
            //    foreach (string item in subList)
            //    {
            //        //nerf.nerf_this(selClass,item)
            //        MessageBox.Show(item);
            //    }
            //}
            int d = 0;
            foreach (string value in values)
            {
                if(d == 0)
                {
                    d++;
                    continue;
                    if(d == values.Length)
                    {
                        d++;
                        continue;
                    }
                }
                //values[d] = nerf.nerf_this(selClass, dataGridView1.Columns[d].Name, value);
                d++;
            }
            conn.Open();
            var querrrrr = string.Format("SELECT MAX(Mana) FROM {0}", selClass);
            int maxmana = -1;
            using (var cmd = new SqlCommand(querrrrr, conn))
            {
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        maxmana = int.Parse(reader.GetString(0));
                    }
                }
                reader.Close();
                conn.Close();
            }

            conn.Open();
            values[values.Length - 1] = RegLogMgr.currentUser.getId().ToString();
            var querry = string.Format("INSERT INTO {0} VALUES (" + stringBuilder + ")", selClass);
            using(var command = new SqlCommand(querry, conn))
            {

                int l = 0;
                foreach(string value in values)
                {
                    command.Parameters.AddWithValue("@v" + l, value);
                    l++;
                }
                var reader = command.ExecuteReader();
                
                
                reader.Close();
                conn.Close();
            }

            int mana = int.Parse(dataGridView1.Rows[0].Cells["Mana"].Value.ToString());
            conn.Open();
            if (maxmana < mana && maxmana != -1)
            {
                mana = (int)(maxmana * 0.75);
                var q = string.Format("UPDATE {0} SET Mana = @M WHERE Id = @ID", selClass);
                using( var cmd = new SqlCommand(q, conn))
                {
                    cmd.Parameters.AddWithValue("@M", mana);
                    cmd.Parameters.AddWithValue("@ID", maxId);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Mana was too big!");
                }
            }
            conn.Close();

            conn.Open();
            var querry2 = string.Format("INSERT INTO Creatures VALUES ( @Id, @Archclass, @Class, @Race, @Name, @UserId, @Mana)", selClass);
            selRace = dataGridView1.Rows[0].Cells["Race"].Value.ToString();
            using (var command = new SqlCommand(querry2, conn))
            {
                command.Parameters.AddWithValue("@Id", maxId);
                command.Parameters.AddWithValue("@Archclass", selArch);
                command.Parameters.AddWithValue("@Class", selClass);
                command.Parameters.AddWithValue("@Race", selRace);
                command.Parameters.AddWithValue("@Name", dataGridView1.Rows[0].Cells["Name"].Value.ToString());
                command.Parameters.AddWithValue("@UserId", RegLogMgr.currentUser.getId());
                command.Parameters.AddWithValue("@Mana", mana);

                var reader = command.ExecuteReader();

                MessageBox.Show("Data uploaded!");

                reader.Close();
                conn.Close();
            }

        }

        public void createColumns(DataGridView dataGridViev)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Archclass");
            dt.Columns.Add("Class");
            dt.Columns.Add("Race");
            dt.Columns.Add("Name");
            dt.Columns.Add("Mana");

            dt.Rows.Add();

            dt.Rows[0]["Archclass"] = selArch;
            dt.Rows[0]["Class"] = selClass;
            dt.Rows[0]["Race"] = selRace;

            dataGridViev.DataSource = dt;
        }

        public void saveNewNewCreature(DataGridView dataGridViev)
        {
            List<string> alist = new List<string>();
            List<string> cslist = new List<string>();
            List<string> rlist = new List<string>();


            conn.Open();
            string arcc = "SELECT Archclass, Class, Race FROM Creatures";

            var queryx = string.Format("SELECT max(Id) FROM Creatures");
            using (var command2 = new SqlCommand(queryx, conn))
            {
                var reader = command2.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        maxId = reader.GetInt32(0) + 1;
                    }
                }
                reader.Close();
                conn.Close();
            }
            conn.Open();
            using (var command = new SqlCommand(arcc, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        alist.Add(reader.GetString(0));
                        cslist.Add(reader.GetString(1));
                        rlist.Add(reader.GetString(2));
                    }
                }

                reader.Close();
                conn.Close();
            }
            alist.Distinct().ToList();
            cslist.Distinct().ToList();
            rlist.Distinct().ToList();

            string race_data = dataGridViev.Rows[0].Cells["Race"].Value.ToString();
            string class_data = dataGridViev.Rows[0].Cells["Class"].Value.ToString();
            string archclass_data = dataGridViev.Rows[0].Cells["Archclass"].Value.ToString();

            //foreach (string a in alist)
            //{
            //    if (string.Equals(a.ToLower(), archclass_data.ToLower())){
            //        MessageBox.Show("We already have this archclass, try something else");
            //        return;
            //    }
            //}
            //
            foreach (string c in cslist)
            {
                if (string.Equals(c.ToLower(), class_data.ToLower()))
                {
                    MessageBox.Show("We already have this class, try something else");
                    return;
                }
            }

            foreach (string r in rlist)
            {
                if (string.Equals(r.ToLower(), race_data.ToLower()))
                {
                    MessageBox.Show("We already have this race, try something else");
                    return;
                }
            }

            //Archclass, Class, Race, Name, Mana

            if (dataGridViev.ColumnCount < 6)
            {
                MessageBox.Show("Add at least one column and try again");
                return;
            }

            foreach (DataGridViewRow rw in dataGridViev.Rows)
            {
                for (int i = 0; i < rw.Cells.Count; i++)
                {
                    if (rw.Cells[i].Value == null || rw.Cells[i].Value == DBNull.Value || String.IsNullOrWhiteSpace(rw.Cells[i].Value.ToString()))
                    {
                        MessageBox.Show("Fill all cells");
                        return;
                    }
                }
            }
            // do inserta
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewRow row in dataGridViev.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    sb.Append(cell.Value);
                    sb.Append(",");
                }
            }
            sb.Length--;

            string[] valuess = sb.ToString().Split(',');
            string[] values = new string[valuess.Length - 2];

            for (int i = 0; i < values.Length ; i++)
            {
                values[i] = valuess[i + 2];
            }


            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder stringBuilder1 = new StringBuilder();
            int k = 0;
            foreach (string value in values)
            {
                stringBuilder1.Append(value);
                stringBuilder.Append("@v" + k + ",");
                k++;
            }
            stringBuilder.Length--;

            //do create table

            StringBuilder columnnames = new StringBuilder();

            for (int i = 0; i < dataGridViev.ColumnCount; i++)
            {
                columnnames.Append(dataGridViev.Columns[i].Name);
                columnnames.Append(",");
            }
            columnnames.Length--;

            string[] colnamestab = columnnames.ToString().Split(',');
            string[] colnamestab2 = new string[colnamestab.Length - 3];
            for (int i = 0; i < colnamestab2.Length; i++)
            {
                colnamestab2[i] = colnamestab[i + 3];
            }

            StringBuilder readycols = new StringBuilder();

            foreach (string col in colnamestab2)
            {
                readycols.Append(col + " NVARCHAR (50) NOT NULL,");
            }




            conn.Open();
            string querry = string.Format("CREATE TABLE dbo.{0} ( Id INT NOT NULL, Race NVARCHAR (50) NOT NULL, " + readycols + "UserId INT NOT NULL, PRIMARY KEY CLUSTERED (Id ASC));", class_data);
           
            using (var command = new SqlCommand(querry, conn))
            {
                var reader = command.ExecuteReader();
                reader.Close();
                conn.Close();
            }
            
            conn.Open();
            var querry2 = string.Format("INSERT INTO Creatures VALUES ( @Id, @Archclass, @Class, @Race, @Name, @UserId, @Mana)", selClass);
            using (var command = new SqlCommand(querry2, conn))
            {
                command.Parameters.AddWithValue("@Id", maxId);
                command.Parameters.AddWithValue("@Archclass", archclass_data);
                command.Parameters.AddWithValue("@Class", class_data);
                command.Parameters.AddWithValue("@Race",race_data);
                command.Parameters.AddWithValue("@Name", dataGridViev.Rows[0].Cells["Name"].Value.ToString());
                command.Parameters.AddWithValue("@UserId", RegLogMgr.currentUser.getId());
                command.Parameters.AddWithValue("@Mana", dataGridViev.Rows[0].Cells["Mana"].Value.ToString());
                var reader = command.ExecuteReader();

                MessageBox.Show("Data uploaded!");
                reader.Close();
                conn.Close();

            }

            conn.Open();
            var querry3 = string.Format("INSERT INTO {0} VALUES ( @Id," + stringBuilder + ", @UserId)", class_data);
            using (var command = new SqlCommand(querry3, conn))
            {
                command.Parameters.AddWithValue("@Id", maxId);

                int l = 0;
                foreach (string value in values)
                {
                    command.Parameters.AddWithValue("@v" + l, value);
                    l++;
                }
                command.Parameters.AddWithValue("@UserId", RegLogMgr.currentUser.getId());

                var reader = command.ExecuteReader();


                reader.Close();
                conn.Close();
            }
        }

        public void addCol(DataGridView view, string name, string value)
        {
            List<string> values = new List<string>();

            

            for (int i = 0; i < view.ColumnCount; i++)
            {
                values.Append(view.Columns[i].Name);
            }

            foreach(string v in values)
            {
                if (string.Equals(v, name))
                {
                    MessageBox.Show("We already have this column!");
                    return;
                }
            }

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Column must have a name");
                return;
            }

            view.Columns.Add(name,name);
            view.Rows[0].Cells[name].Value = value;
        }

        public void delCol(DataGridView view, string name)
        {
            if(string.Equals(name, "Archclass") || string.Equals(name, "Class") || string.Equals(name, "Race") || string.Equals(name,"Name") || string.Equals(name, "Mana"))
            {
                MessageBox.Show("This column can't be deleted");
                return ;
            }

            
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < view.ColumnCount; i++)
            {
                builder.Append(view.Columns[i].Name + ",");
            }
            builder.Length--;

            string[] values = builder.ToString().Split(',');


            foreach (string v in values)
            {
                
                if (string.Equals(v, name))
                {
                    view.Columns.Remove(name);
                    return;
                }
            }
            MessageBox.Show("There isn't a column " + name);
            
        }

    }
}

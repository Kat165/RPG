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
    internal class MainMenuMgr
    {
        int? cuurr;

        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");
        public MainMenuMgr() { }

        public void vidible_btn(Button ES_MainM, Button EU_MainM, Button MyAcc_btn, Button Register_btn, Button See_Main, Button Login_btn, Button Add_Crt_btn_MM)
        {
            if (RegLogMgr.currentUser != null)
                cuurr = RegLogMgr.currentUser.getRoleId();
            else
                cuurr = null;


            if (cuurr == null)
            {
                ES_MainM.Visible = false;
                ES_MainM.Enabled = false;

                EU_MainM.Visible = false;
                EU_MainM.Enabled=false;

                MyAcc_btn.Visible = false;
                MyAcc_btn.Enabled = false;

                Add_Crt_btn_MM.Visible = false;
                Add_Crt_btn_MM.Enabled = false;

                Register_btn.Visible = true;
                Register_btn.Enabled = true;

                See_Main.Visible = true;
                See_Main.Enabled = true;

                Login_btn.Visible = true;
                Login_btn.Enabled = true;
            }
            else if (cuurr == 3)
            {
                ES_MainM.Visible = false;
                ES_MainM.Enabled = false;

                EU_MainM.Visible = false;
                EU_MainM.Enabled = false;

                MyAcc_btn.Visible = true;
                MyAcc_btn.Enabled = true;

                Add_Crt_btn_MM.Visible = true;
                Add_Crt_btn_MM.Enabled = true;

                Register_btn.Visible = true;
                Register_btn.Enabled = true;

                See_Main.Visible = true;
                See_Main.Enabled = true;

                Login_btn.Visible = true;
                Login_btn.Enabled = true;
            }else if(cuurr == 1)
            {
                ES_MainM.Visible = true;
                ES_MainM.Enabled = true;

                EU_MainM.Visible = true;
                EU_MainM.Enabled = true;

                MyAcc_btn.Visible = true;
                MyAcc_btn.Enabled = true;

                Add_Crt_btn_MM.Visible = true;
                Add_Crt_btn_MM.Enabled = true;

                Register_btn.Visible = true;
                Register_btn.Enabled = true;

                See_Main.Visible = true;
                See_Main.Enabled = true;

                Login_btn.Visible = true;
                Login_btn.Enabled = true;
            }
            else if (cuurr == 2)
            {
                ES_MainM.Visible = true;
                ES_MainM.Enabled = true;

                EU_MainM.Visible = false;
                EU_MainM.Enabled = false;

                MyAcc_btn.Visible = true;
                MyAcc_btn.Enabled = true;

                Add_Crt_btn_MM.Visible = true;
                Add_Crt_btn_MM.Enabled = true;

                Register_btn.Visible = true;
                Register_btn.Enabled = true;

                See_Main.Visible = true;
                See_Main.Enabled = true;

                Login_btn.Visible = true;
                Login_btn.Enabled = true;
            }
        }

        public void fillDataGridView(DataGridView view, string text)
        {
            int value;
            if (!int.TryParse(text, out value))
            {
                MessageBox.Show("Input must be a integer");
                return;
            }

            value = Convert.ToInt32(text);

            var querry = string.Format("SELECT TOP {0} * FROM Creatures ORDER BY Mana DESC", value);
            conn.Open();

            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Archclass");
            dt.Columns.Add("Class");
            dt.Columns.Add("Race");
            dt.Columns.Add("Name");
            dt.Columns.Add("UserId");
            dt.Columns.Add("Mana");

            using (var command = new SqlCommand(querry, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dt.Rows.Add(reader.GetInt32(0),reader.GetString(1),reader.GetString(2), reader.GetString(3), reader.GetString(4),reader.GetInt32(5), reader.GetInt32(6));
                    }
                }
                reader.Close();
                conn.Close();
            }
            dt.Columns["Id"].ColumnMapping = MappingType.Hidden;
            view.DataSource = dt;



            //List<string> classes = new List<string>();
            //DataTable dataTable = new DataTable();
            //conn.Open();
            //string arcc = "SELECT Class FROM Creatures";
            //
            //
            //using (var command = new SqlCommand(arcc, conn))
            //{
            //    var reader = command.ExecuteReader();
            //    if (reader.HasRows)
            //    {
            //        while (reader.Read())
            //        {
            //            classes.Add(reader.GetString(0));
            //        }
            //    }
            //
            //    reader.Close();
            //    conn.Close();
            //}
            //
            //classes = classes.Distinct().ToList();
            //var querry2 = "";
            //if (classes.Count > 0)
            //{
            //    if (classes.Count == 1)
            //        querry2 = string.Format("SELECT * FROM {0}", classes[0]);
            //    else
            //    {
            //        querry2 = string.Format("SELECT * FROM {0}", classes[0]);
            //        classes.RemoveAt(0);
            //        StringBuilder sb = new StringBuilder();
            //        int i = 0;
            //        foreach (string c in classes)
            //        {
            //            var x = string.Format(" JOIN {0} USING (@v" + i + ") ", c);
            //            sb.Append(x);
            //            i++;
            //        }
            //
            //        querry2 = querry2 + sb.ToString();
            //        string querry3 = "SELECT * FROM Dragon JOIN Wyvern ON Dragon.Id = Wyvern.Id";
            //
            //        string querry4 = "SELECT * FROM Dragon";
            //        MessageBox.Show(querry2);
            //        conn.Open();
            //
            //
            //        dataTable.Columns.Add("ID");
            //        dataTable.Columns.Add("Name");
            //
            //
            //        using (var command = new SqlCommand(querry4, conn))
            //        {
            //            int j = 0;
            //            foreach (string c in classes)
            //            {
            //                //command.Parameters.AddWithValue("@v" + j, "Id");
            //                j++;
            //            }
            //            var reader = command.ExecuteReader();
            //
            //            MessageBox.Show("''''''''''''''");
            //            if (reader.HasRows)
            //            {
            //                MessageBox.Show("lllllllllll");
            //                while (reader.Read())
            //                {
            //                    //for (int l = 0; l < reader.FieldCount; l++)
            //                    //{
            //                    //    dataTable.Columns.Add(reader[l].ToString());
            //                    //}
            //                    dataTable.Rows.Add(reader["Id"].ToString(), reader["Name"].ToString());
            //                }
            //            }
            //            
            //
            //
            //            reader.Close();
            //            conn.Close();
            //            view.DataSource = dataTable;
            //        }
            //    }
            //
            //
            //}
        }
    }
}

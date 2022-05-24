﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG.Data
{
    internal class SeeAll
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");

        public void LoadData(DataGridView view, string selClass)
        {
            DataTable dt = new DataTable();

            var sql = string.Format("SELECT * FROM {0}", selClass);
            SqlCommand cmd = new SqlCommand(sql, conn);
            conn.Open();

            // create data adapter
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // this will query your database and return the result to your datatable
            da.Fill(dt);
            conn.Close();
            da.Dispose();

            view.DataSource = dt;
        }

        public void LoadBox(ComboBox box)
        {
            string comm = "SELECT Class FROM Creatures";
            List<string> clist = new List<string>();
            conn.Open();
            using (var command = new SqlCommand(comm, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        clist.Add(reader.GetString(0));
                    }
                }

                reader.Close();
                conn.Close();
            }
            clist = clist.Distinct().ToList();

            foreach (string c in clist)
            {
                box.Items.Add(c);
            }
        }
    }
}
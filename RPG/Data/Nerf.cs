using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Data
{
    internal class Nerf
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");

        public string nerf_this(string c_class, string something, string value)
        {
            int x;

            if (!int.TryParse(value, out x))
            {
                return value;
            }

            var querry = string.Format("SELECT MAX({0}) FROM {1}", something, c_class);
            string res = "";
            conn.Open();
            using (var command = new SqlCommand(querry, conn))
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        res = reader.GetString(0);
                    }
                }
                reader.Close();
                conn.Close();
            }

            if (!int.TryParse(res, out x))
            {
                return value;
            }

            x = int.Parse(res);
            int y = int.Parse(value);

            if (y >= x)
            {
                y = (int)(x * 0.75);
            }
            return y.ToString();
        }
    }
}

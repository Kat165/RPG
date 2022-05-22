using RPG.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");

        //SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");

        private void Reign_reg_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Create_Acc_Click(object sender, EventArgs e)
        {
            RegLogMgr regLogMgr = new RegLogMgr();
            regLogMgr.Register(textBox1.Text, textBox2.Text);
            
        }

        private void Register_Load(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'databaseData.Users' . Możesz go przenieść lub usunąć.
            //this.usersTableAdapter.Fill(this.databaseData.Users);

        }
    }
}

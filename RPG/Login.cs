using RPG.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace RPG
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        //SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\kasia\source\repos\RPG\RPG\Database.mdf;Integrated Security=True");

        private void Reign_reg_btn_Click(object sender, EventArgs e)
        {
            
            this.DialogResult = DialogResult.OK;
        }

        private void Log_in_btn_Click(object sender, EventArgs e)
        {
            RegLogMgr logMgr = new RegLogMgr();
            bool logged = logMgr.login(nameText.Text, passText.Text);
            if (logged)
            {
                Log_in_btn.Enabled = false;
            }
        }
    }
}

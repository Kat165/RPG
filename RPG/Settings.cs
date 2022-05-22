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

namespace RPG
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            this.Close();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            MyAccountMgr mgr = new MyAccountMgr();
            mgr.newPassword(textBox1);

        }
    }
}

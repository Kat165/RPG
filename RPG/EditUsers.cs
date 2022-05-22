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
    public partial class EditUsers : Form
    {
        EditUsersMgr mgr = new EditUsersMgr();
        public EditUsers()
        {
            InitializeComponent();
            mgr.fillData(dataGridView1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mgr.updateUsers(dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mgr.deleteRole(dataGridView1);
        }
    }
}

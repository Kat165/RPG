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
    public partial class AccountForm : Form
    {
        MyAccountMgr mgr;
        EditCreaturesMgr cmgr;

        public static int selectedId = -1;
        public AccountForm()
        {
            InitializeComponent();
            mgr = new MyAccountMgr();
            mgr.printName(name_label);
            cmgr = new EditCreaturesMgr();
            cmgr.ShowData(dataGridView1);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            this.Hide();
            settings.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(selectedId == -1)
            {
                MessageBox.Show("Select creture to edit!");
                return;
            }
            EditCreatures edit = new EditCreatures();
            this.Hide();
            edit.ShowDialog();
            this.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                dataGridView1.ClearSelection();
                return;
            }
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            selectedId = int.Parse(row.Cells["Id"].Value.ToString());

        }
    }
}

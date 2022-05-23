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
    public partial class EditCreatures : Form
    {
        EditCreaturesMgr mgr;
        public static int selcid = 0;
        public EditCreatures()
        {
            InitializeComponent();
            mgr = new EditCreaturesMgr();
            if(RegLogMgr.currentUser.getRoleId() == 3)
            {
                mgr.showSpecData(AccountForm.selectedId, dataGridView1);
            }
            mgr.showBtn(label1, archcomboBox, label2, classcomboBox, label3, racecomboBox);
            mgr.fillClassComboBox(classcomboBox);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void classcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            mgr.EditAll(dataGridView1,classcomboBox.SelectedItem.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mgr.saveData(textBox1.Text, textBox2.Text, dataGridView1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                dataGridView1.ClearSelection();
                return;
            }
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            selcid = int.Parse(row.Cells["Id"].Value.ToString());
            MessageBox.Show("Selected!");
        }
    }
}

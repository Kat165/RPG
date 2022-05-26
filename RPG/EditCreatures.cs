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
        Delete delete;
        public EditCreatures()
        {
            InitializeComponent();
            mgr = new EditCreaturesMgr();
            delete = new Delete();
            mgr.showSpecData(AccountForm.selectedId, dataGridView1);
            archcomboBox.Enabled = false;
            archcomboBox.Visible = false;
            racecomboBox.Enabled = false;
            racecomboBox.Visible = false;
            classcomboBox.Enabled = false;
            classcomboBox.Visible = false;
            button1.Enabled = false;
            button1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            button4.Visible = false;
            button4.Enabled = false;
            mgr.showBtn(label1, archcomboBox, label2, classcomboBox, label3, racecomboBox);
            mgr.fillClassComboBox(classcomboBox,archcomboBox,racecomboBox);
        }

        public EditCreatures(int n)
        {
            InitializeComponent();
            mgr = new EditCreaturesMgr();
            delete = new Delete();

            mgr.showBtn(label1, archcomboBox, label2, classcomboBox, label3, racecomboBox);
            mgr.fillClassComboBox(classcomboBox, archcomboBox, racecomboBox);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void classcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(classcomboBox.SelectedItem != null)
                mgr.EditAll(dataGridView1,classcomboBox.SelectedItem.ToString());

            racecomboBox.SelectedIndex = -1;
            racecomboBox.Text = "";
            archcomboBox.SelectedIndex = -1;
            archcomboBox.Text = "";
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

        private void button1_Click(object sender, EventArgs e)
        {
            delete.delSth(archcomboBox, classcomboBox, racecomboBox);
            mgr.fillClassComboBox(classcomboBox, archcomboBox, racecomboBox);
        }

        private void archcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            classcomboBox.SelectedIndex = -1;
            classcomboBox.Text = "";
            racecomboBox.SelectedIndex = -1;
            racecomboBox.Text = "";
        }

        private void racecomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            classcomboBox.SelectedIndex = -1;
            classcomboBox.Text = "";
            archcomboBox.SelectedIndex = -1;
            archcomboBox.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            delete.delCtr(selcid);
            mgr.EditAll(dataGridView1, classcomboBox.SelectedItem.ToString());
            mgr.fillClassComboBox(classcomboBox, archcomboBox, racecomboBox);
        }
    }
}

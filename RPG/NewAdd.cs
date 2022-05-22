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
    public partial class NewAdd : Form
    {
        AddMgrcs add = new AddMgrcs();
        public NewAdd()
        {
            InitializeComponent();
            add.createColumns(dataGridView1);
        }

        private void NewCollumn_Click(object sender, EventArgs e)
        {
            add.addCol(dataGridView1,textBox1.Text,textBox2.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add.delCol(dataGridView1, textBox3.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            add.saveNewNewCreature(dataGridView1);
        }
    }
}

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
    public partial class OldAdd : Form
    {
        AddMgrcs add = new AddMgrcs();
        public OldAdd()
        {
            InitializeComponent();
            add.FillCollumnNames(dataGridView1);
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            add.saveOldNewCreature(this.dataGridView1 as DataGridView);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

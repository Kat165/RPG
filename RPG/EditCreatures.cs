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
        public EditCreatures()
        {
            InitializeComponent();
            mgr = new EditCreaturesMgr();
            mgr.showSpecData(AccountForm.selectedId, dataGridView1);
            mgr.showBtn(label1, archcomboBox, label2, classcomboBox, label3, racecomboBox);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }
    }
}

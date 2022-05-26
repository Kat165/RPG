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
    public partial class Add : Form
    {
        AddMgrcs add = new AddMgrcs();
        public Add()
        {
            InitializeComponent();
            
            add.fillComboBox(archcomboBox, classcomboBox, rowcomboBox);
        }

        private void cnclbutton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void addbutton_Click(object sender, EventArgs e)
        {
            if(classcomboBox.SelectedIndex == -1 && !classcheckBox.Checked)
            {
                MessageBox.Show("Pick Class or select checkbox");
                return;
            }
            add.saveStuff(archcomboBox.Text, classcomboBox.Text, rowcomboBox.Text);
            if (classcheckBox.Checked)
            {
                NewAdd newAdd = new NewAdd();
                this.Hide();
                newAdd.ShowDialog();
                this.Show();
            }
            else 
            {
                OldAdd oldAdd = new OldAdd();
                this.Hide();
                oldAdd.ShowDialog();
                this.Show();
            }
        }

        private void archcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            add.FillClass(archcomboBox, classcomboBox, rowcomboBox);
        }

        private void classcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(classcomboBox.SelectedIndex != -1)
                add.FillRace(classcomboBox,rowcomboBox);
        }
    }
}

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
    public partial class Search : Form
    {
        SeeAll see;
        public Search()
        {
            InitializeComponent();
            see = new SeeAll();
            see.LoadBox(classcomboBox);
            archcomboBox.Visible = false;
            racecomboBox.Visible = false;
            label1.Visible = false;
            label3.Visible = false;
        }

        private void Search_btn_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void classcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            see.LoadData(dataGridView1, classcomboBox.SelectedItem.ToString());
        }
    }
}

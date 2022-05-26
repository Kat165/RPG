using RPG.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPG
{
    public partial class MainMenu : Form
    {
        MainMenuMgr mainMenuMgr;
        public MainMenu()
        {


            InitializeComponent();
            mainMenuMgr = new MainMenuMgr();

            ES_MainM.Visible = false;
            ES_MainM.Enabled = false;

            EU_MainM.Visible = false;
            EU_MainM.Enabled = false;

            MyAcc_btn.Visible = false;
            MyAcc_btn.Enabled = false;

            Add_Crt_btn_MM.Visible = false;
            Add_Crt_btn_MM.Enabled = false;

            Register_btn.Visible = true;
            Register_btn.Enabled = true;

            See_Main.Visible = true;
            See_Main.Enabled = true;

            Login_btn.Visible = true;
            Login_btn.Enabled = true;

            mainMenuMgr.fillDataGridView(dataGridView1, textBox1.Text);
        }

        private void Register_btn_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            this.Hide();
            register.ShowDialog();
            this.Show();
        }

        private void Login_btn_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            this.Hide();
            login.ShowDialog();
            this.Show();
            mainMenuMgr.vidible_btn(ES_MainM, EU_MainM, MyAcc_btn, Register_btn, See_Main, Login_btn, Add_Crt_btn_MM);
        }

        private void See_Main_Click(object sender, EventArgs e)
        {
            Search search = new Search();
            this.Hide();
            search.ShowDialog();
            this.Show();
        }

        private void EU_MainM_Click(object sender, EventArgs e)
        {
            EditUsers editUsers = new EditUsers();
            this.Hide();
            editUsers.ShowDialog();
            this.Show();
        }

        private void ES_MainM_Click(object sender, EventArgs e)
        {
            EditCreatures editCreatures = new EditCreatures(1);
            this.Hide();
            editCreatures.ShowDialog();
            this.Show();
        }

        private void MyAcc_btn_Click(object sender, EventArgs e)
        {
            AccountForm accountForm = new AccountForm();
            this.Hide();
            accountForm.ShowDialog();
            this.Show();
        }

        private void Add_Crt_btn_MM_Click(object sender, EventArgs e)
        {
            Add add = new Add();
            this.Hide();
            add.ShowDialog();
            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            mainMenuMgr.fillDataGridView(dataGridView1, textBox1.Text);
        }
    }
}

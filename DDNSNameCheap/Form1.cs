using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDNSNameCheap
{
    public partial class Form1 : Form
    {
        private bool forceClose;

        public Form1()
        {
            forceClose = false;

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            Update();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!forceClose)
            {
                this.Hide();
                e.Cancel = true;
            }            
        }

        private void notiIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forceClose = true;

            Application.Exit();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm af = new AboutForm();
            af.Show();
        }

        private void updateNowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update();
        }

        private void Update()
        {

        }

        private void LoadData()
        {
            
        }

        private void settingProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileForm pf = new ProfileForm();
            pf.ShowDialog();

            LoadData();

            Update();
        }
    }
}

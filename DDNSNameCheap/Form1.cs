using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDNSNameCheap
{
    public partial class Form1 : Form
    {
        private bool forceClose;

        private string PublicIP;

        public Form1()
        {
            forceClose = false;

            PublicIP = "127.0.0.1";

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadPositionWindow();

            timerPublicIp.Start();

            LoadData();
            Update();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!forceClose)
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

        private  void LoadData()
        {

        }

        public async Task<string> GetPublicIP()
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("http://checkip.dyndns.org/");

            string tempIp = result.Split(':')[1].Trim();

            return tempIp.Split('<')[0].Trim();
        }

        private void LoadPositionWindow()
        {
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            int height = Screen.PrimaryScreen.WorkingArea.Height;

            this.SetDesktopLocation(width - this.Width, height - this.Height);
        }

        private void UpdateList(Profile profile)
        {
            ListViewItem listViewItem = new ListViewItem("");

            listViewItem.SubItems.Add(DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"));
            listViewItem.SubItems.Add(profile.GetHost());
            listViewItem.SubItems.Add("IP update: " + profile.Ip);

            lvUpdate.Items.Add(listViewItem);

            lvUpdate.Refresh();

            statusStrip1.Text = "Last updated at " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
        }

        private void settingProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileForm pf = new ProfileForm();
            pf.ShowDialog();

            LoadData();

            Update();
        }

        private async void timerPublicIp_Tick(object sender, EventArgs e)
        {
            this.PublicIP = await GetPublicIP();
            timerPublicIp.Stop();
            timerPublicIp.Interval = 600000;
            timerPublicIp.Start();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lvUpdate.Items.Clear();
            }
            catch { }

        }
    }
}

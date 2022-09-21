using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace DDNSNameCheap
{
    public partial class Form1 : Form
    {
        private bool forceClose;

        private string PublicIP;

        private List<Profile> profiles;

        private List<CancellationTokenSource> cancellations;

        private string pathData;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            forceClose = false;

            PublicIP = "127.0.0.1";

            pathData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            pathData += "\\DDNSNameCheap\\";

            if (!Directory.Exists(pathData))
            {
                Directory.CreateDirectory(pathData);
            }

            profiles = new List<Profile>();

            cancellations = new List<CancellationTokenSource>();

            LoadPositionWindow();

            await LoadFirstPublicIP();
            await LoadData();

            Task update = UpdateData();

            Task checkingPublicIP = CheckPublicIP();
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

            Serialize();

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

        private async Task UpdateData()
        {
            foreach (CancellationTokenSource c in cancellations)
            {
                try
                {
                    c.Cancel();
                }
                catch { }

            }

            cancellations.Clear();

            foreach (Profile p in profiles)
            {
                CancellationTokenSource c = new CancellationTokenSource();

                cancellations.Add(c);

                Task t = Task.Run(async () =>
                    {
                        while (!c.Token.IsCancellationRequested)
                        {
                            await UpdateIP(p);
                            Thread.Sleep(p.Interval);
                        }
                    });
            }
        }

        private void Serialize()
        {
            string dataFile = "Data.dat";

            StreamWriter sw = new StreamWriter(new FileStream(pathData + dataFile, FileMode.Create));

            XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>));

            serializer.Serialize(sw, profiles);

            sw.Close();
        }

        public async Task UpdateIP(Profile p)
        {
            if (p.Ip != PublicIP)
            {
                p.Ip = PublicIP;
            }

            string url = $@"https://dynamicdns.park-your-domain.com/update?host={p.Host}&domain={p.Domain}&password={p.Key}&ip={p.Ip}";

            HttpClient client = new HttpClient();
            string response = await client.GetStringAsync(url);

            if (response != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);

                string json = JsonConvert.SerializeXmlNode(doc);

                string[] sliptResult = json.Split(',');

                int error = 0;

                string errorDescription = "";

                foreach (string s in sliptResult)
                {
                    if (s.Contains("ErrCount"))
                    {
                        string[] cut = s.Split('{');

                        string tempCut = cut.Last().Split(':').Last();

                        string outParse = tempCut.Replace("\"", "");

                        try
                        {
                            error = int.Parse(outParse);
                        }
                        catch (Exception e)
                        {
                            error = 1;
                            errorDescription = e.Message;
                        }
                    }

                    if (s.Contains("Description"))
                    {
                        string[] cut = s.Split('{');

                        string tempCut = cut.Last().Split(':').Last();

                        errorDescription = tempCut.Replace("\"", "");
                    }

                }

                if (error > 0)
                {
                    UpdateList(p, errorDescription);
                }
                else
                {
                    UpdateList(p);
                }
            }
            else
            {
                UpdateList(p, "Can not request");
            }
        }

        private async Task LoadData()
        {
            string dataFile = "Data.dat";

            if (File.Exists(pathData + dataFile))
            {
                Task t = Task.Run(() =>
                {
                    StreamReader sr = new StreamReader(new FileStream(pathData + dataFile, FileMode.Open));

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>));

                    profiles = serializer.Deserialize(sr) as List<Profile>;

                    sr.Close();

                });

                await t;
            }
        }

        public async Task<string> GetPublicIP()
        {
            HttpClient client = new HttpClient();
            string result = await client.GetStringAsync("http://checkip.dyndns.org/");

            string tempIp = result.Split(':')[1].Trim();

            return tempIp.Split('<')[0].Trim();
        }

        public async Task CheckPublicIP()
        {
            Task t = Task.Run(async () =>
            {
                while (true)
                {
                    this.PublicIP = await GetPublicIP();
                    tssIp.Text = "Current public IP: " + PublicIP;
                    Thread.Sleep(30000);
                }
            });
            await t;
        }

        public async Task LoadFirstPublicIP()
        {
            Task t = Task.Run(async () =>
            {
                this.PublicIP = await GetPublicIP();
                tssIp.Text = "Current public IP: " + PublicIP;
            });
            await t;
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
            listViewItem.SubItems.Add(profile.GetHost);
            listViewItem.SubItems.Add("IP update: " + profile.Ip);

            this.Invoke(new Action(() =>
            {
                lvUpdate.Items.Add(listViewItem);

                lvUpdate.Refresh();

                tssLastUpdate.Text = "Last updated at " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            }));
        }

        private void UpdateList(Profile profile, string message)
        {
            ListViewItem listViewItem = new ListViewItem("");

            listViewItem.SubItems.Add(DateTime.Now.ToString("hh:mm:ss dd/MM/yyyy"));
            listViewItem.SubItems.Add(profile.GetHost);
            listViewItem.SubItems.Add(message);

            this.Invoke(new Action(() =>
            {
                lvUpdate.Items.Add(listViewItem);

                lvUpdate.Refresh();

                tssLastUpdate.Text = "Last updated at " + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy");
            }));
        }

        private async void settingProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ProfileForm pf = new ProfileForm(profiles, pathData);
            pf.ShowDialog();

            await LoadData();

            Task u = UpdateData();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                lvUpdate.Items.Clear();
                lvUpdate.Refresh();
            }
            catch { }

        }
    }
}

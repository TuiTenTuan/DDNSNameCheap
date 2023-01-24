using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DDNSNameCheap
{
    public partial class Form1 : Form
    {
        private bool forceClose;

        private string PublicIP;

        private List<Profile> profiles;

        private List<CancellationTokenSource> cancellations;

        private string pathData;

        private string dataFileName;

        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            forceClose = false;

            PublicIP = "127.0.0.1";

            dataFileName = "Profiles.Dat";

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
            profiles = await Config.Instance.Deserialize(pathData, dataFileName);

            Task update = UpdateData();

            Task checkingPublicIP = CheckPublicIP();

            //TestForm tf = new TestForm();
            //tf.ShowDialog();
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
            this.BringToFront();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forceClose = true;

            Config.Instance.Serialize(pathData, dataFileName, profiles);

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

        public async Task UpdateIP(Profile p)
        {
            if (p.Ip != PublicIP)
            {
                p.Ip = PublicIP;
            }

            if (p.IsDomainName)
            {
                IPAddress address = await GetIPByDomainName(p.DomainName);
                p.Ip = address.MapToIPv4().ToString();
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
            ProfileForm pf = new ProfileForm(profiles, pathData, dataFileName);
            pf.ShowDialog();

            profiles = await Config.Instance.Deserialize(pathData, dataFileName);

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

        private async Task<IPAddress> GetIPByDomainName(string domainName)
        {
            IPAddress[] ip = await Dns.GetHostAddressesAsync(domainName);

            return ip.FirstOrDefault();
        }

        private async void importSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofdImport = new OpenFileDialog();

            ofdImport.Multiselect = false;
            ofdImport.Title = "Import Settings";
            ofdImport.DefaultExt = "Dat";
            ofdImport.AddExtension = false;
            ofdImport.Filter = "Data file (*.dat)|*.dat|Datafile (*.Dat)|*.Dat";

            if (ofdImport.ShowDialog() == DialogResult.OK)
            {
                string sourceFilePath = ofdImport.FileName;

                string extentsionFile = sourceFilePath.Substring(sourceFilePath.LastIndexOf(".") + 1);

                if (extentsionFile.ToLower().CompareTo("dat") == 0)
                {
                    List<Profile> dataLoad = await Config.Instance.Deserialize(sourceFilePath);

                    if (MessageBox.Show(dataLoad.Count() + " Profile load from file.\nDo you want to continue load profile (lost old profile)?", "Load Profile", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        Config.Instance.Serialize(pathData, dataFileName, dataLoad);

                        profiles = dataLoad;

                        await UpdateData();
                    }
                }
                else
                {
                    MessageBox.Show("File input incorrect data type", "Data incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void exportSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(pathData, dataFileName)))
            {
                SaveFileDialog sfdExport = new SaveFileDialog();
                sfdExport.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                sfdExport.Title = "Export Settting Files";
                sfdExport.CheckFileExists = true;
                sfdExport.CheckPathExists = true;
                sfdExport.DefaultExt = "dat";
                sfdExport.Filter = "Data file (*.dat)|*.dat|Datafile (*.Dat)|*.Dat";
                sfdExport.RestoreDirectory = true;

                if (sfdExport.ShowDialog() == DialogResult.OK)
                {
                    if (sfdExport.FileName != "")
                    {
                        File.Copy(Path.Combine(pathData, dataFileName), sfdExport.FileName, true);
                    }
                    else
                    {
                        MessageBox.Show("File path not exits", "Error when save file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("No profile detechted", "Error when export file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDNSNameCheap
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private async Task<IPAddress> GetIPByDomainName(string domainName)
        {
            IPAddress[] ip = await Dns.GetHostAddressesAsync(domainName);

            return ip.FirstOrDefault();
        }

        private async void TestForm_Load(object sender, EventArgs e)
        {
            IPAddress ipdm = await GetIPByDomainName("tuitentuan.ddns.net");

            label1.Text = ipdm.MapToIPv4().ToString();
        }

        private void button1_Click(object sender, EventArgs e)
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
                    MessageBox.Show(sourceFilePath, "Data incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("File input incorrect data type", "Data incorrect", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}

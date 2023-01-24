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
    }
}

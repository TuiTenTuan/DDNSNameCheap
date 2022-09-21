using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml.Serialization.Configuration;

namespace DDNSNameCheap
{
    public partial class ProfileForm : Form
    {
        private List<Profile> _profiles;
        private string _path;

        private bool IsCreateNew;

        public ProfileForm(List<Profile> profiles, string path)
        {
            IsCreateNew = false;

            _profiles = profiles;

            _path = path;

            InitializeComponent();
        }

        private void ProfileForm_Load(object sender, EventArgs e)
        {
            List<Interval> intervals = new List<Interval>();
            intervals.Add(new Interval() { IntervalTime = 300000, Time = "5 Minutes" });
            intervals.Add(new Interval() { IntervalTime = 600000, Time = "10 Minutes" });
            intervals.Add(new Interval() { IntervalTime = 900000, Time = "15 Minutes" });
            intervals.Add(new Interval() { IntervalTime = 1800000, Time = "30 Minutes" });
            intervals.Add(new Interval() { IntervalTime = 3600000, Time = "1 Hour" });
            intervals.Add(new Interval() { IntervalTime = 10800000, Time = "3 Hour" });
            intervals.Add(new Interval() { IntervalTime = 21600000, Time = "6 Hour" });
            intervals.Add(new Interval() { IntervalTime = 43200000, Time = "12 Hours" });
            intervals.Add(new Interval() { IntervalTime = 86400000, Time = "1 Day" });
            intervals.Add(new Interval() { IntervalTime = 259200000, Time = "3 Days" });

            cbInterval.DataSource = intervals;
            cbInterval.DisplayMember = "Time";
            cbInterval.ValueMember = "IntervalTime";

            cbInterval.SelectedIndex = 4;

            if (_profiles.Count <= 0)
            {
                btnDelete.Enabled = false;
                btnSave.Enabled = false;
            }
            else
            {
                cbListProfile.DataSource = _profiles;
                cbListProfile.DisplayMember = "GetHost";
                cbListProfile.SelectedIndex = 0;
            }
        }

        private void Serialize()
        {
            string dataFile = "Data.dat";

            StreamWriter sw = new StreamWriter(new FileStream(_path + dataFile, FileMode.Create));

            XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>));

            serializer.Serialize(sw, _profiles);

            sw.Close();
        }

        private void ClearInput()
        {
            tbHost.Text = "";
            tbDomain.Text = "";
            tbKey.Text = "";
            cbInterval.SelectedIndex = 4;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ClearInput();

            IsCreateNew = true;

            btnSave.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            _profiles.RemoveAt(cbListProfile.SelectedIndex);
            Serialize();

            cbListProfile.DataSource = _profiles;
            cbListProfile.DisplayMember = "GetHost";
            cbListProfile.Update();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsCreateNew)
            {
                Profile p = new Profile(tbDomain.Text, tbHost.Text, tbKey.Text, (int)cbInterval.SelectedValue);

                _profiles.Add(p);

                IsCreateNew = false;
            }
            else
            {
                _profiles[cbListProfile.SelectedIndex].Host = tbHost.Text;
                _profiles[cbListProfile.SelectedIndex].Domain = tbDomain.Text;
                _profiles[cbListProfile.SelectedIndex].Key = tbKey.Text;
                _profiles[cbListProfile.SelectedIndex].Interval = (int)cbInterval.SelectedValue;
            }

            cbListProfile.DataSource = _profiles;
            cbListProfile.Refresh();
            cbListProfile.DisplayMember = "GetHost";

            btnDelete.Enabled = true;

            ClearInput();

            Serialize();
        }

        private void cbListProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbDomain.Text = _profiles[cbListProfile.SelectedIndex].Domain;
            tbHost.Text = _profiles[cbListProfile.SelectedIndex].Host;
            tbKey.Text = _profiles[cbListProfile.SelectedIndex].Key;
            cbInterval.SelectedValue = _profiles[cbListProfile.SelectedIndex].Interval;
        }
    }
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;

namespace DDNSNameCheap
{
    [RunInstaller(true)]
    public partial class InstallerConfig : Installer
    {
        public InstallerConfig() : base()
        {
            InitializeComponent();
        }

        // Override the 'OnAfterInstall' method.
        protected override void OnAfterInstall(IDictionary savedState)
        {
            base.OnAfterInstall(savedState);

            string path = this.Context.Parameters["assemblypath"];

            string fullpath = Path.Combine(path, "DDNSNameCheap.exe");

            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "sc create DDNSNameCheap binPath= \"" + fullpath + "\" type = interact start = auto";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}

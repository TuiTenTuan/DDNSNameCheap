using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDNSNameCheap
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string Domain { get; set; }

        public string Host { get; set; }

        public string Key { get; set; }

        public long Interval { get; set; }

        public string IdProcess { get; set; }

        public string Ip { get; set; }

        public Profile(Guid id, string domain, string host, string key, long interval, string idProcess, string ip)
        {
            Id = id;
            Domain = domain;
            Host = host;
            Key = key;
            Interval = interval;
            IdProcess = idProcess;
            Ip = ip;
        }

        public Profile(string domain, string host, string key, long interval) : this(Guid.NewGuid(), domain, host, key, interval, "", "127.0.0.1") { }

        public Profile() : this("", "", "", 1800) { }

        public string GetHost()
        {
            return Host + "." + Domain;
        }
    }
}

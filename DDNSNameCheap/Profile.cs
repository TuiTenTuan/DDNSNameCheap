using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DDNSNameCheap
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string Domain { get; set; }

        public string Host { get; set; }

        public string Key { get; set; }

        public int Interval { get; set; }

        public string Ip { get; set; }

        public string DomainName { get; set; }

        public bool IsDomainName { get; set; }

        public string GetHost { get { return Host + "." + Domain; } }

        public Profile(Guid id, string domain, string host, string key, int interval, string ip)
        {
            Id = id;
            Domain = domain;
            Host = host;
            Key = key;
            Interval = interval;
            Ip = ip;
            DomainName = "";
            IsDomainName = false;
        }

        public Profile(string domain, string host, string key, int interval) : this(Guid.NewGuid(), domain, host, key, interval, "127.0.0.1") { }

        public Profile() : this("", "", "", 1800) { }
    }
}

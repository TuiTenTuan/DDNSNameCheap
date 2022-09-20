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


    }
}

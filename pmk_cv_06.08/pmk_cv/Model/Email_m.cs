using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRCloud.Model
{
    class Email_m
    {
        public class MailServer_m
        {
            public string type { get; set; }
            public string mailserver { get; set; }
            public int port { get; set; }
            public bool ssl { get; set; }
            public string login { get; set; }
            public string password { get; set; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RINTE_Care.Models
{
    public class Email
    {
        public string SenderEmail { get; set; }

        public string DestinationName1 { get; set; } = null;
        public string DestinationEmail1 { get; set; } = null;

        public string DestinationName2 { get; set; } = null;
        public string DestinationEmail2 { get; set; } = null;
        public string Subject { get; set; }
        public string Body { get; set; }
        public string FileAttachment { get; set; } = default;
    }
}

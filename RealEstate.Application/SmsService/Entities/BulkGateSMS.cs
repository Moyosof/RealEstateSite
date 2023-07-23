using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.SmsService.Entities
{
    public class BulkGateSMS
    {
        public string Url { get; set; }
        public string application_id { get; set; }
        public string application_token { get; set; }
        public string Host { get; set; }
    }
}

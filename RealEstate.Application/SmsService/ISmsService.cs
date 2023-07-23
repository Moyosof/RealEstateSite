﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.SmsService
{
    public interface ISmsService
    {
        Task<string> SendOtpToNumber(BulkgateDTO smsDTo);
        Task<string> SendSmsHollaTags(HollaTagsDTO smsDTo);
    }
}

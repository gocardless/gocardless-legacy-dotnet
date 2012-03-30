using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sample.Mvc3.Models
{
    public class LinksModel
    {
        public string NewBillUrl { get; set; }
        public string NewSubscriptionUrl { get; set; }
        public string NewPreAuthorizationUrl { get; set; }
        public string Error { get; set; }
    }
}
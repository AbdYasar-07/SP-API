using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SP_API.SpService
{
    public class MySiteConfiguration
    {
        public const string SectionName = "MySiteConfiguration";

        public string ListName { get; set; }
        public string ErrorListName { get; set; }
        public string SiteUrl { get; set; }
        public string AppId { get; set; }
        public string AppSecret { get; set; }

    }
}

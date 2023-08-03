using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Client;

namespace Crawler
{
    public sealed class FeedSettings
    {
        public required string Feed { get; set; }
        public string[] ExcludedIdPrefixes { get; set; }
    }
}

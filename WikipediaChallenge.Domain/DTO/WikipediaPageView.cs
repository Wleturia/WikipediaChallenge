using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikipediaChallenge.Domain.DTO
{
    public class WikipediaPageView
    {
        public WikipediaPageView(string url, string folder, string uFolder, string filename)
        {
            this.url = url;
            this.cFolder = folder;
            this.uFolder = folder;
            this.filename = filename;
        }

        public string url { get; set; }
        public string cFolder { get; set; }
        public string uFolder { get; set; }
        public string filename { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikipediaChallenge.Domain.DTO
{
    public class WikipediaPageView
    {
        public WikipediaPageView(string url, string folder, string filename)
        {
            this.url = url;
            this.folder = folder;
            this.filename = filename;
        }

        public string url { get; set; }
        public string folder { get; set; }
        public string filename { get; set; }
    }
}

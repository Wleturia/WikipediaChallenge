using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikipediaChallenge.Domain.DTO
{
    public class WikipediaPageView
    {
        public WikipediaPageView(string url, string folder, string uFolder, string filename, string fileExtension)
        {
            this.url = url;
            this.cFolder = folder;
            this.uFolder = uFolder;
            this.filename = filename;
            this.fileExtension = fileExtension;
        }

        public string fileExtension { get; set; }
        public string url { get; set; }
        public string cFolder { get; set; }
        public string uFolder { get; set; }
        public string filename { get; set; }
    }
}

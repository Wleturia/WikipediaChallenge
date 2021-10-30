using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace WikipediaChallenge.Console
{
    public class Environment
    {
        public static string PageViewBaseUrlTemplate = ConfigurationManager.AppSettings["RepositoryURITemplate"];
        public static string DownloadBaseFolder = ConfigurationManager.AppSettings["DownloadFolder"];
        public static string UncompressedBaseFolder = ConfigurationManager.AppSettings["UncompressedFolder"];
    }
}

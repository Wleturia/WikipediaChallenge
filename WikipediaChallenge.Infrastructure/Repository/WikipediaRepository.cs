using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WikipediaChallenge.Domain.Entity;
using WikipediaChallenge.Domain.Repository;

namespace WikipediaChallenge.Infrastructure.Repository
{
    public class WikipediaRepository : IWikipediaRepository
    {
        public String repositoryURL;

        public WikipediaRepository(string repositoryURL)
        {
            this.repositoryURL = repositoryURL;
        }

        public Exception DownloadDataWikipediaDTO(Domain.DTO.WikipediaPageView wikipediaPageView)
        {
            Console.WriteLine("Retrieving data from: " + wikipediaPageView.url);
            Console.WriteLine("Writing into: " + wikipediaPageView.cFolder);

            using WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(wikipediaPageView.url, @wikipediaPageView.cFolder + wikipediaPageView.filename);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error" + err);
                return err;
            }
            return null;
        }

        public (List<PageView> pageViews, Exception exception) GetDataFromURITemplateWithDate(DateTime date)
        {
            /*
                        bool existsFolder = System.IO.Directory.Exists(downloadF);
                        if (!existsFolder)
                            System.IO.Directory.CreateDirectory(downloadF);

                        List<PageView> pageViews = new List<PageView>();
                        Console.WriteLine("Repository");
                        return (pageViews, new Exception("Critical error"));
            */
            throw new NotImplementedException();

        }

        public string GetRepositoryURL()
        {
            return this.repositoryURL;
        }
    }
}

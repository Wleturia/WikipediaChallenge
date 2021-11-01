using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using WikipediaChallenge.Domain.DTO;
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
        public string GetRepositoryURL()
        {
            return this.repositoryURL;
        }

        public Exception DecompressDataWikipediaDTO(WikipediaPageView wikipediaPageView)
        {
            try
            {
                using FileStream originalFileStream = File.Open(wikipediaPageView.cFolder + wikipediaPageView.filename + wikipediaPageView.fileExtension, FileMode.Open);

                using FileStream decompressedFileStream = File.Create(wikipediaPageView.uFolder + wikipediaPageView.filename);

                using GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress);

                Console.WriteLine(String.Format("Unziping {0} into {1}", wikipediaPageView.filename, wikipediaPageView.uFolder));
                decompressionStream.CopyTo(decompressedFileStream);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return err;
            }
            return null;
        }

        public Exception DownloadDataWikipediaDTO(WikipediaPageView wikipediaPageView)
        {
            Console.WriteLine("Retrieving data from: " + wikipediaPageView.url);
            Console.WriteLine("Writing into: " + wikipediaPageView.cFolder);

            using WebClient wc = new WebClient();
            try
            {
                wc.DownloadFile(wikipediaPageView.url, @wikipediaPageView.cFolder + wikipediaPageView.filename + wikipediaPageView.fileExtension);
            }
            catch (Exception err)
            {
                Console.WriteLine("Error" + err);
                return err;
            }
            return null;
        }
    }
}

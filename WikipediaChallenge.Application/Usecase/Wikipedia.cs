using System;
using System.Collections.Generic;
using WikipediaChallenge.Domain.Entity;
using WikipediaChallenge.Domain.Repository;
using WikipediaChallenge.Domain.Usecase;
using WikipediaChallenge.Domain.DTO;
using System.IO;
using System.Linq;

namespace WikipediaChallenge.Application.Usecase
{
    public class Wikipedia : IWikipedia
    {
        public readonly IWikipediaRepository wikipediaRepository;
        public readonly ILocalRepository localRepository;

        public Wikipedia(IWikipediaRepository wikipediaRepository, ILocalRepository localRepository)
        {
            this.wikipediaRepository = wikipediaRepository;
            this.localRepository = localRepository;
        }

        public Exception DecompressWikipediaData(IEnumerable<WikipediaPageView> wikipediaPageViews)
        {
            Exception err = null;

            foreach (var wko in wikipediaPageViews)
            {
                err = localRepository.CreateFolder(wko.uFolder);
                if (err != null)
                {
                    break;
                }

                if (!localRepository.VerifyFile(wko.uFolder + wko.filename))
                {
                    err = wikipediaRepository.DecompressDataWikipediaDTO(wko);
                    if (err != null)
                    {
                        break;
                    }
                }
            }
            return err;
        }

        public Exception DownloadWikipediaData(IEnumerable<WikipediaPageView> wikipediaPageViews)
        {
            Exception err = null;
            foreach (var wko in wikipediaPageViews)
            {
                err = localRepository.CreateFolder(wko.cFolder);
                if (err != null)
                {
                    break;
                }


                if (!localRepository.VerifyFile(wko.cFolder + wko.filename + wko.fileExtension))
                {
                    err = wikipediaRepository.DownloadDataWikipediaDTO(wko);
                    if (err != null)
                    {
                        break;
                    }
                }
            }
            return err;
        }

        // Should move to services layer
        public IEnumerable<Domain.DTO.WikipediaPageView> FromDateTimeListRetrieveWikipediaPageViewDTOList(IEnumerable<DateTime> dateTimes)
        {
            List<Domain.DTO.WikipediaPageView> list = new();
            foreach (var date in dateTimes)
            {
                var year = date.Year.ToString();
                var month = date.ToString("MM");
                var day = date.ToString("yyyyMMdd");
                var time = date.ToString("HH0000");

                var downloadURL = String.Format(wikipediaRepository.GetRepositoryURL(), year, month, day, time);
                var uFolder = localRepository.GetUncompressedLocationFolder() + String.Format("/{0}/{0}-{1}/", year, month);
                var cFolder = localRepository.GetCompressedLocationFolder() + String.Format("/{0}/{0}-{1}/", year, month);
                var filename = String.Format("pageviews-{0}-{1}", day, time);

                Domain.DTO.WikipediaPageView wikipediaPageView = new(downloadURL, cFolder, uFolder, filename, ".gz");
                list.Add(wikipediaPageView);
            }

            return list;
        }

        public IEnumerable<Domain.Entity.PageView> ProcessDecompressedWikipediaData(IEnumerable<WikipediaPageView> wikipediaPageViews, int qty)
        {
            Dictionary<string, int> PageViewEntityDict = new Dictionary<string, int>();
            string separator = "|";

            foreach (var wk in wikipediaPageViews)
            {
                Console.WriteLine("Processing: " + wk.filename);
                using (var sr = localRepository.OpenStreamReader(wk.uFolder + wk.filename))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var strSplit = line.Split(" ");

                        if (strSplit.Length != 4)
                        {
                            Console.WriteLine("Skipping corrupted file record: " + wk.filename);
                            continue;
                        }

                        // Group by DomainCode and PageTitle
                        string concatProperty = strSplit[0] + separator + strSplit[1];

                        if (!PageViewEntityDict.ContainsKey(concatProperty))
                        {
                            PageViewEntityDict.Add(concatProperty, Int32.Parse(strSplit[2]));
                            continue;
                        }

                        PageViewEntityDict[concatProperty] += Int32.Parse(strSplit[2]);
                    }
                }
            }


            var pageViewEntityDictSliced = PageViewEntityDict.OrderByDescending(it => it.Value).Take(qty);

            List<Domain.Entity.PageView> list = new List<Domain.Entity.PageView>();
            foreach (var obj in pageViewEntityDictSliced)
            {
                var strSlice = obj.Key.Split("|");
                // For optimization purposes last value will be null
                list.Add(new Domain.Entity.PageView(strSlice[0], strSlice[1], obj.Value, ""));
            }
            return list;

            //return null;
        }
    }
}

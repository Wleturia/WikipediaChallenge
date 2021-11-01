using System;
using System.Collections.Generic;
using WikipediaChallenge.Domain.Entity;
using WikipediaChallenge.Domain.Repository;
using WikipediaChallenge.Domain.Usecase;
using WikipediaChallenge.Domain.DTO;

namespace WikipediaChallenge.Application.Usecase
{
    public class Application : IApplication
    {
        public readonly IWikipediaRepository wikipediaRepository;
        public readonly ILocalRepository localRepository;

        public Application(IWikipediaRepository wikipediaRepository, ILocalRepository localRepository)
        {
            this.wikipediaRepository = wikipediaRepository;
            this.localRepository = localRepository;
        }

        public Exception DecompressWikipediaData(List<WikipediaPageView> wikipediaPageViews)
        {
            Exception err = null;
            wikipediaPageViews.ForEach(wko =>
            {
                err = localRepository.CreateFolder(wko.uFolder);
                if (err != null)
                {
                    return;
                }

                if (!localRepository.VerifyFile(wko.uFolder + wko.filename))
                {
                    wikipediaRepository.DecompressDataWikipediaDTO(wko);
                }
            });
            return null;
        }

        public Exception DownloadWikipediaData(List<WikipediaPageView> wikipediaPageViews)
        {
            Exception err = null;
            wikipediaPageViews.ForEach(wko =>
            {
                err = localRepository.CreateFolder(wko.cFolder);
                if (err != null)
                {
                    return;
                }


                if (!localRepository.VerifyFile(wko.cFolder + wko.filename + wko.fileExtension))
                {
                    wikipediaRepository.DownloadDataWikipediaDTO(wko);
                }
            });
            return err;
        }

        // Should move to services layer
        public List<Domain.DTO.WikipediaPageView> FromDateTimeListRetrieveWikipediaPageViewDTOList(List<DateTime> dateTimes)
        {
            List<Domain.DTO.WikipediaPageView> list = new();
            dateTimes.ForEach(date =>
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
            });

            return list;
        }

        public List<PageView> ProcessDecompressedWikipediaData(List<WikipediaPageView> wikipediaPageViews)
        {
            List<PageView> pageViews = new();

            wikipediaPageViews.ForEach(wk =>
            {
                var sr = localRepository.OpenStreamReader(wk.uFolder + wk.filename);

                while (!sr.EndOfStream)
                {
                    string s = sr.ReadLine();
                    var strSplit = s.Split(" ");
                    pageViews.Add(new PageView(strSplit[0], strSplit[1], Int32.Parse(strSplit[2]), strSplit[3]));
                }
            });

            return pageViews;
        }
    }
}

using System;
using System.Collections.Generic;
using WikipediaChallenge.Domain.Entity;
using WikipediaChallenge.Domain.Repository;
using WikipediaChallenge.Domain.Usecase;
using System.Configuration;

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

        public (List<PageView>, Exception err) GetPageViewForPreviuosHours(int hours)
        {
            (List<PageView> pageViews, Exception err) = wikipediaRepository.GetDataFromURITemplateWithDate(DateTime.Now);

            return (pageViews, err);
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
                var filename = String.Format("pageviews-{0}-{1}.gz", day, time);

                Domain.DTO.WikipediaPageView wikipediaPageView = new(downloadURL, cFolder, uFolder, filename);
                list.Add(wikipediaPageView);
            });

            return list;
        }
    }
}

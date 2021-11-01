using System.Collections.Generic;
using WikipediaChallenge.Domain.Mapping;
using System;
using System.Linq;

namespace WikipediaChallenge.Infrastructure.Delivery
{
    public class ApplicationController
    {
        readonly Domain.Usecase.IApplication application;
        PageViewMapping mapping = new PageViewMapping();

        public ApplicationController(Domain.Usecase.IApplication app)
        {
            application = app;
        }

        public void GetDataFromLastHours(int hours)
        {
            if (hours < 1)
            {
                Console.WriteLine("Hour cannot be less than 1");
                return;
            }

            List<DateTime> datetimes = new();

            Enumerable.Range(0, hours).ToList().ForEach(hour =>
            {
                datetimes.Add(DateTime.Now.AddHours(hour * -1));
            });

            List<Domain.DTO.WikipediaPageView> wikipediaPageViewsDTO = application.FromDateTimeListRetrieveWikipediaPageViewDTOList(datetimes);

            application.DownloadWikipediaData(wikipediaPageViewsDTO);
            application.DecompressWikipediaData(wikipediaPageViewsDTO);

            var PageViewEntity = application.ProcessDecompressedWikipediaData(wikipediaPageViewsDTO);

            var groupBy = new List<string> { "domainCode", "pageTitle" };

            Dictionary<string, Domain.Entity.PageView> PageViewEntityDict = new Dictionary<string, Domain.Entity.PageView>();

            PageViewEntity.ForEach(pageView =>
            {
                string concatProperty = "";
                groupBy.ForEach(property =>
                {
                    var p = pageView.GetType().GetProperty(property);
                    var v = p.GetValue(pageView, null);
                    concatProperty += v;
                });

                if (!PageViewEntityDict.ContainsKey(concatProperty))
                {
                    PageViewEntityDict.Add(concatProperty, pageView);
                    return;
                }

                PageViewEntityDict[concatProperty].countViews += pageView.countViews;
            });

            var groupedData = PageViewEntityDict.Values.ToList()
                .OrderByDescending(it => it.countViews)
                .Take(100)
                .ToList();

            var valueData = mapping.MapPageViewFromModelToDTOList(groupedData);

            ConsoleTables.ConsoleTable.From<Domain.VO.PageView>(valueData).Write();
            return;
        }
    }
}

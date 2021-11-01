using System.Collections.Generic;
using WikipediaChallenge.Domain.Mapping;
using System;
using System.Linq;

namespace WikipediaChallenge.Infrastructure.Delivery
{
    public class ApplicationController
    {
        readonly Domain.Usecase.IWikipedia application;
        PageViewMapping mapping = new PageViewMapping();

        public ApplicationController(Domain.Usecase.IWikipedia app)
        {
            application = app;
        }

        // HELPERS
        private List<DateTime> GetDateTimeListFromIntHours(int hours)
        {
            if (hours < 1)
            {
                throw new InvalidOperationException("Hour cannot be negative");
            }

            List<DateTime> datetimes = new();

            Enumerable.Range(0, hours).ToList().ForEach(hour =>
            {
                datetimes.Add(DateTime.Now.AddHours(hour * -1));
            });

            return datetimes;
        }

        public void GetDataFromLastHours(int hours)
        {
            try
            {
                var datetimes = GetDateTimeListFromIntHours(hours);
                GetDataFromDatetimeList(datetimes);
            }
            catch (Exception err)
            {
                Console.WriteLine("Cannot retrieve data from hours " + err.Message);
            }
        }

        public void GetDataFromDatetimeList(List<DateTime> datetimes)
        {
            List<Domain.DTO.WikipediaPageView> wikipediaPageViewsDTO = application.FromDateTimeListRetrieveWikipediaPageViewDTOList(datetimes);

            ConsoleTables.ConsoleTable.From<Domain.DTO.WikipediaPageView>(wikipediaPageViewsDTO).Write();


            application.DownloadWikipediaData(wikipediaPageViewsDTO);
            application.DecompressWikipediaData(wikipediaPageViewsDTO);

            var PageViewEntity = application.ProcessDecompressedWikipediaData(wikipediaPageViewsDTO);

            Console.WriteLine("RAW DATA: " + PageViewEntity.Count);


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

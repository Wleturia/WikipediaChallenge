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
            List<DateTime> datetimes = new List<DateTime>();
            try
            {
                datetimes = GetDateTimeListFromIntHours(hours);
            }
            catch (Exception err)
            {
                Console.WriteLine("Cannot retrieve data from hours " + err.Message);
            }

            GetDataFromDatetimeList(datetimes);
        }

        public void GetDataFromDatetimeList(List<DateTime> datetimes)
        {
            IEnumerable<Domain.DTO.WikipediaPageView> wikipediaPageViewsDTO = application.FromDateTimeListRetrieveWikipediaPageViewDTOList(datetimes);

            ConsoleTables.ConsoleTable.From<Domain.DTO.WikipediaPageView>(wikipediaPageViewsDTO).Write();

            var err = application.DownloadWikipediaData(wikipediaPageViewsDTO);
            if (err != null)
            {
                Console.WriteLine("There was an error retrieving data from wikipedia source");
                return;
            }
            err = application.DecompressWikipediaData(wikipediaPageViewsDTO);
            if (err != null)
            {
                Console.WriteLine("There was an error decompressing data from local source");
                return;
            }

            var PageViewEntityList = application.ProcessDecompressedWikipediaData(wikipediaPageViewsDTO);

            var groupedData = PageViewEntityList
                .OrderByDescending(it => it.countViews)
                .Take(100);

            var valueData = mapping.MapPageViewFromModelToDTOList(groupedData);

            ConsoleTables.ConsoleTable.From<Domain.VO.PageView>(valueData).Write();
            return;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace WikipediaChallenge.Test
{
    public class ApplicationController
    {
        static string wikipediaURL = "https://dumps.wikimedia.org/other/pageviews/{0}/{0}-{1}/pageviews-{2}-{3}.gz";
        static string dfolder = "c:/tmp/compressed";
        static string ufolder = "c:/tmp/uncompressed";

        static Infrastructure.Repository.WikipediaRepository wikipediaRepository = new(wikipediaURL);
        static Infrastructure.Repository.LocalRepository localRepository = new(ufolder, dfolder);

        static Application.Usecase.Wikipedia usecase = new(wikipediaRepository, localRepository);
        Infrastructure.Delivery.ApplicationController applicationController = new(usecase);

        [Fact]
        public void RetrieveDTOListFromDateTimeList()
        {
            var d1 = new DateTime(2021, 11, 1, 00, 00, 00);

            System.Console.WriteLine(d1);

            List<DateTime> dateTimes = new List<DateTime> { d1 };


            Domain.DTO.WikipediaPageView wikipediaPageView = new(
            "https://dumps.wikimedia.org/other/pageviews/2021/2021-11/pageviews-20211101-000000.gz",
            "c:/tmp/uncompressed/2021/2021-11/",
            "c:/tmp/compressed/2021/2021-11/",
            "pageviews-20211101-000000", ".gz");

            List<Domain.DTO.WikipediaPageView> wikipediaPageViews = new List<Domain.DTO.WikipediaPageView> { wikipediaPageView };

            var resp = usecase.FromDateTimeListRetrieveWikipediaPageViewDTOList(dateTimes);

            var obj1Str = Newtonsoft.Json.JsonConvert.SerializeObject(wikipediaPageViews);
            var obj2Str = Newtonsoft.Json.JsonConvert.SerializeObject(resp);
            Assert.Equal(obj1Str, obj2Str);
        }

    }
}

using System;
using System.Collections.Generic;

namespace WikipediaChallenge.Domain.Usecase
{
    public interface IWikipedia
    {
        public IEnumerable<DTO.WikipediaPageView> FromDateTimeListRetrieveWikipediaPageViewDTOList(IEnumerable<DateTime> dateTimes);
        public Exception DownloadWikipediaData(IEnumerable<DTO.WikipediaPageView> wikipediaPageViews);
        public Exception DecompressWikipediaData(IEnumerable<DTO.WikipediaPageView> wikipediaPageViews);
        public IEnumerable<Entity.PageView> ProcessDecompressedWikipediaData(IEnumerable<DTO.WikipediaPageView> wikipediaPageViews, int qty);
    }
}

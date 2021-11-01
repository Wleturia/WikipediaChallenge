using System;
using System.Collections.Generic;

namespace WikipediaChallenge.Domain.Usecase
{
    public interface IWikipedia
    {
        public List<DTO.WikipediaPageView> FromDateTimeListRetrieveWikipediaPageViewDTOList(List<DateTime> dateTimes);
        public Exception DownloadWikipediaData(List<DTO.WikipediaPageView> wikipediaPageViews);
        public Exception DecompressWikipediaData(List<DTO.WikipediaPageView> wikipediaPageViews);
        public List<Entity.PageView> ProcessDecompressedWikipediaData(List<DTO.WikipediaPageView> wikipediaPageViews);
    }
}

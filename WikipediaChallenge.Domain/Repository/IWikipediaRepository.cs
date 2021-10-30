using System;
using System.Collections.Generic;

namespace WikipediaChallenge.Domain.Repository
{
    public interface IWikipediaRepository
    {
        public Exception DownloadDataWikipediaDTO(DTO.WikipediaPageView wikipediaPageView);

        public (List<Entity.PageView> pageViews, Exception exception) GetDataFromURITemplateWithDate(DateTime date);

        public string GetRepositoryURL();
    }
}

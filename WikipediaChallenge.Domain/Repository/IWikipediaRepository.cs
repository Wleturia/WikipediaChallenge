using System;
using System.Collections.Generic;

namespace WikipediaChallenge.Domain.Repository
{
    public interface IWikipediaRepository
    {
        public Exception DownloadDataWikipediaDTO(DTO.WikipediaPageView wikipediaPageView);
        public Exception DecompressDataWikipediaDTO(DTO.WikipediaPageView wikipediaPageView);
        public string GetRepositoryURL();
    }
}

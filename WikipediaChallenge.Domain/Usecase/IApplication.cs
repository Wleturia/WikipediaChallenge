using System;
using System.Collections.Generic;

namespace WikipediaChallenge.Domain.Usecase
{
    public interface IApplication
    {
        public List<DTO.WikipediaPageView> FromDateTimeListRetrieveWikipediaPageViewDTOList(List<DateTime> dateTimes);
    }
}

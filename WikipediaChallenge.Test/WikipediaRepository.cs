using System;
using Xunit;

namespace WikipediaChallenge.Test
{
    public class WikipediaRepository
    {
        static string wikipediaRepositoryURL = "https://dumps.wikimedia.org/other/pageviews/{0}/{0}-{1}/pageviews-{2}-{3}.gz";

        Infrastructure.Repository.WikipediaRepository wikipediaRepository = new(wikipediaRepositoryURL);

        [Fact]
        public void GetRepositoryURL()
        {
            var result = wikipediaRepository.GetRepositoryURL();
            Assert.Equal(wikipediaRepositoryURL, result);
        }
    }
}

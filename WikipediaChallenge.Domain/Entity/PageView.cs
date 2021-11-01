namespace WikipediaChallenge.Domain.Entity
{
    public class PageView
    {
        public PageView(string domainCode, string pageTitle, int countViews, string totalResponseSize)
        {
            this.domainCode = domainCode;
            this.pageTitle = pageTitle;
            this.countViews = countViews;
            this.totalResponseSize = totalResponseSize;
        }
        public string domainCode { get; set; }
        public string pageTitle { get; set; }
        public int countViews { get; set; }
        public string totalResponseSize { get; set; }
    }
}

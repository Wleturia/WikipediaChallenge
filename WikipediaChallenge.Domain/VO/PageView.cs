using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikipediaChallenge.Domain.VO
{
    public class PageView
    {
        public PageView(string domainCode, string pageTitle, int cnt)
        {
            DomainCode = domainCode;
            PageTitle = pageTitle;
            CNT = cnt;
        }

        public string DomainCode { get; set; }
        public string PageTitle { get; set; }
        public int CNT { get; set; }
    }
}

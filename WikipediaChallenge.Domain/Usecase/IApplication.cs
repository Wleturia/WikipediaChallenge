using System.Collections.Generic;

namespace WikipediaChallenge.Domain.Usecase
{
    public interface IApplication
    {
        public IEnumerable<Entity.PageView> GetPageViewForPreviuosHours(int hours);
    }
}

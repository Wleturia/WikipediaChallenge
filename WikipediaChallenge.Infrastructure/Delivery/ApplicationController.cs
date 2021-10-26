using System.Collections.Generic;
using WikipediaChallenge.Domain.VO;

namespace WikipediaChallenge.Infrastructure.Delivery
{
    public class ApplicationController
    {
        readonly Application.Usecase.Application application;

        public ApplicationController(Application.Usecase.Application app)
        {
            application = app;
        }

        public void GetData()
        {
            Stack<PageView> pageViews = new Stack<PageView>();
            PageView p1 = new("it.m", "renault", 10);
            PageView p2 = new("en", "apple", 50);
            pageViews.Push(p1);
            pageViews.Push(p2);


            application.GetPageViewForPreviuosHours(5);

            ConsoleTables.ConsoleTable.From<PageView>(pageViews).Write();
            return;
        }
    }
}

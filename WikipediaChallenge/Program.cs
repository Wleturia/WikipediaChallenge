using System;
using WikipediaChallenge.Infrastructure.Delivery;
using WikipediaChallenge.Infrastructure.Repository;

namespace WikipediaChallenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("WikipediaChallenge");

            ApplicationController ac = Initialize();

            ac.GetData();
        }

        static ApplicationController Initialize()
        {
            Repository repository = new();
            Application.Usecase.Application usecase = new(repository);
            ApplicationController applicationController = new(usecase);

            return applicationController;
        }
    }
}

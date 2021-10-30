using System;
using WikipediaChallenge.Infrastructure.Delivery;
using WikipediaChallenge.Infrastructure.Repository;

namespace WikipediaChallenge
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Console.WriteLine("WikipediaChallenge");

            ApplicationController ac = Initialize();

            ac.GetDataFromLastHours(5);
        }

        static ApplicationController Initialize()
        {
            WikipediaRepository wikipediaRepository = new(Console.Environment.PageViewBaseUrlTemplate);
            LocalRepository localRepository = new(Console.Environment.DownloadBaseFolder, Console.Environment.UncompressedBaseFolder);
            Exception err = localRepository.Initialize();
            if (err != null)
            {
                System.Console.WriteLine("Cannot initalize local repository" + err);
                System.Environment.Exit(1);
            }

            Application.Usecase.Application usecase = new(wikipediaRepository, localRepository);

            ApplicationController applicationController = new(usecase);

            return applicationController;
        }
    }
}

using System;
using System.Collections.Generic;
using WikipediaChallenge.Domain.Entity;
using WikipediaChallenge.Domain.Repository;
using WikipediaChallenge.Domain.Usecase;

namespace WikipediaChallenge.Application.Usecase
{
    public class Application : IApplication
    {
        private readonly IRepository repository;

        public Application(IRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<PageView> GetPageViewForPreviuosHours(int hours)
        {
            repository.Test();
            Console.WriteLine("Consultando data" + hours);

            return null;
        }
    }
}

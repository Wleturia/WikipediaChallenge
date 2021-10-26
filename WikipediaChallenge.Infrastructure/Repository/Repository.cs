using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WikipediaChallenge.Domain.Repository;

namespace WikipediaChallenge.Infrastructure.Repository
{
    public class Repository : IRepository
    {
        public void Test()
        {
            Console.WriteLine("Repository");
        }
    }
}

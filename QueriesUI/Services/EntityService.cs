using Projects.QueriesUI.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projects.QueriesUI.Services
{
    class EntityService : IEntityService
    {
        public string Run()
        {
            Console.WriteLine("Running entity service");
            return "";
        }
    }
}


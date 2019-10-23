using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Context;

namespace WebApi.Repositories
{
    public class CagedRepository
    {


        private readonly WebApiContext context;

        public CagedRepository()
        {
            context = new WebApiContext();
        }

        public IList<CagedModel> FindAll()
        {
            return context.Caged.ToList();
        }

    }
}

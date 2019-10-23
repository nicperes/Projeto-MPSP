using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Context;

namespace WebApi.Repositories
{
    public class CensecRepository
    {

        private readonly WebApiContext context;

        public CensecRepository()
        {
            context = new WebApiContext();
        }

        public IList<CensecModel> FindAll()
        {
            return context.Censec.ToList();
        }


    }
}

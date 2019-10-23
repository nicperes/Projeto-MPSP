using WebApi.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class CadespRepository
    {

        

        private readonly WebApiContext context;

        public CadespRepository()
        {
            context = new WebApiContext();
        }

        public IList<CadespModel> FindAll()
        {
            return context.Cadesp.ToList();
        }

    }


}


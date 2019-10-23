using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Context;

namespace WebApi.Repositories
{
    public class DetranRepository
    {

        private readonly WebApiContext context;

        public DetranRepository()
        {
            context = new WebApiContext();
        }

        public IList<DetranModel> FindAll()
        {
            return context.Detran.ToList();
        }


    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Context;

namespace WebApi.Repositories
{
    public class JucespRepository
    {

        private readonly WebApiContext context;

        public JucespRepository()
        {
            context = new WebApiContext();
        }

        public IList<JucespModel> FindAll()
        {
            return context.Jucesp.ToList();
        }

    }
}

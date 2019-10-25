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


        public void Insert(CagedModel cagedModel)
        {
            using (context)
            {
                context.Caged.Add(cagedModel);
                context.SaveChanges();

            }
        }

        public IList<CagedModel> FindById()
        {
            return context.Caged.ToList();
        }


        public IList<CagedModel> FindAll()
        {
            return context.Caged.ToList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Context;

namespace WebApi.Repositories
{
    public class SivecRepository
    {

        private readonly WebApiContext context;

        public SivecRepository()
        {
            context = new WebApiContext();
        }

        public void Insert(SivecModel sivecModel)
        {
            using (context)
            {
                context.Sivec.Add(sivecModel);
                context.SaveChanges();

            }
        }

        public IList<SivecModel> FindById()
        {
            return context.Sivec.ToList();
        }

        public IList<SivecModel> FindAll()
        {
            return context.Sivec.ToList();
        }


    }
}

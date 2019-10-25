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


        public void Insert(JucespModel jucespModel)
        {
            using (context)
            {
                context.Jucesp.Add(jucespModel);
                context.SaveChanges();

            }
        }

        public IList<JucespModel> FindById()
        {
            return context.Jucesp.ToList();
        }


        public IList<JucespModel> FindAll()
        {
            return context.Jucesp.ToList();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Context;

namespace WebApi.Repositories
{
    public class SielRepository
    {

        private readonly WebApiContext context;

        public SielRepository()
        {
            context = new WebApiContext();
        }


        public void Insert(SielModel sielModel)
        {
            using (context)
            {
                context.Siel.Add(sielModel);
                context.SaveChanges();

            }
        }

        public IList<SielModel> FindById()
        {
            return context.Siel.ToList();
        }

        public IList<SielModel> FindAll()
        {
            return context.Siel.ToList();
        }


    }
}

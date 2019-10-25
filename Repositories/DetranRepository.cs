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


        public void Insert(DetranModel detranModel)
        {
            using (context)
            {
                context.Detran.Add(detranModel);
                context.SaveChanges();

            }
        }

        public IList<DetranModel> FindById()
        {
            return context.Detran.ToList();
        }



        public IList<DetranModel> FindAll()
        {
            return context.Detran.ToList();
        }


    }

}

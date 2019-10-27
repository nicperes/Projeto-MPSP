using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
using WebApi.Repositories.Context;
namespace WebApi.Repositories
{
    public class ArpenspRepository
    {

        private readonly WebApiContext  context;

        public ArpenspRepository()
        {
            context = new WebApiContext();
        }

        public void Insert(ArpenspModel arpenspModel)
        {
            using (context)
            {
                context.Arpensp.Add(arpenspModel);
                context.SaveChanges();

            }
        }


        public ArpenspModel FindById(int id)
        {
            var pessoa = context.Arpensp
                     //.Include(a => a.CadespModel)
                     //.Include(b => b.JucespModel)
                     //.Include(c => c.CensecModel)
                     //.Include(d => d.CagedModel)
                     //.Include(d => d.SielModel)
                     //.Include(d => d.SivecModel)
                     .SingleOrDefault(p => p.CNPJCPFArpensp == id);

            return pessoa;

        }


        public List<ArpenspModel> FindAll()
        {
            List<ArpenspModel> listaPessoas;
            using (context)
            {

                listaPessoas = context.Arpensp
                    //.Include(a => a.CadespModel)
                    // .Include(b => b.JucespModel)
                    // .Include(c => c.CensecModel)
                    // .Include(d => d.CagedModel)
                    //  .Include(d => d.SielModel)
                    // .Include(d => d.SivecModel)
                    .ToList();
            }


            return listaPessoas;
        }

        public void Update(ArpenspModel arpenspModel)
        {
            context.Arpensp.Update(arpenspModel);
            context.SaveChanges();
        }


      


    }


}


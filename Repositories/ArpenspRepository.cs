using Microsoft.EntityFrameworkCore;
using WebApi.Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;
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
                     .Include(a => a.CadespModel)
                     .Include(b => b.JucespModel)
                     .Include(c => c.CensecModel)
                     .Include(d => d.CagedModel)
                     .Include(d => d.DetranModel)
                     .SingleOrDefault(p => p.CNPJCPFArpensp == id);

            return pessoa;

        }


        public IList<ArpenspModel> FindAll()
        {
            IList<ArpenspModel> listaPessoas;
            using (context)
            {

                listaPessoas = context.Arpensp
                    .Include(a => a.CadespModel)
                     .Include(b => b.JucespModel)
                     .Include(c => c.CensecModel)
                     .Include(d => d.CagedModel)
                     .Include(a => a.DetranModel)
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


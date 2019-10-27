using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.Repositories.Context;

namespace WebApi.Repositories
{
    

    public class ConsultaAnteriorRepository
    {
        private readonly WebApiContext context;

        public ConsultaAnteriorRepository()
        {
            context = new WebApiContext();
        }

        public void Insert(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            using (context)
            {
                context.PesquisaCPFCNPJ.Add(pesquisaCPFCNPJ);
                context.SaveChanges();

            }
        }

        public List<PesquisaCPFCNPJ> FindAll()
        {
            List<PesquisaCPFCNPJ> listaConsultas;

            using (context)
            {
                
                listaConsultas = context.PesquisaCPFCNPJ
                    .ToList();
                
            }

            

            return listaConsultas;
        }

    }
}
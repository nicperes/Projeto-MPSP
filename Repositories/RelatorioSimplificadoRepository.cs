using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;
using WebApi.Scraping;
using WebApi.Controllers;

namespace WebApi.Repositories
{
    public class RelatorioSimplificadoRepository
    {
        private readonly WebScrapingArpensp webScrapingArpensp;
        

        public RelatorioSimplificadoRepository()
        {
            webScrapingArpensp = new WebScrapingArpensp();
            
        }

        public ArpenspModel Simples(string arpensp)
        {
            ArpenspModel arpenspModel = JsonConvert.DeserializeObject<ArpenspModel>(arpensp);
            
            return arpenspModel;
        }
        
    }
}
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
        private readonly WebScraping webScrapingArpensp;
        

        public RelatorioSimplificadoRepository()
        {
            webScrapingArpensp = new WebScraping();
            
        }

        public ArpenspModel SimplesArpensp(string arpensp)
        {
            ArpenspModel arpenspModel = JsonConvert.DeserializeObject<ArpenspModel>(arpensp);
            
            return arpenspModel;
        }

        public CadespModel SimplesCadesp(string cadesp)
        {
            CadespModel cadespModel = JsonConvert.DeserializeObject<CadespModel>(cadesp);

            return cadespModel;
        }

        public JucespModel SimplesJucesp(string jucesp)
        {
            JucespModel jucespModel = JsonConvert.DeserializeObject<JucespModel>(jucesp);

            return jucespModel;
        }

        public CagedModel SimplesCaged(string caged)
        {
            CagedModel cagedModel = JsonConvert.DeserializeObject<CagedModel>(caged);

            return cagedModel;
        }

        public DetranModel SimplesDetran(string detran)
        {
            DetranModel detranModel = JsonConvert.DeserializeObject<DetranModel>(detran);

            return detranModel;
        }

        public CensecModel SimplesCensec(string censec)
        {
            CensecModel censecModel = JsonConvert.DeserializeObject<CensecModel>(censec);

            return censecModel;
        }
    }
}
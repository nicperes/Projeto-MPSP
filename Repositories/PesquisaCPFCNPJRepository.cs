using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;
using WebApi.Models;
using WebApi.Scraping;

namespace WebApi.Repositories
{
    public class PesquisaCPFCNPJRepository
    {
        private readonly WebScraping webScrapingArpensp;

        public PesquisaCPFCNPJRepository()
        {
            webScrapingArpensp = new WebScraping();
        }
        /*
       public JsonResult WebScrapingArpenp(PesquisaCPFCNPJ cpfcnpj)
       {

          string objjsonData = webScrapingArpensp();
               Response.Write(objjsonData);

               return Json(objjsonData, JsonRequestBehavior.AllowGet);

          
    } */
    }
}
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.IO;
using System.Web.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using System.Dynamic;
using Facebook;
using WebApi.Models;
using java.io;
using org.apache.pdfbox.pdfparser;
using org.apache.pdfbox.util;
using java.net;
using Newtonsoft.Json;
using Microsoft.Ajax.Utilities;
using WebApi.Repositories;
using WebApi.Scraping;
//using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidacaoRepository validacaoRepository;
        private readonly WebScrapingArpensp webScrapingArpensp;
        private readonly WebScrapingCadesp webScrapingCadesp;
        private readonly RelatorioSimplificadoRepository relatorioSimplificadoRepository;
        
        public HomeController()
        {
            validacaoRepository = new ValidacaoRepository();
            webScrapingArpensp = new WebScrapingArpensp();
            webScrapingCadesp = new WebScrapingCadesp();
            relatorioSimplificadoRepository = new RelatorioSimplificadoRepository();
        }

        [HttpGet]
        public  ActionResult Login()
        {
            
            return View(new LoginModel());

        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            bool validacao = validacaoRepository.Validacao(loginModel);

            if (validacao == false)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                //Ação após login sucesso
                //return View(loginModel);
                return RedirectToAction("Menu", "Home");
            }
        }


        [HttpGet]
        public ActionResult Menu()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PesquisaCPFCNPJ()
        {
            return View(new PesquisaCPFCNPJ());
        }

        [HttpPost]
        public ActionResult PesquisaCPFCNPJ(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            return RedirectToAction("RelatorioSimplificado",pesquisaCPFCNPJ);
        }

        [HttpGet]
        public ActionResult RelatorioSimplificado(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string arpensp = webScrapingArpensp.Arpensp(pesquisaCPFCNPJ);
            string cadesp = webScrapingCadesp.Cadesp(pesquisaCPFCNPJ);
            ArpenspModel arpenspModel = relatorioSimplificadoRepository.SimplesArpensp(arpensp);
            CadespModel cadespModel = relatorioSimplificadoRepository.SimplesCadesp(cadesp);

            var tuple = new Tuple<ArpenspModel, CadespModel>(arpenspModel, cadespModel);
            return View(tuple);
        }

        
        public ActionResult RelatorioPdf()
        {

            //relatorioSimplificadoRepository.RelatorioPDF();

            return RedirectToAction("Menu");
        }



        //Conversão do PDF
        public void ReadPDF()
        {

            IWebDriver driver = new ChromeDriver();

            URL TestURL = new URL(driver.Url);

            BufferedInputStream TestFile = new BufferedInputStream(TestURL.openStream());
            PDFParser TestPDF = new PDFParser(TestFile);
            TestPDF.parse();
            String TestText = new PDFTextStripper().getText(TestPDF.getPDDocument());
            System.IO.File.WriteAllText(@"C:\Users\Nicolas PC\Desktop\teste\PDFTESTE.txt", TestText);
        }

        

        public void WebSrapingArisp()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                /*driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/ ");
                driver.FindElement(By.Id("username")).SendKeys("fiap");
                driver.FindElement(By.Id("password")).SendKeys("mpsp");
                driver.FindElement(By.Id("password")).SendKeys(Keys.Enter);
                */

                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arisp/login.html");
                driver.FindElement(By.Id("btnCallLogin")).Click();
                driver.FindElement(By.Id("btnAutenticar")).Click();
                //driver.FindElement(By.XPath("//*[@id='liInstituicoes']/div/ul/li[3]")).Click();
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arisp/pagina4-tipo-de-pesquisa.html");
                driver.FindElement(By.Id("Prosseguir")).Click();
                driver.FindElement(By.XPath("//*[@id='main']/div[3]/div[2]/div[1]/div/div[2]/div/input")).Click();

                //Scroll para baixo
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                jse.ExecuteScript("window.scrollBy(0,250);");

                driver.FindElement(By.XPath("//*[@id='chkHabilitar']")).Click();
                driver.FindElement(By.Id("Prosseguir")).Click();
                driver.FindElement(By.Id("filterDocumento")).SendKeys("123456789");
                driver.FindElement(By.Id("btnPesquisar")).Click();

                var table = driver.FindElement(By.TagName("tbody"));
                var rows = table.FindElements(By.TagName("tr"));

                int count = 0;

                foreach (var row in rows)
                {
                    count++;

                }

                string texto = driver.FindElement(By.TagName("label")).Text;

                for (int i = 0; i > count; i++)
                {
                    if (texto == "Foi pesquisado, encontramos ocorrência(s), a base de dados está atualizada.")
                    {
                        var teste = 0;
                    }

                }

                //var texto = driver.FindElement(By.TagName("label"));


                /*for(int i = 0; i > count; i++)-
                {
                        driver.FindElement(By.Name("chkCidades")).Click();
                    
                }*/

                driver.FindElement(By.Id("btnProsseguir")).Click();

            }

        }

        public JsonResult WebScrapingJucesp()
        {

            using (IWebDriver driver = new ChromeDriver())
            {
                /*driver.Manage().Window.Maximize();
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/ ");
                driver.FindElement(By.Id("username")).SendKeys("fiap");
                driver.FindElement(By.Id("password")).SendKeys("mpsp");
                driver.FindElement(By.Id("password")).SendKeys(Keys.Enter);
                */

                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/jucesp/index.html");
                driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_txtPalavraChave")).SendKeys("google");
                driver.FindElement(By.XPath("//*[@id='ctl00_cphContent_frmBuscaSimples_pnlBuscaSimples']/table/tbody/tr/td[2]/input")).Click();

                driver.FindElement(By.XPath("//*[@id='formBuscaAvancada']/table/tbody/tr[1]/td/div/div[2]/label/input")).SendKeys("Q8TJA");
                driver.FindElement(By.ClassName("btcadastro")).Click();

                var tables = driver.FindElement(By.XPath("//*[@id='ctl00_cphContent_gdvResultadoBusca_gdvContent']/tbody"));
                var rows = tables.FindElements(By.TagName("tr"));

                var count = -1;

                foreach (var row in rows)
                {
                    count++;

                }
                driver.FindElement(By.XPath("//*[@id='ctl00_cphContent_gdvResultadoBusca_gdvContent']/tbody/tr/td")).Click();

                var resultadoFinal = driver.FindElement(By.Id("dados")).Text;

                string[] strsplit = resultadoFinal.Replace("\r\n", ":").Split(':');

                string data = strsplit[1].Replace("17", "");
                string nome = strsplit[4];
                string nMatriz = strsplit[7];
                string tipoEmpresa = strsplit[12];
                string dataConst = strsplit[14];
                string inicioAtiv = strsplit[16];
                string cnpj = strsplit[18];
                string capital = strsplit[26];
                string logradouro = strsplit[28];
                string numero = strsplit[30];
                string complemento = strsplit[34];
                string bairro = strsplit[32];
                string municipio = strsplit[36];
                string cep = strsplit[38];
                string uf = strsplit[40];

                JucespModel objJu = new JucespModel();
                objJu.Data = data;
                objJu.Nome = nome;
                objJu.NumMatriz = nMatriz;
                objJu.TipoEmpresa = tipoEmpresa;
                objJu.DataConst = dataConst;
                objJu.InicioAtiv = inicioAtiv;
                objJu.CNPJ = cnpj;
                objJu.Capital = capital;
                objJu.Logradouro = logradouro;
                objJu.Numero = numero;
                objJu.Complemento = complemento;
                objJu.Bairro = bairro;
                objJu.Municipio = municipio;
                objJu.Cep = cep;
                objJu.Uf = uf;
                string objjsonData = JsonConvert.SerializeObject(objJu);
                Response.Write(objjsonData);

                System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Jucesp.txt", objjsonData);

                return Json(objjsonData, JsonRequestBehavior.AllowGet);
            }
        }


    }
}

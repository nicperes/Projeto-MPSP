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
using org.apache.pdfbox.io;
using org.apache.pdfbox.cos;
using org.apache.pdfbox.pdmodel;
//using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidacaoRepository validacaoRepository;
        private readonly WebScraping webScraping;
        private readonly RelatorioSimplificadoRepository relatorioSimplificadoRepository;
        
        public HomeController()
        {
            validacaoRepository = new ValidacaoRepository();
            webScraping = new WebScraping();
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
            string arpensp = "";
            string cadesp = "";
            string caged = "";
            string juscesp = "";
            if (pesquisaCPFCNPJ.Arpensp == "on"){
                arpensp = webScraping.Arpensp(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Cadesp == "on"){
                cadesp = webScraping.Cadesp(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Caged == "on"){
                caged = webScraping.Caged(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Jucesp == "on"){
                juscesp = webScraping.Jucesp(pesquisaCPFCNPJ);
            }
            

            ArpenspModel arpenspModel = relatorioSimplificadoRepository.SimplesArpensp(arpensp);
            CadespModel cadespModel = relatorioSimplificadoRepository.SimplesCadesp(cadesp);
            JucespModel jucespModel = relatorioSimplificadoRepository.SimplesJucesp(juscesp);
            CagedModel cagedModel = relatorioSimplificadoRepository.SimplesCaged(caged);

            var tuple = new Tuple<ArpenspModel, CadespModel, JucespModel, CagedModel>(arpenspModel, cadespModel, jucespModel,cagedModel);
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

        public void Censec()
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/censec/login.html");

                driver.FindElement(By.Id("EntrarButton")).Click();
                driver.FindElement(By.Id("menucentrais")).Click();
                driver.FindElement(By.XPath("//*[@id='ctl00_CESDILi']/a")).Click();
                driver.FindElement(By.XPath("//*[@id='ctl00_CESDIConsultaAtoHyperLink']")).Click();
                driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DocumentoTextBox")).SendKeys("21321321323");
                driver.FindElement(By.ClassName("BT_Buscar")).SendKeys(Keys.Enter);

                driver.FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_ResultadoBuscaGeralPanel']/div[2]/div[1]/div/table/tbody/tr[2]/td[1]/input")).Click();
                driver.FindElement(By.ClassName("BT_Buscar")).SendKeys(Keys.Enter);

                string carga = driver.FindElement(By.XPath("//*[@id='aspnetForm']/div[5]/div/div[3]/div[2]/div[3]/div[1]/div")).Text;
                string mes = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_MesReferenciaDropDownList")).Text;
                string ano = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_AnoReferenciaDropDownList")).Text;
                string ato = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_TipoAtoDropDownList")).Text;
                string diaAto = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DiaAtoTextBox")).Text;
                string mesAto = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_MesAtoTextBox")).Text;
                string anoAto = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_AnoAtoTextBox")).Text;
                string livro  = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_LivroTextBox")).Text;
                string folha = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_FolhaTextBox")).Text;

                string nomes = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_PartesUpdatePanel")).Text;

                string uf = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DadosCartorio_CartorioUFTextBox")).Text;
                string municipio = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DadosCartorio_CartorioMunicipioTextBox")).Text;
                string cartorio = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DadosCartorio_CartorioNomeTextBox")).Text;
                string tabela = driver.FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView']/div/table")).Text;

                CensecModel objCen = new CensecModel();
                objCen.Carga = carga;
                objCen.Mes = mes;
                objCen.Ano = ano;
                objCen.Ato = ato;
                objCen.DiaAto = diaAto;
                objCen.MesAto = mesAto;
                objCen.AnoAto = anoAto;
                objCen.Livro = livro;
                objCen.Folha = folha;

                string objjsonData = JsonConvert.SerializeObject(objCen);

                System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Censec.txt", objjsonData);
                

            }
        }

        public void Detran()
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            using (IWebDriver driver = new ChromeDriver())
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/login.html");
                driver.FindElement(By.Id("form:j_id563205015_44efc15b")).Click();
                driver.FindElement(By.Id("navigation_a_M_16")).Click();
                driver.FindElement(By.XPath("//*[@id='navigation_a_F_16']")).Click();
                driver.FindElement(By.Id("form:rg")).SendKeys("524390045");
                driver.FindElement(By.Id("form:nome")).SendKeys("Joao");
                driver.FindElement(By.LinkText("Pesquisar")).Click();

                driver.SwitchTo().Window(driver.WindowHandles[1]);
                URL url = new URL(driver.Url);
                //https://stackoverflow.com/questions/3563147/can-selenium-verify-text-inside-a-pdf-loaded-by-the-browser
                //https://stackoverflow.com/questions/32515883/how-to-verify-highlighted-text-present-in-pdf-using-selenium-webdriver
                java.net.Proxy proxy = new java.net.Proxy(java.net.Proxy.Type.HTTP, new InetSocketAddress("177.54.218.140", 8080));
                URLConnection urlc = url.openConnection(proxy);
                urlc.addRequestProperty("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.90 Safari/537.36");
                BufferedInputStream fileToParse = new BufferedInputStream(urlc.getInputStream());
                //RandomAccessBufferedFileInputStream fileToParse = new RandomAccessBufferedFileInputStream(urlc.getInputStream());
                PDFParser parser = new PDFParser(fileToParse);
                parser.parse();
                COSDocument cosDoc = parser.getDocument();
                PDDocument pdDoc = new PDDocument(cosDoc);
                PDFTextStripper pdfStripper = new PDFTextStripper();
                pdfStripper.setStartPage(1);
                pdfStripper.setEndPage(1);
                string parsedText = pdfStripper.getText(pdDoc);

                string saida = new PDFTextStripper().getText(parser.getPDDocument());
                System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Detran.txt", saida);
            }
        }
    }
}

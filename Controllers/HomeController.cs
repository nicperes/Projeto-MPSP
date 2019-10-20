using OpenQA.Selenium.Chrome;
using System;
using System.Web.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebApi.Models;
using java.io;
using org.apache.pdfbox.pdfparser;
using java.net;
using Newtonsoft.Json;
using WebApi.Repositories;
using WebApi.Scraping;
using org.apache.pdfbox.cos;
using org.apache.pdfbox.pdmodel;
using org.apache.pdfbox.util;

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
            //Adicionar no Banco de Dados o CPF/CNPJ e o horario
            return RedirectToAction("RelatorioSimplificado",pesquisaCPFCNPJ);
        }

        [HttpGet]
        public ActionResult RelatorioSimplificado(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string arpensp = "";
            string cadesp = "";
            string caged = "";
            string censec = "";
            string detran = "";
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
            if (pesquisaCPFCNPJ.Censec == "on")
            {
                censec = webScraping.Censec(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Jucesp == "on"){
                juscesp = webScraping.Jucesp(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Detran == "on"){
                detran = webScraping.Detran(pesquisaCPFCNPJ);
            }
            

            ArpenspModel arpenspModel = relatorioSimplificadoRepository.SimplesArpensp(arpensp);
            CadespModel cadespModel = relatorioSimplificadoRepository.SimplesCadesp(cadesp);
            JucespModel jucespModel = relatorioSimplificadoRepository.SimplesJucesp(juscesp);
            CagedModel cagedModel = relatorioSimplificadoRepository.SimplesCaged(caged);
            DetranModel detranModel = relatorioSimplificadoRepository.SimplesDetran(detran);
            CensecModel censecModel = relatorioSimplificadoRepository.SimplesCensec(censec);

            //var tuple = new Tuple<ArpenspModel, CadespModel, JucespModel, CagedModel, Tuple<DetranModel>>(arpenspModel,cadespModel,jucespModel,cagedModel,Tuple.Create(detranModel));

            return View(new PesquisaCPFCNPJ() {ArpenspModel = arpenspModel, CadespModel = cadespModel, JucespModel = jucespModel, CagedModel = cagedModel, DetranModel = detranModel, CensecModel = censecModel });
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
            //String TestText = new PDFTextStripper().getText(TestPDF.getPDDocument());
            //System.IO.File.WriteAllText(@"C:\Users\Nicolas PC\Desktop\teste\PDFTESTE.txt", TestText);
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


    }
}

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
using HiQPdf;
using System.IO;
using StringWriter = System.IO.StringWriter;
using System.Web.UI;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

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
        public ActionResult Login()
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
            return RedirectToAction("RelatorioSimplificado", pesquisaCPFCNPJ);
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
            string siel = "";
            string sivec = "";

            if (pesquisaCPFCNPJ.Arpensp == "on")
            {
                arpensp = webScraping.Arpensp(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Cadesp == "on")
            {
                cadesp = webScraping.Cadesp(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Caged == "on")
            {
                caged = webScraping.Caged(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Censec == "on")
            {
                censec = webScraping.Censec(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Jucesp == "on")
            {
                juscesp = webScraping.Jucesp(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Detran == "on")
            {
                detran = webScraping.Detran(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Siel == "on")
            {
                siel = webScraping.Siel(pesquisaCPFCNPJ);
            }
            if (pesquisaCPFCNPJ.Sivec == "on")
            {
                sivec = webScraping.Sivec(pesquisaCPFCNPJ);
            }



            ArpenspModel arpenspModel = relatorioSimplificadoRepository.SimplesArpensp(arpensp);
            CadespModel cadespModel = relatorioSimplificadoRepository.SimplesCadesp(cadesp);
            JucespModel jucespModel = relatorioSimplificadoRepository.SimplesJucesp(juscesp);
            CagedModel cagedModel = relatorioSimplificadoRepository.SimplesCaged(caged);
            DetranModel detranModel = relatorioSimplificadoRepository.SimplesDetran(detran);
            CensecModel censecModel = relatorioSimplificadoRepository.SimplesCensec(censec);
            SielModel sielModel = relatorioSimplificadoRepository.SimplesSiel(siel);
            SivecModel sivecModel = relatorioSimplificadoRepository.SimplesSivec(sivec);

            return View(new PesquisaCPFCNPJ() { ArpenspModel = arpenspModel, CadespModel = cadespModel, JucespModel = jucespModel, CagedModel = cagedModel, DetranModel = detranModel, CensecModel = censecModel, SielModel = sielModel, SivecModel = sivecModel });
        }

        //public ActionResult PrintPDF(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        //{
        //    string result = RenderRazorViewToString("ViewName", model);
        //    return RedirectToAction("PesquisaCPFCNPJ");
        //}

        //public static class RazorViewToString
        //{
        //    public static string RenderRazorViewToString(this Controller controller, string viewName, object model)
        //    {
        //        controller.ViewData.Model = model;
        //        using (var sw = new StringWriter())
        //        {
        //            var viewResult = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
        //            var viewContext = new ViewContext(controller.ControllerContext, viewResult.View, controller.ViewData, controller.TempData, sw);
        //            viewResult.View.Render(viewContext, sw);
        //            viewResult.ViewEngine.ReleaseView(controller.ControllerContext, viewResult.View);
        //            return sw.GetStringBuilder().ToString();
        //        }
        //    }
        //}

        public void Arisp()
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
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

                string texto = driver.FindElement(By.LinkText("Foi pesquisado, encontramos ocorrência(s), a base de dados está atualizada.")).Text;

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



        public void Infocrim()
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            using (IWebDriver driver = new ChromeDriver())
            {

                Actions builder = new Actions(driver);

                //Validação
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/ ");
                driver.FindElement(By.Id("username")).SendKeys("fiap");
                driver.FindElement(By.Id("password")).SendKeys("mpsp");
                driver.FindElement(By.Id("password")).SendKeys(Keys.Enter);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/infocrim/login.html");
                driver.FindElement(By.XPath("/html/body/table/tbody/tr[3]/td/table/tbody/tr/td/table/tbody/tr/td/table/tbody/tr[2]/td[4]/a/img")).Click();
                driver.FindElement(By.XPath("/html/body/a/table[3]/tbody/tr/td[2]/table[1]/tbody/tr[3]/td/table/tbody/tr[2]/td/table/tbody/tr/td/div/a/img")).Click();
                driver.FindElement(By.XPath("/html/body/table/tbody/tr[2]/td/table[3]/tbody/tr[2]/td[2]/a")).Click();
                driver.FindElement(By.XPath("/html/body/table/tbody/tr/td/a[2]/img")).Click();
                driver.FindElement(By.XPath("/html/body/print-preview-app//print-preview-sidebar//div[2]/print-preview-destination-settings//print-preview-settings-section[1]/div/print-preview-destination-select//select")).Click();
                driver.FindElement(By.XPath("/html/body/print-preview-app//print-preview-sidebar//div[2]/print-preview-destination-settings//print-preview-settings-section[1]/div/print-preview-destination-select//select/option[2]")).Click();

                URL url = new URL(driver.Url);
                BufferedInputStream fileToParse = new BufferedInputStream(url.openStream());
                PDFParser parser = new PDFParser(fileToParse);
                parser.parse();
                COSDocument cosDoc = parser.getDocument();
                PDDocument pdDoc = new PDDocument(cosDoc);
                PDFTextStripper pdfStripper = new PDFTextStripper();
                pdfStripper.setStartPage(1);
                pdfStripper.setEndPage(1);
                string parsedText = pdfStripper.getText(cosDoc);

                string saida = new PDFTextStripper().getText(parser.getPDDocument());

                System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Infocrim.txt", saida);
            }
        }

        [HttpPost]
        public string Arpensp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string arpensp = webScraping.Arpensp(pesquisaCPFCNPJ);

            return arpensp;
        }

        public string Cadesp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string cadesp = webScraping.Cadesp(pesquisaCPFCNPJ);

            return cadesp;
        }

        public string Caged(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string caged = webScraping.Caged(pesquisaCPFCNPJ);

            return caged;
        }

        public string Censec(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string censec = webScraping.Censec(pesquisaCPFCNPJ);

            return censec;
        }

        public string Jucesp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string jucesp = webScraping.Jucesp(pesquisaCPFCNPJ);

            return jucesp;
        }

        public string Detran(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string detran = webScraping.Detran(pesquisaCPFCNPJ);

            return detran;
        }

        public string Siel(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string siel = webScraping.Siel(pesquisaCPFCNPJ);

            return siel;
        }

        public string Sivec(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            string sivec = webScraping.Sivec(pesquisaCPFCNPJ);

            return sivec;
        }

    }
}

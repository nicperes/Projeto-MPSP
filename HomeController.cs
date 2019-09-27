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
//using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly ValidacaoRepository validacaoRepository;

        public HomeController()
        {
            validacaoRepository = new ValidacaoRepository();
        }

        [HttpGet]
        public  ActionResult Login()
        {
            
            return View(new LoginModel());

        }

        [System.Web.Http.HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            
                bool validacao = validacaoRepository.Validacao(loginModel);

                if (validacao == false)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    //return View(loginModel);
                    return RedirectToAction("Index", "Home");
            }
            
            
        }


        [HttpGet]
        //localhost:49850/home/WebScrapingArpenp
        //Arpensp
        public JsonResult WebScrapingArpenp()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                /*driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/ ");
                driver.FindElement(By.Id("username")).SendKeys("fiap");
                driver.FindElement(By.Id("password")).SendKeys("mpsp");
                driver.FindElement(By.Id("password")).SendKeys(Keys.Enter);
                */
                //driver.Manage().Window.Maximize();
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/login.html");

                driver.FindElement(By.XPath("//*[@id='main']/div[2]/div[2]/div[2]/div/a/img")).Click();
                //System.Threading.Thread.Sleep(500);
                driver.FindElement(By.ClassName("item3")).Click();
                //System.Threading.Thread.Sleep(500);
                driver.FindElement(By.XPath("//*[@id='wrapper']/ul/li[2]/ul/li[1]/a")).Click();
                driver.FindElement(By.XPath("//*[@id='principal']/div/form/table/tbody/tr[9]/td[2]/input")).SendKeys("Teste");
                driver.FindElement(By.ClassName("botao")).SendKeys(Keys.Enter);

                var resultado = driver.FindElement(By.ClassName("principal")).Text;

                string[] strsplit = resultado.Replace("\r\n",":").Split(':');

                string cartorioRegistro = strsplit[3];
                string numeroCNS = strsplit[5];
                string uf = strsplit[7];
                string nomeConj = strsplit[10];
                string novoNomeConj = strsplit[12];
                string nomeConj2 = strsplit[14];
                string novoNomeConj2 = strsplit[16];
                string dataCasamento = strsplit[18];
                string matricula = strsplit[20];
                string dataEntrada = strsplit[22];
                string dataRegistro = strsplit[24];

                ArpenspModel objArp = new ArpenspModel();
                objArp.CartorioRegistro = cartorioRegistro;
                objArp.NumCNS = numeroCNS;
                objArp.UF = uf;
                objArp.NomeConj = nomeConj;
                objArp.NovoNomeConj = novoNomeConj;
                objArp.NomeConj2 = nomeConj2;
                objArp.NovoNomeConj2 = novoNomeConj2;
                objArp.DataCasamento = dataCasamento;
                objArp.Matricula = matricula;
                objArp.DataEntrada = dataEntrada;
                objArp.DataRegistro = dataRegistro;
                
                string objjsonData = JsonConvert.SerializeObject(objArp, new JsonSerializerSettings { Formatting = Formatting.Indented });
                
                Response.Write(objjsonData);
                //System.IO.File.WriteAllText(@"C:\Users\Nicolas PC\Desktop\teste\Arpensp.txt", objjsonData);
                System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Arpensp.txt", objjsonData);
                return Json(objjsonData, JsonRequestBehavior.AllowGet);

            }
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

        //Cadesp
        public JsonResult WebScrapingCadesp()
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

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/cadesp/login.html");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_UserName")).SendKeys("fiap");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_Password")).SendKeys("mpsp");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_Password")).SendKeys(Keys.Enter);

                //driver.FindElement(By.XPath("//*[@id='ctl00_menuPlaceHolder_menuControl1_LoginView1_menuSuperiorn1']/table/tbody/tr/td/a")).Click();

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/cadesp/pagina3-pesquisa.html");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_tcConsultaCompleta_TabPanel1_txtIdentificacao")).SendKeys("54545454545454");

                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_tcConsultaCompleta_TabPanel1_btnConsultarEstabelecimento")).SendKeys(Keys.Enter);

                var resultado1 = driver.FindElement(By.XPath("//*[@id='ctl00_conteudoPaginaPlaceHolder_dlCabecalho']/tbody/tr/td/table")).Text;

                var resultado = driver.FindElement(By.XPath("//*[@id='ctl00_conteudoPaginaPlaceHolder_dlEstabelecimentoGeral']/tbody/tr[2]/td")).Text;

                var resultadoFinal = resultado1 + resultado;

                string[] strsplit = resultadoFinal.Replace("\r\n", ":").Split(':');

                string ie = strsplit[1].Replace("Situação", "");
                string situacao = strsplit[2];
                string cnpj = strsplit[4].Replace("Data da Inscrição no Estado", "");
                string dataInscricao = strsplit[5];
                string nomeEmpresarial = strsplit[7].Replace("Regime Estadual", "");
                string regimeEstadual = strsplit[8];
                string drt = strsplit[10].Replace("Posto Fiscal", "");
                string postoFiscal = strsplit[11];
                string nire = strsplit[21];
                string ocorrenciaFiscal = strsplit[26];
                string tipoUnidade = strsplit[28].Replace("Formas de Atuação", "");
                string formaAtuacao = strsplit[30];

                CadespModel objCad = new CadespModel();

                objCad.IE = ie;
                objCad.Situacao = situacao;
                objCad.CNPJ = cnpj;
                objCad.DataInscricao = dataInscricao;
                objCad.NomeEmpresarial = nomeEmpresarial;
                objCad.RegimeEstadual = regimeEstadual;
                objCad.DRT = drt;
                objCad.PostoFiscal = postoFiscal;
                objCad.Nire = nire;
                objCad.OcorrenciaFiscal = ocorrenciaFiscal;
                objCad.TipoUnidade = tipoUnidade;
                objCad.FormasAtuacao = formaAtuacao;

                string objjsonData = JsonConvert.SerializeObject(objCad, new JsonSerializerSettings { Formatting = Formatting.Indented });

                Response.Write(objjsonData);

                //System.IO.File.WriteAllText(@"C:\Users\Nicolas PC\Desktop\teste\Cadesp.txt", resultado1 + resultado);
                System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Cadesp.txt", objjsonData);

                return Json(objjsonData, JsonRequestBehavior.AllowGet);
            }


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

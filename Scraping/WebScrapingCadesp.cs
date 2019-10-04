using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Scraping
{
    public class WebScrapingCadesp
    {
        //Cadesp
        public string Cadesp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            { 
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

                //System.IO.File.WriteAllText(@"C:\Users\Nicolas PC\Desktop\teste\Cadesp.txt", resultado1 + resultado);
                //System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Cadesp.txt", objjsonData);

                return objjsonData;
            }


        }
    }
}
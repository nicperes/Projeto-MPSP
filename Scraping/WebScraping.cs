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
using OpenQA.Selenium.Remote;

namespace WebApi.Scraping
{
    public class WebScraping
    {

        [HttpPost]
        public string Arpensp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/login.html");

                driver.FindElement(By.XPath("//*[@id='main']/div[2]/div[2]/div[2]/div/a/img")).Click();
                driver.FindElement(By.ClassName("item3")).Click();
                driver.FindElement(By.XPath("//*[@id='wrapper']/ul/li[2]/ul/li[1]/a")).Click();
                driver.FindElement(By.XPath("//*[@id='principal']/div/form/table/tbody/tr[10]/td[2]/input")).SendKeys(pesquisaCPFCNPJ.CPFCNPJ);
                driver.FindElement(By.ClassName("botao")).SendKeys(Keys.Enter);

                var resultado = driver.FindElement(By.ClassName("principal")).Text;

                string[] strsplit = resultado.Replace("\r\n", ":").Split(':');

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

                return objjsonData;

            }
        }

        //Cadesp
        public string Cadesp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            using (IWebDriver driver = new ChromeDriver(options))
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

                return objjsonData;
            }


        }

        //Jucesp
        public string Jucesp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            using (IWebDriver driver = new ChromeDriver(options))
            {
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
                string objjsonData = JsonConvert.SerializeObject(objJu, new JsonSerializerSettings { Formatting = Formatting.Indented });
                
                //System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Jucesp.txt", objjsonData);

                return objjsonData;
            }
        }

    }
}
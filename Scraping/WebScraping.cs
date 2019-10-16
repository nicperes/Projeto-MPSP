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
using java.sql;

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

                string cartorioRegistro = strsplit[3].Trim();
                string numeroCNS = strsplit[5].Replace("-","").Trim();
                string uf = strsplit[7].Trim();
                string nomeConj = strsplit[10].Trim();
                string novoNomeConj = strsplit[12].Trim();
                string nomeConj2 = strsplit[14].Trim();
                string novoNomeConj2 = strsplit[16].Trim();
                string dataCasamento = strsplit[18].Trim();
                string matricula = strsplit[20].Trim();
                string dataEntrada = strsplit[22].Trim();
                string dataRegistro = strsplit[24].Trim();

                ArpenspModel objArp = new ArpenspModel();
                objArp.CartorioRegistro = cartorioRegistro;
                objArp.NumCNS = Double.Parse(numeroCNS);
                objArp.UF = uf;
                objArp.NomeConj = nomeConj;
                objArp.NovoNomeConj = novoNomeConj;
                objArp.NomeConj2 = nomeConj2;
                objArp.NovoNomeConj2 = novoNomeConj2;
                objArp.DataCasamento = dataCasamento;
                objArp.Matricula = Double.Parse(matricula);
                objArp.DataEntrada = dataEntrada;
                objArp.DataRegistro = dataRegistro;

                string objjsonData = JsonConvert.SerializeObject(objArp, new JsonSerializerSettings { Formatting = Formatting.Indented });

                //string arr = JsonConvert.DeserializeObject<string>(objjsonData);

                string bd = objjsonData as string;

                System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Arpensp.txt", bd);

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

                string ie = strsplit[1].Replace("Situação", "").Trim();
                string situacao = strsplit[2].Trim();
                string cnpj = strsplit[4].Replace("Data da Inscrição no Estado", "").Trim();
                string dataInscricao = strsplit[5].Trim();
                string nomeEmpresarial = strsplit[7].Replace("Regime Estadual", "").Trim();
                string regimeEstadual = strsplit[8].Trim();
                string drt = strsplit[10].Replace("Posto Fiscal", "").Trim();
                string postoFiscal = strsplit[11].Trim();
                string nire = strsplit[21].Trim();
                string ocorrenciaFiscal = strsplit[26].Trim();
                string tipoUnidade = strsplit[28].Replace("Formas de Atuação", "").Trim();
                string formaAtuacao = strsplit[30].Trim();

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

                string data = strsplit[1].Replace("17", "").Trim();
                string nome = strsplit[4].Trim();
                string nMatriz = strsplit[7].Trim();
                string tipoEmpresa = strsplit[12].Trim();
                string dataConst = strsplit[14].Trim();
                string inicioAtiv = strsplit[16].Trim();
                string cnpj = strsplit[18].Trim();
                string capital = strsplit[26].Trim();
                string logradouro = strsplit[28].Trim();
                string numero = strsplit[30].Trim();
                string complemento = strsplit[34].Trim();
                string bairro = strsplit[32].Trim();
                string municipio = strsplit[36].Trim();
                string cep = strsplit[38].Trim();
                string uf = strsplit[40].Trim();

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

        
        public string Caged(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            //using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/caged/login.html");
                driver.FindElement(By.Id("btn-submit")).Click();
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/caged/pagina3-consulta-autorizado-responsavel.html");
                driver.FindElement(By.Id("formPesquisarAutorizado:txtChavePesquisaAutorizado014")).SendKeys(pesquisaCPFCNPJ.CPFCNPJ);
                driver.FindElement(By.Id("formPesquisarAutorizado:bt027_8")).Click();

                var resultado = driver.FindElement(By.XPath("//*[@id='conteudo']/fieldset[3]")).Text;

                string[] strsplit = resultado.Replace("\r\n", ":").Split(':');

                string nome = strsplit[1].Trim();
                string telefone = strsplit[9].Trim();
                string ramal = strsplit[12].Trim();
                string email = strsplit[15].Trim();

                CagedModel objCa = new CagedModel();
                objCa.Nome = nome;
                objCa.Telefone = telefone;
                objCa.Ramal = ramal;
                objCa.Email = email;

                string objjsonData = JsonConvert.SerializeObject(objCa);

                System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Caged.txt", objjsonData);

                return objjsonData;
            }
        }

    }
}
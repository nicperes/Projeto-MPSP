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
    public class WebScrapingArpensp
    {

        [HttpPost]
        public string Arpensp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/arpensp/login.html");

                driver.FindElement(By.XPath("//*[@id='main']/div[2]/div[2]/div[2]/div/a/img")).Click();
                //System.Threading.Thread.Sleep(500);
                driver.FindElement(By.ClassName("item3")).Click();
                //System.Threading.Thread.Sleep(500);
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

                //System.IO.File.WriteAllText(@"C:\Users\Nicolas PC\Desktop\teste\Arpensp.txt", objjsonData);
                System.IO.File.WriteAllText(@"C:\Users\nperes\Desktop\Projeto\Arquivos\Arpensp.txt", objjsonData);
                return objjsonData;

            }
        }

    }
}
using OpenQA.Selenium.Chrome;
using System;
using System.Web.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using WebApi.Models;
using java.io;
using org.apache.pdfbox.pdfparser;
using org.apache.pdfbox.util;
using java.net;
using Newtonsoft.Json;
using org.apache.pdfbox.cos;
using org.apache.pdfbox.pdmodel;

namespace WebApi.Scraping
{
    public class WebScraping
    {
        
        [HttpPost]
        public string Arpensp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            //using (IWebDriver driver = new ChromeDriver(options))
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

                //string arr = JsonConvert.DeserializeObject<string>(objjsonData);

                string bd = objjsonData as string;

                //System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Arpensp.txt", bd);

                return objjsonData;

            }
        }

        //Cadesp
        public string Cadesp(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            //using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/cadesp/login.html");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_UserName")).SendKeys("fiap");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_Password")).SendKeys("mpsp");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_loginControl_Password")).SendKeys(Keys.Enter);

                //driver.FindElement(By.XPath("//*[@id='ctl00_menuPlaceHolder_menuControl1_LoginView1_menuSuperiorn1']/table/tbody/tr/td/a")).Click();

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/cadesp/pagina3-pesquisa.html");
                driver.FindElement(By.Id("ctl00_conteudoPaginaPlaceHolder_tcConsultaCompleta_TabPanel1_txtIdentificacao")).SendKeys(pesquisaCPFCNPJ.CPFCNPJ);

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
                objCad.CNPJ = long.Parse(cnpj.Replace(".","").Replace("/","").Replace("-",""));
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
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            //using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/jucesp/index.html");
                driver.FindElement(By.Id("ctl00_cphContent_frmBuscaSimples_txtPalavraChave")).SendKeys(pesquisaCPFCNPJ.Nome);
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
                objJu.CNPJJucesp = long.Parse(cnpj.Replace(".", "").Replace("/", "").Replace("-", ""));
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
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            //using (IWebDriver driver = new ChromeDriver(options))
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

                //System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Caged.txt", objjsonData);

                return objjsonData;
            }
        }


        public string Detran(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {
            var options = new ChromeOptions();
            //options.AddArguments("headless");
            options.AddArguments("no-sandbox");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            //using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/detran/login.html");
                driver.FindElement(By.Id("form:j_id563205015_44efc15b")).Click();
                driver.FindElement(By.Id("navigation_a_M_16")).Click();
                driver.FindElement(By.XPath("//*[@id='navigation_a_F_16']")).Click();
                driver.FindElement(By.Id("form:rg")).SendKeys(pesquisaCPFCNPJ.CPFCNPJ);
                driver.FindElement(By.Id("form:nome")).SendKeys(pesquisaCPFCNPJ.Nome);
                driver.FindElement(By.LinkText("Pesquisar")).Click();

                driver.SwitchTo().Window(driver.WindowHandles[1]);
                URL url = new URL(driver.Url);
                BufferedInputStream fileToParse = new BufferedInputStream(url.openStream());
                PDFParser parser = new PDFParser(fileToParse);
                parser.parse();
                COSDocument cosDoc = parser.getDocument();
                PDDocument pdDoc = new PDDocument(cosDoc);
                PDFTextStripper pdfStripper = new PDFTextStripper();
                pdfStripper.setStartPage(1);
                pdfStripper.setEndPage(1);
                string parsedText = pdfStripper.getText(pdDoc);

                string saida = new PDFTextStripper().getText(parser.getPDDocument());

                driver.SwitchTo().Window(driver.WindowHandles[0]);
                driver.FindElement(By.Id("navigation_a_M_16")).Click();
                driver.FindElement(By.PartialLinkText("Consultar Imagem da CNH")).Click();
                driver.FindElement(By.LinkText("Pesquisar")).Click();
                driver.SwitchTo().Window(driver.WindowHandles[2]);
                //string nomePai = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]")).Text;
                string nPai = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[3]/td/table/tbody/tr[2]/td/span")).Text;
                string nMae = driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[3]/div/table/tbody/tr/td/table/tbody/tr[2]/td/table/tbody/tr/td[2]/table/tbody/tr[4]/td/table/tbody/tr[2]/td/span")).Text;


                driver.SwitchTo().Window(driver.WindowHandles[0]);
                driver.FindElement(By.Id("navigation_a_M_18")).Click();
                driver.FindElement(By.PartialLinkText("Consultar Veículo Base Estadual")).Click();
                driver.FindElement(By.XPath("/html/body/div[4]/div/table/tbody/tr/td/div/div/form/div[1]/div[2]/table[2]/tbody/tr[2]/td[2]/input")).SendKeys(pesquisaCPFCNPJ.CPFCNPJ);
                driver.FindElement(By.LinkText("Pesquisar")).Click();
                driver.SwitchTo().Window(driver.WindowHandles[3]);

                URL urlCarro = new URL(driver.Url);
                BufferedInputStream fileToParseCarro = new BufferedInputStream(urlCarro.openStream());
                PDFParser parserCarro = new PDFParser(fileToParseCarro);
                parserCarro.parse();
                COSDocument cosDocCarro = parserCarro.getDocument();
                PDDocument pdDocCarro = new PDDocument(cosDocCarro);
                PDFTextStripper pdfStripperCarro = new PDFTextStripper();
                pdfStripper.setStartPage(1);
                pdfStripper.setEndPage(1);
                string parsedTextCarro = pdfStripperCarro.getText(pdDocCarro);

                string saidaCarro = new PDFTextStripper().getText(parserCarro.getPDDocument());

                string resultado = saida + nPai + nMae + saidaCarro;

                string[] strsplit = resultado.Replace("\r\n", ":").Split(':');

                string cpf = strsplit[33].Trim();
                string rg = strsplit[13].Trim();
                string expeditor = strsplit[34].Trim();
                string registro = strsplit[36].Trim();
                string local = strsplit[38].Trim();
                string espelhoPid = strsplit[40].Trim();
                string emissaoCnh = strsplit[42].Trim();
                string categoria = strsplit[46].Trim();
                string primeiraHab = strsplit[48].Trim();
                string statusCnh = strsplit[50].Trim();
                string renach = strsplit[52].Trim();
                string espelhoCnh = strsplit[54].Trim();
                string validadeCnh = strsplit[56].Trim();
                string pontuacao = strsplit[58].Trim();
                string nomePai = strsplit[119].Trim();
                string nomeMae = strsplit[120].Trim();
                string placa = strsplit[144].Replace(" 7107 - SAO PAULO", "").Trim();
                string municipioPlaca = strsplit[144].Replace("gge4223  ", "").Trim();
                string renavam = strsplit[146].Replace("  9AAAAVAU0J4001600 ", "").Trim();
                string chassi = strsplit[146].Replace("01172566666  ", "").Trim();
                string numMotor = strsplit[148].Replace("  22/11/18 00", "").Trim();
                string dataAltMotor = strsplit[148].Replace("CWL031481  ", "").Trim();
                string tipo = strsplit[151].Replace(" 1 - IMPORTADO 16 - ALCO/GASOL", "").Trim();
                string procedencia = strsplit[151].Replace("6 - AUTOMOVEL ", "").Replace(" 16 - ALCO/GASOL ", "").Trim();
                string combustivel = strsplit[151].Replace("6 - AUTOMOVEL 1 - IMPORTADO ", "").Trim();
                string cor = strsplit[153].Replace("  162801 – VARIANT GL ", "").Trim();
                string marcaModelo = strsplit[153].Replace("4 - BRANCA  162801 – ", "").Trim();
                string categoriaAut = strsplit[155].Replace(" 1971 1972 ", "").Trim();
                string anoFab = strsplit[155].Replace("1 - PARTICULAR ", "").Replace(" 1972 ", "").Trim();
                string anoMod = strsplit[155].Replace("1 - PARTICULAR 1971 ", "").Trim();
                string logradouro = strsplit[166].Replace("  00121 ", "").Trim();
                string numero = strsplit[166].Replace("AV LINS DE VASCONCELOS  ", "").Trim();
                string complemento = strsplit[182].Replace("  010006-010 ", "").Trim();
                string cep = strsplit[182].Replace("4 ANDAR  ", "").Trim();
                string bairro = strsplit[184].Replace(" 7107 - SAO PAULO SP ", "").Trim();
                string licenciamento = strsplit[225].Replace("   07/03/2019 ", "").Trim();
                string dataLicenciamento = strsplit[225].Replace("2019   ", "").Trim();
                string dataEmissaoCRV = strsplit[227].Trim();

                DetranModel objDen = new DetranModel();
                objDen.CNPJCPF = long.Parse(cpf.Replace(".","").Replace("-", ""));
                objDen.RG = rg;
                objDen.Expeditor = expeditor;
                objDen.Registro = registro;
                objDen.Local = local;
                objDen.PID = espelhoPid;
                objDen.EmissaoCnh = emissaoCnh;
                objDen.Categoria = categoria;
                objDen.PrimeiraHabilitação = primeiraHab;
                objDen.StatusCnh = statusCnh;
                objDen.Renach = renach;
                objDen.EspelhoCnh = espelhoCnh;
                objDen.ValidadeCnh = validadeCnh;
                objDen.Pontuacao = pontuacao;
                objDen.NomePai = nPai;
                objDen.NomeMae = nMae;
                objDen.Placa = placa;
                objDen.MunicipioCarro = municipioPlaca;
                objDen.Renavam = renavam;
                objDen.Chassi = chassi;
                objDen.NumMotor = numMotor;
                objDen.DataAltMotor = dataAltMotor;
                objDen.Tipo = tipo;
                objDen.Procedencia = procedencia;
                objDen.Combustivel = combustivel;
                objDen.Cor = cor;
                objDen.MarcaModelo = marcaModelo;
                objDen.CategoriaAut = categoriaAut;
                objDen.Fabricacao = anoFab;
                objDen.Modelo = anoMod;
                objDen.Logradouro = logradouro;
                objDen.Numero = numero;
                objDen.Complemento = complemento;
                objDen.CEP = cep;
                objDen.Bairro = bairro;
                objDen.Licenciamento = licenciamento;
                objDen.DataLicenciamento = dataLicenciamento;
                objDen.DataEmissaoCRV = dataEmissaoCRV;

                string objjsonData = JsonConvert.SerializeObject(objDen, new JsonSerializerSettings { Formatting = Formatting.Indented });

                //System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Detran.txt", objjsonData);

                return objjsonData;
            }
        }

        public string Censec(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            //using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/censec/login.html");

                driver.FindElement(By.Id("EntrarButton")).Click();
                driver.FindElement(By.Id("menucentrais")).Click();
                driver.FindElement(By.XPath("//*[@id='ctl00_CESDILi']/a")).Click();
                driver.FindElement(By.XPath("//*[@id='ctl00_CESDIConsultaAtoHyperLink']")).Click();
                driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DocumentoTextBox")).SendKeys(pesquisaCPFCNPJ.CPFCNPJ);
                driver.FindElement(By.ClassName("BT_Buscar")).SendKeys(Keys.Enter);

                driver.FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_ResultadoBuscaGeralPanel']/div[2]/div[1]/div/table/tbody/tr[2]/td[1]/input")).Click();
                driver.FindElement(By.ClassName("BT_Buscar")).SendKeys(Keys.Enter);

                string carga = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[3]/div[1]/div/input")).GetAttribute("value");
                string mes = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_MesReferenciaDropDownList")).Text;
                string ano = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_AnoReferenciaDropDownList")).Text;
                string ato = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_TipoAtoDropDownList")).GetAttribute("value");
                string diaAto = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[3]/div[6]/div/input[1]")).GetAttribute("value");
                string mesAto = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[3]/div[6]/div/input[3]")).GetAttribute("value");
                string anoAto = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[3]/div[6]/div/input[4]")).GetAttribute("value");
                string livro = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[3]/div[7]/div/input[1]")).GetAttribute("value");
                string folha = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[3]/div[9]/div/input[1]")).GetAttribute("value");

                string tabelaPartes = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[6]/div[1]/div/div/table/tbody")).Text;

                string uf = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DadosCartorio_CartorioUFTextBox")).GetAttribute("value");
                string municipio = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DadosCartorio_CartorioMunicipioTextBox")).GetAttribute("value");
                string cartorio = driver.FindElement(By.Id("ctl00_ContentPlaceHolder1_DadosCartorio_CartorioNomeTextBox")).GetAttribute("value");
                string tabela = driver.FindElement(By.XPath("//*[@id='ctl00_ContentPlaceHolder1_DadosCartorio_DivTelefonesCartorioListView']/div/table")).Text;

                string[] strsplit = tabelaPartes.Replace("\r\n", ":").Replace("  ", ":").Split(':');
                var nomes = "";
                var cpfCnpj = "";
                var quali = "";
                var table = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[6]/div[1]/div/div/table/tbody"));
                var rows = table.FindElements(By.TagName("tr"));
                var count = rows.Count;

                //For para pegar os nomes
                for (int i = 0; i < count * 3; i += 3)
                {
                    nomes = nomes + " |" + strsplit[i];
                }

                //For para pegar os CPFs/CNPJs
                for (int i = 1; i < count * 3; i += 3)
                {
                    cpfCnpj = cpfCnpj + " | " + strsplit[i];
                }

                //For para pegar as qualidades
                for (int i = 2; i < count * 3; i += 3)
                {
                    quali = quali + " | " + strsplit[i];
                }

                string[] strsplit2 = tabela.Replace("\r\n", ":").Replace("  ", ":").Split(':');
                var telefones = "";
                var tipos = "";
                var contatos = "";
                var status = "";
                var table2 = driver.FindElement(By.XPath("/html/body/form/div[5]/div/div[3]/div[2]/div[7]/div[2]/div[2]/div/table/tbody"));
                var rows2 = table2.FindElements(By.TagName("tr"));
                var count2 = rows2.Count;

                //For para pegar os telefones
                for (int i = 0; i < count2 * 5; i += 5)
                {
                    telefones = telefones + " |" + strsplit2[i];
                }

                //For para pegar os tipos
                for (int i = 1; i < count2 * 5; i += 5)
                {
                    tipos = tipos + " |" + strsplit2[i];
                }

                //For para pegar os contatos
                for (int i = 3; i < count2 * 5; i += 5)
                {
                    contatos = contatos + " |" + strsplit2[i];
                }

                //For para pegar os status
                for (int i = 4; i < count2 * 5; i += 5)
                {
                    status = status + " |" + strsplit2[i];
                }

                CensecModel objCen = new CensecModel();
                objCen.Carga = carga.Trim();
                objCen.Mes = mes.Trim();
                objCen.Ano = ano.Trim();
                objCen.Ato = ato.Replace("\r\n", "").Trim();
                objCen.DiaAto = diaAto.Trim();
                objCen.MesAto = mesAto.Trim();
                objCen.AnoAto = anoAto.Trim();
                objCen.Livro = livro.Trim();
                objCen.Folha = folha.Trim();
                objCen.NomesPartes = nomes.Trim();
                objCen.CpfCnpjPartes = cpfCnpj.Trim();
                objCen.QualidadePartes = quali.Trim();
                objCen.UF = uf.Trim();
                objCen.Municipio = municipio.Trim();
                objCen.Cartorio = cartorio.Trim();
                objCen.TelefoneCartorio = telefones.Replace("_", "").Trim();
                objCen.TipoTelefoneCartorio = tipos.Trim();
                objCen.ContatoCartorio = contatos.Trim();
                objCen.StatusCartorio = status.Trim();

                string objjsonData = JsonConvert.SerializeObject(objCen);

                //System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Censec.txt", objjsonData);

                return objjsonData;
            }
        }

        public string Siel(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            //using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/siel/login.html");
                driver.FindElement(By.XPath("//html/body/div[1]/div[1]/div[4]/form/table/tbody/tr[3]/td[2]/input")).Click();
                driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form[2]/fieldset[1]/table/tbody/tr[1]/td[2]/input")).SendKeys(pesquisaCPFCNPJ.Nome);
                driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form[2]/fieldset[2]/table[1]/tbody/tr/td[2]/input")).Click();
                driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form[1]/div[2]/table/tbody/tr/td/p")).Click();
                driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form[2]/fieldset[2]/table[1]/tbody/tr/td[2]/input")).SendKeys(pesquisaCPFCNPJ.Processo);
                driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/form[2]/table/tbody/tr/td[2]/input")).Click();
                string tabela = driver.FindElement(By.XPath("/html/body/div[1]/div[1]/div[4]/table")).Text;

                string[] strsplit = tabela.Replace("\r\n", ":").Split(':');
                string titulo = strsplit[2].Replace("Título ", "").Trim();
                string zona = strsplit[4].Replace("Zona ", "").Trim();
                string dataDomicilio = strsplit[8].Replace("Data Domicílio ", "").Trim();

                SielModel objSiel = new SielModel();
                objSiel.Titulo = titulo;
                objSiel.Zona = zona;
                objSiel.DataDomicilio = dataDomicilio;

                string objjsonData = JsonConvert.SerializeObject(objSiel);

                //System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\SielSaida.txt", objjsonData);

                return objjsonData;
            }
        }

        public string Sivec(PesquisaCPFCNPJ pesquisaCPFCNPJ)
        {

            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot",options))
            //using (IWebDriver driver = new ChromeDriver(options))
            {
                Actions builder = new Actions(driver);

                //Validação
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/ ");
                driver.FindElement(By.Id("username")).SendKeys("fiap");
                driver.FindElement(By.Id("password")).SendKeys("mpsp");
                driver.FindElement(By.Id("password")).SendKeys(Keys.Enter);

                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/sivec/login.html");
                driver.FindElement(By.Id("nomeusuario")).SendKeys("fiap");
                driver.FindElement(By.Id("senhausuario")).SendKeys("mpsp");
                driver.FindElement(By.Id("Acessar")).Click();

                driver.FindElement(By.XPath("/html/body/nav/div[2]/ul/li[4]/a")).Click();
                driver.FindElement(By.XPath("/html/body/nav/div[2]/ul/li[4]/ul/li[2]/a")).Click();
                driver.FindElement(By.XPath("/html/body/nav/div[2]/ul/li[4]/ul/li[2]/ul/li[1]/a")).Click();

                driver.FindElement(By.Id("idValorPesq")).SendKeys(pesquisaCPFCNPJ.CPFCNPJ);
                driver.FindElement(By.Id("procurar")).Click();
                System.Threading.Thread.Sleep(2000);
                driver.FindElement(By.XPath("//*[@id='tabelaPesquisa']/tbody/tr[1]/td[1]/a")).Click();

                var tabela = driver.FindElement(By.XPath("/html/body/form[1]/div/div[5]/div[4]/table/tbody")).Text;

                string[] strsplit = tabela.Replace("\r\n", ":").Split(':');

                string dataEmissao = strsplit[1].Replace(" Alcunha", "").Trim();
                string estadoCivil = strsplit[4].Replace(" Naturalidade", "").Trim();
                string naturalizado = strsplit[7].Replace(" Posto de Identificação", "").Trim();
                string postoIdentificacao = strsplit[8].Trim();
                string grauInstituicao = strsplit[10].Replace(" Fórmula Fundamental", "").Trim();
                string corOlho = strsplit[14].Trim();
                string corCabelo = strsplit[17].Trim();
                string corPele = strsplit[19].Replace(" Profissão", "").Trim();
                string profissao = strsplit[20].Trim();

                SivecModel objSivec = new SivecModel();
                objSivec.DataEmissao = dataEmissao;
                objSivec.EstadoCivil = estadoCivil;
                objSivec.Naturalizado = naturalizado;
                objSivec.PostoIdentificacao = postoIdentificacao;
                objSivec.GrauInstituicao = grauInstituicao;
                objSivec.CorOlho = corOlho;
                objSivec.CorPele = corPele;
                objSivec.CorCabelo = corCabelo;
                objSivec.Profissao = profissao;

                string objjsonData = JsonConvert.SerializeObject(objSivec);

                //System.IO.File.WriteAllText(@"C:\Users\favar\Desktop\Texto\Sivec.txt", objjsonData);

                return objjsonData;
            }
        }

    }
}
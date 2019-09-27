using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Repositories
{
    public class ValidacaoRepository
    {
        public Boolean Validacao(LoginModel loginModel)
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/ ");
                driver.FindElement(By.Id("username")).SendKeys(loginModel.Login);
                driver.FindElement(By.Id("password")).SendKeys(loginModel.Senha);
                driver.FindElement(By.Id("password")).SendKeys(Keys.Enter);

                //var teste = driver.FindElement(By.CssSelector(".alert")).Displayed;

                /*if (teste == true)
                {
                    return false;
                }
                else
                {
                    return true;
                }*/

                return true;
            }
        }
    }
}
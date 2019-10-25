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
            
            var options = new ChromeOptions();
            options.AddArguments("headless");
            using (IWebDriver driver = new ChromeDriver("C:/inetpub/wwwroot/wwwroot", options))
            //using (IWebDriver driver = new ChromeDriver(options))
            {
                driver.Navigate().GoToUrl("http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/ ");
                driver.FindElement(By.Id("username")).SendKeys(loginModel.Login);
                driver.FindElement(By.Id("password")).SendKeys(loginModel.Senha);
                driver.FindElement(By.Id("password")).SendKeys(Keys.Enter);
                
                var teste = driver.Url;

                if (teste == "http://ec2-18-231-116-58.sa-east-1.compute.amazonaws.com/login?error")
                {
                    return false;
                }
                else
                {
                    return true;
                }
                
            }
        }
    }
}
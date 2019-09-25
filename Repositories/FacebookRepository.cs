using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Repositories
{
    public class FacebookRepository
    {

        public void WebScraping()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl("http://www.facebook.com.br");

                driver.FindElement(By.Id("email")).SendKeys("nicolasfperes@hotmail.com");
                driver.FindElement(By.Id("u_0_b")).Click();


            }
        }

    }
}
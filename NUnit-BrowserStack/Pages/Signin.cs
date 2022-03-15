using System;
using OpenQA.Selenium;

namespace BrowserStack
{
    public class Signin 
    {
        IWebDriver Driver;
        public Signin(IWebDriver ldriver)
        {
            this.Driver = ldriver;
        }

        IWebElement lnksignin => Driver.FindElement(By.Id("signin"));
        IWebElement ddUsername => Driver.FindElement(By.XPath("//div[contains(text(),'Username')]/.."));
        IWebElement ddPassword => Driver.FindElement(By.XPath("//div[contains(text(),'Password')]/.."));
        IWebElement btnlogin => Driver.FindElement(By.Id("login-btn"));

        public void Login(String user, String pass)
        {
            Driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);
            lnksignin.Click();
            ddUsername.Click();
            IJavaScriptExecutor js = (IJavaScriptExecutor)Driver;
            IWebElement txtuser = Driver.FindElement(By.XPath("//*[text()='"+ user +"']"));
            js.ExecuteScript("arguments[0].click();", txtuser);
            ddPassword.Click();
            IWebElement txtpass = Driver.FindElement(By.XPath("//*[text()='" + pass + "']"));
            js.ExecuteScript("arguments[0].click();", txtpass);
            btnlogin.Click();
        
        }

    }
}

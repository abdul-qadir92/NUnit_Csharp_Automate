using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BrowserStack.Pages
{
    public class Home
    {
        IWebDriver Driver;
        public Home(IWebDriver ldriver)
        {
            this.Driver = ldriver;
        }
        IWebElement lnkOrders => Driver.FindElement(By.Id("orders"));
        IWebElement image => Driver.FindElement(By.XPath("(//div[@class='shelf-item__thumb']/img)[1]"));

        public void NavigateOrders()
        {
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            lnkOrders.Click();
        }

        public void verifySignin()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(Driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(250);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element to be searched not found";

            IWebElement searchResult = fluentWait.Until(x => x.FindElement(By.CssSelector("span.username")));
            Assert.That(searchResult.Displayed, "Sign in successful");
        }

        public void verifyimageLoaded()
        {
            Object result = ((IJavaScriptExecutor)Driver).ExecuteScript(
                    "return arguments[0].complete && " +
                            "typeof arguments[0].naturalWidth != \"undefined\" && " +
                            "arguments[0].naturalWidth > 0", image);

            Assert.That((Boolean)result, "Product images loaded for the user");
        }

    }
}

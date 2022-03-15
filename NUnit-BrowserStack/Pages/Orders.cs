using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;

namespace BrowserStack.Pages
{
    public class Orders
    {
        IWebDriver Driver;
        public Orders(IWebDriver ldriver)
        {
            this.Driver = ldriver;
        }
        IWebElement paneOrders => Driver.FindElement(By.XPath("//div[@id='__next']//div[contains(@class,'top-medium')]"));
        
        public void VerifyOrders()
        {
            Driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(10);
            IList<IWebElement> orderTitle = Driver.FindElements(By.XPath("//strong[text()='Title:']/.."));
            IList<IWebElement> orderPrice = Driver.FindElements(By.XPath("//span[contains(@class,'price')]"));
            var Allorders = new ArrayList();
            for (int i = 0; i < orderPrice.Count; i++)
            {
                Allorders.Add(orderTitle[i].Text.Substring(7) + ' ' + orderPrice[i].Text);
            }
            Assert.That(Allorders.Count > 0, "Orders Available for Exisitng user");
            foreach (var order in Allorders)
            {
                Console.WriteLine(order);
            }
        }
    }
}

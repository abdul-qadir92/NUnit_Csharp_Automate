using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager.DriverConfigs.Impl;

namespace NUnit_Csharp_Automate.Utilities
{
    public class BrowserUtility
    {
        public IWebDriver Init(IWebDriver driver)
        {
            new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
            driver = new ChromeDriver();
            return driver;
        }

    }
}

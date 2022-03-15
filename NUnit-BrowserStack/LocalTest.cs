using BrowserStack.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;

namespace BrowserStack
{
  [TestFixture("local", "chrome")]
  public class LocalTest : BrowserStackNUnitTest
  {
    public LocalTest(string profile, string environment) : base(profile, environment) { }

    [SetUp]
    public void Launch()    
    {
        driver.Manage().Window.Maximize();
        driver.Navigate().GoToUrl("http://localhost:3000/");
    }

        [Test]
        [Parallelizable]
        public void Exisitng_Orders()
        {
            Signin page = new Signin(driver);
            page.Login("existing_orders_user", "testingisfun99");
            Home home = new Home(driver);
            home.verifySignin();
            home.NavigateOrders();
            Orders orders = new Orders(driver);
            orders.VerifyOrders();
        }

        [Test]
        [Parallelizable]
        public void Image_Not_Loading()
        {
            Signin page = new Signin(driver);
            page.Login("image_not_loading_user", "testingisfun99");
            Home home = new Home(driver);
            home.verifySignin();
            home.verifyimageLoaded();
        }
    }
}

using System.Collections.Specialized;
using System.Configuration;
using BrowserStack.Pages;
using NUnit.Framework;

namespace BrowserStack
{
    [TestFixture("single", "edge")]
    public class Test : BrowserStackNUnitTest
    {
        public Test(string profile, string environment) : base(profile, environment) { }

        /* [TestFixture]       // To Run on premise
        [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
        public class Test
        {
            protected IWebDriver driver;
            protected readonly BrowserUtility _utility = new BrowserUtility();
            [SetUp]
            public void Setup()
            {
                driver = _utility.Init(driver);
                driver.Manage().Window.Maximize();
                driver.Url = "https://bstackdemo.com/";
            } */

        [SetUp]
        public void Launch()
        {
            NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;
            if (settings.Get("real_mobile")==null)
                driver.Manage().Window.Maximize();
            driver.Url = "https://bstackdemo.com/";
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
  
        /*[TearDown]        // To Run on premise
        public void TearDown()
        {
            driver.Quit();
        }
        */
    }
}
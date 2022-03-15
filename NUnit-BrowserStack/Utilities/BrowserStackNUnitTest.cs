using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;


namespace BrowserStack
{
  [TestFixture]
  [FixtureLifeCycle(LifeCycle.InstancePerTestCase)]
  public class BrowserStackNUnitTest
  {

    protected IWebDriver driver;
    protected string profile;
    protected string environment;
    private static Local browserStackLocal;
    static String build;
    static String localprofile;
    

    public BrowserStackNUnitTest(string profile, string environment)
    {
      this.profile = profile;
      this.environment = environment;
            localprofile = profile;
    }

    [OneTimeSetUp]
    public static void SetBuild()
    {
      build = DateTime.Now.ToString("MM.dd.yy.HH.mm");
            if (localprofile.Contains("local"))
            {
                browserStackLocal = new Local();
                List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("key", ConfigurationManager.AppSettings.Get("key")),
                    new KeyValuePair<string, string>("binarypath", "/Users/qadir/Documents/BrowserStackLocal")
                };
                browserStackLocal.start(bsLocalArgs);
            }
      
    }

    [SetUp]
    public void Init()
    {
      NameValueCollection caps = ConfigurationManager.GetSection("capabilities/" + profile) as NameValueCollection;
      NameValueCollection settings = ConfigurationManager.GetSection("environments/" + environment) as NameValueCollection;

            //Set Build names in the caps
            var writableCollection = new NameValueCollection(caps);
            writableCollection.Set("build", caps.Get("project") + "-build-" + build);
            writableCollection.Set("name", TestContext.CurrentContext.Test.Name);

            DesiredCapabilities capability = new DesiredCapabilities();

            foreach (string key in writableCollection.AllKeys)
      {
        capability.SetCapability(key, writableCollection[key]);
      }

      foreach (string key in settings.AllKeys)
      {
        capability.SetCapability(key, settings[key]);
      }

      String username = Environment.GetEnvironmentVariable("BROWSERSTACK_USERNAME");
      if(username == null)
      {
        username = ConfigurationManager.AppSettings.Get("user");
      }

      String accesskey = Environment.GetEnvironmentVariable("BROWSERSTACK_ACCESS_KEY");
      if (accesskey == null)
      {
        accesskey = ConfigurationManager.AppSettings.Get("key");
      }

      capability.SetCapability("browserstack.user", username);
      capability.SetCapability("browserstack.key", accesskey);

      if (capability.GetCapability("browserstack.local") != null && capability.GetCapability("browserstack.local").ToString() == "true")
      {
        /*browserStackLocal = new Local();
        List<KeyValuePair<string, string>> bsLocalArgs = new List<KeyValuePair<string, string>>() {
          new KeyValuePair<string, string>("key", accesskey)
        };
        browserStackLocal.start(bsLocalArgs);*/
      }

      driver = new RemoteWebDriver(new Uri("http://"+ ConfigurationManager.AppSettings.Get("server") +"/wd/hub/"), capability);
    }

    [TearDown]
    public void Cleanup()
    {
            if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Passed)
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"passed\", \"reason\": \"\"}}");
            }
            else
            {
                String Message = TestContext.CurrentContext.Result.Message.ToString().Trim();
                String reason = (Message != null && Message.Length > 254) ? Message.Substring(0, 254) : Message;
                ((IJavaScriptExecutor)driver).ExecuteScript("browserstack_executor: {\"action\": \"setSessionStatus\", \"arguments\": {\"status\":\"failed\", \"reason\":\"" + reason.Replace("[^a-zA-Z0-9._-]", " ").Replace("\n","").Replace("\r","") + "\"}}");
            }
            driver.Quit();
    }

    [OneTimeTearDown]
    public static void closeLocal()
    {
            if (browserStackLocal != null)
            {
                browserStackLocal.stop();
            }
    }
  }
}

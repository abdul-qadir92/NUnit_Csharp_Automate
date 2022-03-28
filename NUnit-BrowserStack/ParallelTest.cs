using NUnit.Framework;

namespace BrowserStack
{
  [TestFixture("parallel", "iphone12")]
  [TestFixture("parallel", "samsungS21")]
  [TestFixture("parallel", "safari")]
  [TestFixture("parallel", "edge")]
  [Parallelizable(ParallelScope.Fixtures)]
  public class ParallelTest : Test
  {
    public ParallelTest(string profile, string environment) : base(profile, environment) { }
  }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V1X.Types
{
    [TestClass]
    public class StringTests
    {
        [TestMethod]
        public void VerbatimStringLiteralsTest()
        {
            Assert.AreEqual(
                "c:\\user\\ohyecloudy.txt",
                @"c:\user\ohyecloudy.txt",
                "escape sequences를 처리하지 않아야 한다.");
        }
    }
}

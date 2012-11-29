// 참고:
// extern (C# Reference) - msdn
// http://msdn.microsoft.com/en-US/library/e59b22c5

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Runtime.InteropServices; 

namespace CSharpFeaturesTest.V20.ExternalAssemblyAlias
{
    [TestClass]
    public class ExternalAssemblyAliasTest
    {
        [DllImport("Shlwapi.dll")]
        static extern bool PathFileExistsA(string path);
                                
        [TestMethod]
        public void BasicTest()
        {
            Assert.IsTrue(PathFileExistsA("c:\\"));
            Assert.IsTrue(PathFileExistsA("c:\\windows"));
            Assert.IsFalse(PathFileExistsA("z:\\WWEEQAAW123541\\2asdwqeq1\\aaawwertqas.adsa"));
        }
    }
}

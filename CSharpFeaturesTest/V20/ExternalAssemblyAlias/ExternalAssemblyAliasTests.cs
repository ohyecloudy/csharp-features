// 참고:
// extern (C# Reference) - msdn
// http://msdn.microsoft.com/en-US/library/e59b22c5

using Xunit;
using System.Runtime.InteropServices; 

namespace CSharpFeaturesTest.V20.ExternalAssemblyAlias
{
    
    public class ExternalAssemblyAliasTests
    {
        [DllImport("Shlwapi.dll")]
        static extern bool PathFileExistsA(string path);
                                
        [Fact]
        public void ExternalAssemblyAliasTest()
        {
            Assert.True(PathFileExistsA("c:\\"));
            Assert.True(PathFileExistsA("c:\\windows"));
            Assert.False(PathFileExistsA("z:\\WWEEQAAW123541\\2asdwqeq1\\aaawwertqas.adsa"));
        }
    }
}

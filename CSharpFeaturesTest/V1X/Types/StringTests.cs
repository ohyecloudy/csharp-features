using Xunit;

namespace CSharpFeaturesTest.V1X.Types
{
    
    public class StringTests
    {
        [Fact]
        public void VerbatimStringLiteralsTest()
        {
            // "escape sequences를 처리하지 않아야 한다."
            Assert.Equal(
                "c:\\user\\ohyecloudy.txt",
                @"c:\user\ohyecloudy.txt");
        }
    }
}

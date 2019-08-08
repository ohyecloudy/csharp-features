// 참고:
// Friend Assemblies (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/0tke9fxk

using Xunit;

namespace CSharpFeaturesTest.V20.FriendAssemblies
{
    
    public class FriendAssembliesTests
    {
        [Fact]
        public void FriendAssembliesTest()
        {
            v20FriendAssembly.InternalClass ic = new v20FriendAssembly.InternalClass();
            Assert.Equal(5, ic.PublicProperty);

            Assert.Equal(15, v20FriendAssembly.PublicClass.InternalStaticField);
        }
    }
}

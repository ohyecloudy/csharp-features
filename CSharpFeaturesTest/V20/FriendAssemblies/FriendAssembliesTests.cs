// 참고:
// Friend Assemblies (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/0tke9fxk

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V20.FriendAssemblies
{
    [TestClass]
    public class FriendAssembliesTests
    {
        [TestMethod]
        public void FriendAssembliesTest()
        {
            v20FriendAssembly.InternalClass ic = new v20FriendAssembly.InternalClass();
            Assert.AreEqual(5, ic.PublicProperty);

            Assert.AreEqual(15, v20FriendAssembly.PublicClass.InternalStaticField);
        }
    }
}

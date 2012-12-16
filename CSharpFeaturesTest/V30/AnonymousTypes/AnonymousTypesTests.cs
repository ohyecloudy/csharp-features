// 참고:
// Anonymous Types (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb397696

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V30.AnonymousTypes
{
    [TestClass]
    public class AnonymousTypesTest
    {
        [TestMethod]
        public void BasicTest()
        {
            var v = new { Amount = 108, Message = "Hello" };
            
            // read only!
            // v.Amount = 110;

            Assert.AreEqual("<>f__AnonymousType0`2", v.GetType().Name, "2는 패러매터 숫자를 가리킨다");
            Assert.AreEqual("System.Object", v.GetType().BaseType.FullName, "object 상속");
        }
    }
}

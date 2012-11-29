// 참고:
// Object and Collection Initializers (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb384062

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V30.ObjectInitializers
{
    [TestClass]
    public class ObjectInitializersTest
    {
        class TargetClass
        {
            public int intMember;
            public string strMember;
            public int Property { get; set; }
        }

        [TestMethod]
        public void BasicTest()
        {
            // object initializer 사용
            TargetClass t = new TargetClass
            {
                intMember = 5,
                strMember = "ohyecloudy",
                Property = 1,
            };

            Assert.AreEqual(5, t.intMember);
            Assert.AreEqual("ohyecloudy", t.strMember);
            Assert.AreEqual(1, t.Property);
        }

        [TestMethod]
        public void AnonymousTypeTest()
        {
            // anonymous type을 생성할 때 사용
            var pet = new
            {
                Age = 10,
                Name = "Fluffy",
            };

            Assert.AreEqual(10, pet.Age);
            Assert.AreEqual("Fluffy", pet.Name);
        }
    }
}

// 참고:
// Object and Collection Initializers (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb384062

using Xunit;

namespace CSharpFeaturesTest.V30.ObjectInitializers
{
    
    public class ObjectInitializersTests
    {
        class TargetClass
        {
            public int intMember;
            public string strMember;
            public int Property { get; set; }
        }

        [Fact]
        public void ObjectInitializersTest()
        {
            // object initializer 사용
            TargetClass t = new TargetClass
            {
                intMember = 5,
                strMember = "ohyecloudy",
                Property = 1,
            };

            Assert.Equal(5, t.intMember);
            Assert.Equal("ohyecloudy", t.strMember);
            Assert.Equal(1, t.Property);
        }

        [Fact]
        public void AnonymousTypeTest()
        {
            // anonymous type을 생성할 때 사용
            var pet = new
            {
                Age = 10,
                Name = "Fluffy",
            };

            Assert.Equal(10, pet.Age);
            Assert.Equal("Fluffy", pet.Name);
        }
    }
}

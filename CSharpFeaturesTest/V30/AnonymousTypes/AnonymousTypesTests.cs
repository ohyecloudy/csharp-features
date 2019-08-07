// 참고:
// Anonymous Types (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb397696

using Xunit;

namespace CSharpFeaturesTest.V30.AnonymousTypes
{
    
    public class AnonymousTypesTests
    {
        [Fact]
        public void AnonymousTypesTest()
        {
            var v = new { Amount = 108, Message = "Hello" };
            
            // read only!
            // v.Amount = 110;

            Assert.True("<>f__AnonymousType0`2" == v.GetType().Name, "2는 패러매터 숫자를 가리킨다");
            Assert.True("System.Object" == v.GetType().BaseType.FullName, "object 상속");
        }
    }
}

// 참고:
// Named and Optional Arguments (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/dd264739

using Xunit;
using System;

namespace CSharpFeaturesTest.V40.NamedAndOptionalArguments
{
    
    public class NamedAndOptionalArgumentsTest
    {        
        int Power(int baseNum, int exponent)
        {
            return (int)Math.Pow(baseNum, exponent);
        }

        [Fact]
        public void NamedArgumentsTest()
        {
            // "parameter: argument식으로 named arguments 사용"
            Assert.Equal(Power(2, 8), Power(exponent: 8, baseNum: 2));
        }

        void ExampleMethod(
            int required,
            string optionalStr = "default string",
            int optionalInt = new Int32())
        {
        }

        [Fact]
        public void OptionalArgumentsTest()
        {
            ExampleMethod(5);
            
            // 불가능. 익숙하지? 
            // ExampleMethod(5, ,2); 

            // named arguments를 사용해 건너뛰는 게 가능.
            ExampleMethod(5, optionalInt: 2);
        }
    }
}

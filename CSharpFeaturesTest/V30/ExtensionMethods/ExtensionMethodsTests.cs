// 참고:
// Extension Methods (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb383977

using Xunit;
using System;

namespace CustomExtensions
{
    // top level에서 정의해야 함
    // ExtensionMethodsTests 안에서 정의하면 컴파일 에러
    public static class MyExtensions
    {
        // this modifier를 사용
        // 정의는 반드시 static method. 사용은 instance method 처럼.
        public static int WordCount(this String str)
        {
            return str.Split(
                new char[] { ' ', '.', '?' },
                StringSplitOptions.RemoveEmptyEntries).Length;
        }

        // 패러매터를 추가로 받는 함수이면 this modifier가 붙은 패러매터 뒤에 정의
        public static string ConcatWith(this String str, String rhs)
        {
            return String.Concat(str, rhs);
        }
    }
}

namespace CSharpFeaturesTest.V30.ExtensionMethods
{
    // 별도 namespace로 묶었다면 using을 사용해 mapping
    using CustomExtensions;

    
    public class ExtensionMethodsTests
    {  
        [Fact]
        public void ExtensionMethodsTest()
        {
            string str = "hello world world world";

            Assert.True(4 == str.WordCount(), "string에는 WordCount() method가 없지만 위에서 extension method로 추가");
            Assert.Equal("hello world world world!", str.ConcatWith("!"));
        }
    }
}

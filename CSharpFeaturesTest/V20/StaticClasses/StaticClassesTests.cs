﻿// 참고:
// Static Classes and Static Class Members (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/79b3xss3

using Xunit;

namespace CSharpFeaturesTest.V20.StaticClasses
{
    
    public class StaticClassesTests
    {
        static class Util
        {
            // instance constructor를 사용할 수 없다.
            // public Util() {}
            public static int Number = 5;
            public static int NumProperty { get { return 10; } }

            // instance method는 정의 못함. static method만 정의할 수 있다.
            // public int method() { return 0; }
        }

        // 상속할 수 없다. static class는 sealed 된다.
        // static class DerivedUtil : Util { }
        
        [Fact]
        public void StaticClassesTest()
        {
            // 인스턴스를 만들 수 없다.
            // Util util = new Util();

            Assert.Equal(5, Util.Number);
            Assert.Equal(10, Util.NumProperty);
        }
    }
}

// 참고:
// Nullable Types (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/1t3y8s4s

using Xunit;
using System;

namespace CSharpFeaturesTest.V20.NullableTypes
{
    
    public class NullableTypesTests
    {
        [Fact]
        public void NullableTypesTest()
        {
            // reference type은 불가능. null을 표현할 수 없는 value type만 가능하다.
            // 만든 목적이 그거기 땜씨롱.
            // string? str = null;

            int? num = null;
            Assert.False(num.HasValue);
            Assert.Equal(default(int), num.GetValueOrDefault()); // "null이면 type default값을 리턴"

            num = 1;
            Assert.True(num.HasValue);
            Assert.Equal(1, num.Value);
            Assert.Equal(num.Value, num.GetValueOrDefault()); // "null이 아니므로 Value 리턴"
        }

        [Fact]
        public void ExceptionTest()
        {
            // int? num과 같다
            Nullable<int> num = null;
            // "HasValue가 false일 때, Value로 접근하면 예외 발생"
            Assert.Throws<InvalidOperationException>(() => num.Value);
        }

        [Fact]
        public void OperatorTest()
        {
            int? lhs = 1;
            int? rhs = null;

            Assert.True(lhs != rhs);
            Assert.False(lhs > rhs);
            Assert.False(lhs < rhs);
            Assert.Null(lhs + rhs);
            Assert.Null(lhs - rhs);
            Assert.Null(lhs * rhs);
            Assert.Null(lhs / rhs);
            Assert.Null(++rhs);
            Assert.Null(--rhs);

            // "null이 아니므로 ?? operator는 -1이 아닌 Value를 리턴"
            Assert.Equal(1, (lhs ?? -1));
            // "null이므로 -1을 리턴"
            Assert.Equal(-1, (rhs ?? -1));
        }

        [Fact]
        public void IdentifyTest()
        {
            int? i = 5;
            Assert.True("System.Int32" == i.GetType().FullName, "Nullable이 아닌 Int32 type으로 캐스팅된 정보를 리턴한다");
            Assert.True(i is int, "GetType() 결과와 마찬가지 결과");
        }
    }
}
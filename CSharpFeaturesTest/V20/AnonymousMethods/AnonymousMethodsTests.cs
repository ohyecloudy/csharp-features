// 참고:
// Anonymous Methods (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/0yw3tz5k

using Xunit;

namespace CSharpFeaturesTest.V20.AnonymousMethods
{
    public class AnonymousMethodsTests
    {
        delegate T Operator<T>(T lhs, T rhs);

        T ExecuteOperator<T>(Operator<T> op, T lhs, T rhs)
        {
            return op(lhs, rhs);
        }

        [Fact]
        public void AnonymousMethodsTest()
        {
            Operator<int> sum = delegate(int lhs, int rhs)
            {
                return lhs + rhs;
            };

            Assert.Equal(2 + 3, sum(2, 3));

            Assert.Equal(
                2 + 3,
                ExecuteOperator(
                    delegate(int lhs, int rhs) { return lhs + rhs; },
                    2, 3));
        }

        delegate int Del();

        [Fact]
        public void CapturedOrOuterVariablesTest()
        {
            int n = 0;

            // delegate 정의는 함수 내부에서 못한다.
            // delegate int Del();

            Del del = delegate() { return ++n; };

            Assert.Equal(1, del());
            Assert.True(1 == n, "n은 delegate에서 capture. n을 delegate 내부에서 증가했기 때문에 n이 1이다");
            Assert.Equal(2, del());
            Assert.Equal(3, del());
            Assert.Equal(3, n);
        }
    }
}
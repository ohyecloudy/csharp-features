﻿// 참고:
// checked (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/74b4xzyw.aspx
// unchecked (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/a569z7k8.aspx
// ?? Operator (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/ms173224.aspx

using Xunit;

namespace CSharpFeaturesTest.V1X.Operators
{
    
    public class OperatorsTests
    {
        static int maxIntVal = int.MaxValue;

        [Fact]
        public void CheckedTest()
        {
            bool raisedException = false;
            int n = 0;
            try
            {
                n = checked(maxIntVal + 10);

                // or
                // checked
                // {
                //     n = maxIntVal + 10
                // }
            }
            catch (System.OverflowException)
            {
                raisedException = true;
            }

            Assert.True(
                raisedException,
                "checked를 사용하면 산술 연산에서 overflow가 발생했을 때, 예외를 던져야 한다.");

            // "checked를 사용하면 overflow가 발생했을 때, 발생 이전 값을 유지해야 한다"
            Assert.True(0 == n);
        }

        [Fact]
        public void WithoutCheckedTest()
        {
            bool raisedException = false;
            int n = 0;
            try
            {
                n = maxIntVal + 10;
            }
            catch (System.OverflowException)
            {
                raisedException = true;
            }

            Assert.False(
                raisedException,
                "uncheck는 overflow가 발생했을 때 예외를 안 던져야 한다");

            Assert.Equal(-2147483639, n);
        }

        [Fact]
        public void UncheckedTest()
        {
            // 컴파일러에서 오버플로우를 검출해서 컴파일 에러. (친절하시네)
            // int n = int.MaxValue + 10;

            int n = unchecked(int.MaxValue + 10);
            //or
            //
            //unchecked
            //{
            //    n = int.MaxValue + 10;
            //}

            Assert.True(
                -2147483639 == n,
                "unchecked로 감싸주면 검사를 안 하기 때문에 컴파일 에러가 안 나야 한다");
        }

        [Fact]
        public void NullCoalescingTest()
        {
            {
                int? x = null;
                int y = x ?? -1;
                Assert.True(-1 == y, "null이면 rhs를 사용해야 한다");
            }

            {
                int? x = 1;
                int y = x ?? -1;
                Assert.True(1 == y, "null이 아니면 lhs를 사용해야 한다");
            }
        }
    }
}

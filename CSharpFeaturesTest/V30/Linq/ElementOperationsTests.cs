// 참고:
// Element Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546140

using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class ElementOperationsTests
    {
        // OrDefault 계열은 몇개만 테스트.
        // 생략: FirstOrDefault(), LastOrDefault(), SingleOrDefault()

        [Fact]
        public void ElementAtMethodTest()
        {
            IEnumerable<int> nums = Enumerable.Range(0, 5).Select(x => x * x);

            for (int i = 0; i < 5; ++i)
            {
                Assert.Equal(Math.Pow(i, 2), nums.ElementAt(i));
            }
        }

        [Fact]
        public void ElementAtOrDefaultMethodTest()
        {
            Assert.Equal(1, Enumerable.Range(1, 5).ElementAtOrDefault(0));
            Assert.Equal(default(int), Enumerable.Range(1, 5).ElementAtOrDefault(10));
            Assert.Equal(default(string), Enumerable.Empty<string>().ElementAtOrDefault(0));
        }

        [Fact]
        public void FirstMethodExceptionTest()
        {
            // "소스 시퀀스가 비었을 경우 예외를 던진다."
            Assert.Throws<InvalidOperationException>(() => Enumerable.Empty<int>().First());
        }

        [Fact]
        public void FirstMehtodTest()
        {
            Assert.Equal(5, Enumerable.Range(5, 2).First());

            Assert.True(
                6 == Enumerable.Range(5, 5).First(x => x > 5),
                "condition을 넣을 수 있다");
        }

        [Fact]
        public void LastMethodTest()
        {
            Assert.Equal(9, Enumerable.Range(5, 5).Last());
            Assert.Equal(6, Enumerable.Range(5, 5).Last(x => x < 7));
        }

        [Fact]
        public void SingleMethodTest()
        {
            string[] fruits1 = { "apple" };

            string fruit = fruits1.Single();
            Assert.Equal("apple", fruit);

            string[] fruits2 = { "orange", "apple" };
            string fruit2 = fruits2.Single(x => x.Length > 5);
            Assert.Equal("orange", fruit2);
        }
    }
}

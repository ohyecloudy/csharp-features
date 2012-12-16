// 참고:
// Element Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546140

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    [TestClass]
    public class ElementOperationsTests
    {
        // OrDefault 계열은 몇개만 테스트.
        // 생략: FirstOrDefault(), LastOrDefault(), SingleOrDefault()

        [TestMethod]
        public void ElementAtMethodTest()
        {
            IEnumerable<int> nums = Enumerable.Range(0, 5).Select(x => x * x);

            for (int i = 0; i < 5; ++i)
            {
                Assert.AreEqual(Math.Pow(i, 2), nums.ElementAt(i));
            }
        }

        [TestMethod]
        public void ElementAtOrDefaultMethodTest()
        {
            Assert.AreEqual(1, Enumerable.Range(1, 5).ElementAtOrDefault(0));
            Assert.AreEqual(default(int), Enumerable.Range(1, 5).ElementAtOrDefault(10));
            Assert.AreEqual(default(string), Enumerable.Empty<string>().ElementAtOrDefault(0));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "소스 시퀀스가 비었을 경우 예외를 던진다.")]
        public void FirstMethodExceptionTest()
        {
            Enumerable.Empty<int>().First();
        }

        [TestMethod]
        public void FirstMehtodTest()
        {
            Assert.AreEqual(5, Enumerable.Range(5, 2).First());

            Assert.AreEqual(
                6, 
                Enumerable.Range(5, 5).First(x => x > 5),
                "condition을 넣을 수 있다");
        }

        [TestMethod]
        public void LastMethodTest()
        {
            Assert.AreEqual(9, Enumerable.Range(5, 5).Last());
            Assert.AreEqual(6, Enumerable.Range(5, 5).Last(x => x < 7));
        }

        [TestMethod]
        public void SingleMethodTest()
        {
            string[] fruits1 = { "apple" };

            string fruit = fruits1.Single();
            Assert.AreEqual("apple", fruit);

            string[] fruits2 = { "orange", "apple" };
            string fruit2 = fruits2.Single(x => x.Length > 5);
            Assert.AreEqual("orange", fruit2);
        }
    }
}

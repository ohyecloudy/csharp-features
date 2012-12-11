// 참고:
// Partitioning Data - msdn
// http://msdn.microsoft.com/en-us/library/bb546164

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    [TestClass]
    public class PartitioningDataTests
    {
        [TestMethod]
        public void SkipMethodTest()
        {
            int[] nums = { 0, 1, 2, 3, 4 };

            IEnumerator<int> etor = nums.Skip(3).GetEnumerator();
            etor.MoveNext();

            Assert.AreEqual(3, etor.Current);
        }

        [TestMethod]
        public void SkipWhileMethodTest()
        {
            int[] nums = { 43, 22, 10, 55, 9 };

            IEnumerator<int> etor = nums.SkipWhile(n => n > 20).GetEnumerator();

            etor.MoveNext();
            Assert.AreEqual(10, etor.Current, "20보다 큰 원소는 skip");

            etor.MoveNext();
            Assert.AreEqual(
                55,
                etor.Current,
                "처음 열거하는 위치를 잡는다. 20보다 큰 수를 모두 걸러내는 게 아님." +
                "걸러내는 게 필요하다면 where를 쓰거나 소팅후 SkipWhile을 사용");
        }

        [TestMethod]
        public void TakeMethodTest()
        {
            int[] grades = { 59, 82, 70, 56, 92, 98, 85 };

            IEnumerator<int> topThreeGrades =
                grades.OrderByDescending(grade => grade).
                Take(3).GetEnumerator();

            topThreeGrades.MoveNext();
            Assert.AreEqual(98, topThreeGrades.Current);

            topThreeGrades.MoveNext();
            Assert.AreEqual(92, topThreeGrades.Current);

            topThreeGrades.MoveNext();
            Assert.AreEqual(85, topThreeGrades.Current);

            Assert.IsFalse(topThreeGrades.MoveNext());
        }

        [TestMethod]
        public void TakeWhileMethodTest()
        {
            string[] fruits = 
            { 
                "apple", "banana", "mango", "orange", "passionfruit", "grape" 
            };

            IEnumerator<string> etor =
                fruits.TakeWhile(fruit => String.Compare("orange", fruit, true) != 0).GetEnumerator();

            etor.MoveNext();
            Assert.AreEqual("apple", etor.Current);

            etor.MoveNext();
            Assert.AreEqual("banana", etor.Current);

            etor.MoveNext();
            Assert.AreEqual("mango", etor.Current);

            Assert.IsFalse(etor.MoveNext(), "orange와 다를때까지만 열거하므로 여기서 스톱");
        }
    }
}

// 참고:
// Partitioning Data - msdn
// http://msdn.microsoft.com/en-us/library/bb546164

using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class PartitioningDataTests
    {
        [Fact]
        public void SkipMethodTest()
        {
            int[] nums = { 0, 1, 2, 3, 4 };

            IEnumerator<int> etor = nums.Skip(3).GetEnumerator();
            etor.MoveNext();

            Assert.Equal(3, etor.Current);
        }

        [Fact]
        public void SkipWhileMethodTest()
        {
            int[] nums = { 43, 22, 10, 55, 9 };

            IEnumerator<int> etor = nums.SkipWhile(n => n > 20).GetEnumerator();

            etor.MoveNext();
            Assert.True(10 == etor.Current, "20보다 큰 원소는 skip");

            etor.MoveNext();
            Assert.True(
                etor.Current == 55,
                "처음 열거하는 위치를 잡는다. 20보다 큰 수를 모두 걸러내는 게 아님." +
                "걸러내는 게 필요하다면 where를 쓰거나 소팅후 SkipWhile을 사용");
        }

        [Fact]
        public void TakeMethodTest()
        {
            int[] grades = { 59, 82, 70, 56, 92, 98, 85 };

            IEnumerator<int> topThreeGrades =
                grades.OrderByDescending(grade => grade).
                Take(3).GetEnumerator();

            topThreeGrades.MoveNext();
            Assert.Equal(98, topThreeGrades.Current);

            topThreeGrades.MoveNext();
            Assert.Equal(92, topThreeGrades.Current);

            topThreeGrades.MoveNext();
            Assert.Equal(85, topThreeGrades.Current);

            Assert.False(topThreeGrades.MoveNext());
        }

        [Fact]
        public void TakeWhileMethodTest()
        {
            string[] fruits = 
            { 
                "apple", "banana", "mango", "orange", "passionfruit", "grape" 
            };

            IEnumerator<string> etor =
                fruits.TakeWhile(fruit => String.Compare("orange", fruit, true) != 0).GetEnumerator();

            etor.MoveNext();
            Assert.Equal("apple", etor.Current);

            etor.MoveNext();
            Assert.Equal("banana", etor.Current);

            etor.MoveNext();
            Assert.Equal("mango", etor.Current);

            Assert.False(etor.MoveNext(), "orange와 다를때까지만 열거하므로 여기서 스톱");
        }
    }
}

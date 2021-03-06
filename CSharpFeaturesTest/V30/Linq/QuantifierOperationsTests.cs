﻿// 참고:
// Quantifier Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546128

using Xunit;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class QuantifierOperationsTests
    {
        [Fact]
        public void AllMethodTest()
        {
            int[] nums = new int[] { 5, 6, 7, 8, 9, 10 };

            Assert.True(
                nums.All(n => n >= 5),
                "collection 모든 원소를 평가한다. 다 5보다 크기 때문에 true");

            Assert.False(nums.All(n => n < 10));
        }

        [Fact]
        public void AnyMethodTest()
        {
            int[] nums = new int[] { 5, 6, 7, 8, 9, 10 };

            Assert.True(
                nums.Any(n => n % 2 == 0),
                "하나 이상이 조건을 만족하면 true, 짝수는 여러개 존재하기 때문에 true");

            Assert.DoesNotContain(nums, n => n < 5);
        }

        [Fact]
        public void ContainsMethodTest()
        {
            string[] fruits = { "apple", "banana", "mango", "orange", "passionfruit", "grape" };

            Assert.True(fruits.Contains("banana") == true);
            Assert.False(fruits.Contains("Banana") == true);
        }
    }
}

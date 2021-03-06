﻿// 참고:
// Aggregation Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546138

using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class AggregationOperationsTests
    {
        [Fact]
        public void AggregateMethodTest()
        {
            // Average(), Max(), Min(), Sum()은 helper
            // Decimal, Double, Int32, Int64, Single type 지원
            
            {
                string sentence = "the quick brown fox jumps over the lazy dog";
                string[] words = sentence.Split(' ');

                string reversed = words.Aggregate(
                    (workingSentence, next) => next + " " + workingSentence);
                Assert.Equal(
                    "dog lazy the over jumps fox brown quick the",
                    reversed);
            }

            {
                int[] ints = { 4, 8, 8, 3, 9, 0, 7, 8, 2 };
                const int SEED = 0;

                Assert.Equal(
                    6,
                    ints.Aggregate(SEED, (total, next) => next % 2 == 0 ? total + 1 : total));
            }

            {
                string[] fruits = { "apple", "mango", "orange", "passionfruit", "grape" };
                const string SEED = "banana";

                string longestName =
                    fruits.Aggregate(
                        SEED,
                        (longest, next) => next.Length > longest.Length ? next : longest,
                        fruit => fruit.ToUpper()); // result selector
                Assert.Equal("PASSIONFRUIT", longestName);
            }
        }

        [Fact]
        public void AverageMethodTest()
        {
            // Decimal, Double, Int32, Int64, Single 가능
            Assert.Equal(
                5.5,
                Enumerable.Range(1, 10).Average());

            int?[] nums = { 1, 2, 3, null };
            // "null 원소는 아예 제외. / 4 가 아님을 주목"
            Assert.Equal(
                (1 + 2 + 3) / 3,
                nums.Average());

            string[] fruits = { "apple", "banana", "mango", "orange", "passionfruit", "grape" };
            Assert.Equal(
                6.5,
                fruits.Average(s => s.Length));
        }

        [Fact]
        public void CountMethodTest()
        {
            IEnumerable<string> fruits = new string[] { "apple", "banana", "mango", "orange", "passionfruit", "grape" };
            Assert.Equal(6, fruits.Count());

            Func<string, bool> predicate = (string x) => x.Length >= 6;
            Assert.Equal(3, fruits.Count(predicate));
        }

        [Fact]
        public void LongCountMethodTest()
        {
            // Count와 return 타입만 다름

            IEnumerable<string> fruits = new string[] {};
            Assert.Equal("Int64", fruits.LongCount().GetType().Name);
        }
    }
}

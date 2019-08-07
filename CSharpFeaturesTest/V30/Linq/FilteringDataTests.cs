// 참고:
// Filtering Data - msdn
// http://msdn.microsoft.com/en-us/library/bb546161
// Basic LINQ Query Operations (C#) - msdn
// http://msdn.microsoft.com/en-us/library/bb397927
// where clause (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/bb311043

using Xunit;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class FilteringDataTests
    {
        static bool IsEven(int i)
        {
            return i % 2 == 0;
        }

        [Fact]
        public void WhereClauseTest()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var queryLowNum2 =
                from num in numbers
                where num < 5 && num % 2 == 0
                select num;

            Assert.True(3 == queryLowNum2.Count(), "0, 2, 4 이렇게 3개");

            var queryHighEvenNums =
                from num in numbers
                where num > 5 && IsEven(num)
                select num;

            Assert.True(2 == queryHighEvenNums.Count(), "6, 8 이렇게 2개");
        }

        [Fact]
        public void OfTypeMethodTest()
        {
            ArrayList fruits = new System.Collections.ArrayList();
            fruits.Add("Mango");
            fruits.Add("Orange");
            fruits.Add("Apple");
            fruits.Add(3.0);
            fruits.Add("Banana");

            // type을 지정해 그 type으로 cast 할 수 있는 element만 열거
            IEnumerable<string> query = fruits.OfType<string>();
            Assert.True(4 == query.Count(), "3.0은 제외한 나머지 4개");
        }
    }
}

// 참고:
// Standard Query Operators Overview - msdn
// http://msdn.microsoft.com/en-us/library/bb397896
// Sorting Data - msdn
// http://msdn.microsoft.com/en-us/library/bb546145
// Basic LINQ Query Operations (C#) - msdn
// http://msdn.microsoft.com/en-us/library/bb397927
// orderby clause (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/bb383982

using Xunit;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class SortingDataTests
    {
        struct Customer
        {
            public string City { get; set; }
            public string Name { get; set; }
        }

        [Fact]
        public void OrderByClauseTest()
        {
            // OrderBy, OrderByDescending, ThenBy, ThenByDescending는 
            // orderby syntax로 사용가능

            Customer[] customers = new Customer[]
            {
                new Customer()
                {
                    City = "Seoul",
                    Name = "a",
                },

                new Customer()
                {
                    City = "London",
                    Name = "b",
                },

                new Customer()
                {
                    City = "NewYork",
                    Name = "z",
                },
            };

            var query =
                (from cust in customers
                 where cust.City != "London"
                 orderby cust.Name descending // ascending이 default
                 select cust).ToArray();

            Assert.Equal("z", query[0].Name); // "Customer 이름으로 내림차순 정렬이기 때문"
            Assert.Equal("a", query[1].Name);
        }

        [Fact]
        public void ReverseMethodTest()
        {
            int[] nums = new int[] { 1, 2, 3 };

            var query =
                (from n in nums
                 select n).Reverse().ToArray();

            Assert.Equal(3, query[0]);
            Assert.Equal(2, query[1]);
            Assert.Equal(1, query[2]);
        }
    }
}
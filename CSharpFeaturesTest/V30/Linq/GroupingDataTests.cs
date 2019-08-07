// 참고:
// Grouping Data - msdn
// http://msdn.microsoft.com/en-us/library/bb546139
// Basic LINQ Query Operations (C#) - msdn
// http://msdn.microsoft.com/en-us/library/bb397927
// group clause (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/bb384063
// into (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/bb311045

using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class GroupingDataTests
    {
        struct Customer
        {
            public string City { get; set; }
            public string Name { get; set; }
            public int ID { get; set; }
        }

        struct Student
        {
            public string LastName { get; set; }
            public List<int> Scores { get; set; }
        }

        [Fact]
        public void GroupByStringClauseTest()
        {
            Customer[] customers = new Customer[]
            {
                new Customer()
                {
                    City = "Seoul",
                    Name = "seoul 1",
                },

                new Customer()
                {
                    City = "London",
                    Name = "london 1",
                },

                new Customer()
                {
                    City = "Seoul",
                    Name = "seoul 2",
                },
            };

            var query =
                from cust in customers
                group cust by cust.City;

            Assert.Equal("GroupedEnumerable`2", query.GetType().Name);
            Assert.True(
                query as IEnumerable<IGrouping<string, Customer>> != null,
                "IEnumerable<IGrouping<>> 인터페이스 상속");

            var queryResultArray = query.ToArray();
            Assert.Equal("Seoul", queryResultArray[0].Key);
            Assert.Equal("London", queryResultArray[1].Key);

            Assert.Equal(2, queryResultArray[0].Count());
            Assert.True(1 == queryResultArray[1].Count());

            // 중첩 foreach로 전체 group을 순회할 수 있다.
            //foreach (var custGroup in query)
            //{
            //    foreach (var cust in custGroup)
            //    {
            //    }
            //}
        }

        [Fact]
        public void GroupByBoolClauseTest()
        {
            List<Student> students = new List<Student>()
            {
                new Student { LastName="Omelchenko", Scores= new List<int> {97, 72, 81, 60} },
                new Student { LastName="O'Donnell", Scores= new List<int> {75, 84, 91, 39} },
                new Student { LastName="Mortensen", Scores= new List<int> {88, 94, 65, 85} },
                new Student { LastName="Garcia", Scores= new List<int> {97, 89, 85, 82} },
                new Student { LastName="Beebe", Scores= new List<int> {35, 72, 91, 70} } 
            };

            // by 뒤에 clause 결과가 boolean.
            var query =
                from student in students
                group student by student.Scores.Average() >= 80;

            var queryResult = query.ToArray();

            Assert.False(queryResult[0].Key, "boolean 값을 key로 grouping");
            Assert.True(queryResult[1].Key);
        }

        [Fact]
        public void GroupByNumericRangeTest()
        {
            List<Student> students = new List<Student>()
            {
                new Student { LastName="Omelchenko", Scores= new List<int> {97, 72, 81, 60} },
                new Student { LastName="O'Donnell", Scores= new List<int> {75, 84, 91, 39} },
                new Student { LastName="Mortensen", Scores= new List<int> {88, 94, 65, 85} },
                new Student { LastName="Garcia", Scores= new List<int> {97, 89, 85, 82} },
                new Student { LastName="Beebe", Scores= new List<int> {35, 72, 91, 70} } 
            };

            // group by로 나온 결과물을 한차례 더 가공하려면 into로 결과물을 저장한 다음
            // 그 결과물에 연산을 수행한다.
            // into는 group, join, select 와 같이 사용할 수 있다.
            var query =
                from student in students
                let avg = (int)student.Scores.Average()
                group student by (avg == 0 ? 0 : avg / 10) into g
                orderby g.Key
                select g;

            Assert.True(query is IEnumerable<IGrouping<int, Student>>);

            var queryResult = query.ToArray();
            Assert.Equal(6, queryResult[0].Key);
            Assert.Equal(7, queryResult[1].Key);
            Assert.Equal(8, queryResult[2].Key);
        }

        [Fact]
        public void GroupByCompositeKeysTest()
        {
            Customer[] customers = new Customer[]
            {
                new Customer()
                {
                    City = "Seoul",
                    Name = "a",
                    ID = 4,
                },

                new Customer()
                {
                    City = "London",
                    Name = "b",
                    ID = 5,
                },

                new Customer()
                {
                    City = "NewYork",
                    Name = "z",
                    ID = 1,
                },

                new Customer()
                {
                    City = "Seoul",
                    Name = "a",
                    ID = 7,
                },
            };

            var query =
                from cust in customers
                group cust by new { name = cust.Name, city = cust.City } into g
                orderby g.Key.city
                select g;

            var queryResult = query.ToArray();
            Assert.Equal(3, queryResult.Length); // "name과 city로 grouping. 그래서 4개가 아닌 3개"

            Assert.Equal("London", queryResult[0].Key.city);
            Assert.Single(queryResult[0]);
            Assert.Equal("NewYork", queryResult[1].Key.city);
            Assert.Single(queryResult[1]);
            Assert.Equal("Seoul", queryResult[2].Key.city);
            Assert.Equal(2, queryResult[2].Count());
        }

        [Fact]
        public void ToLookupMethodTest()
        {
            List<Student> students = new List<Student>()
            {
                new Student { LastName="Omelchenko", Scores= new List<int> {97, 72, 81, 60} },
                new Student { LastName="O'Donnell", Scores= new List<int> {75, 84, 91, 39} },
                new Student { LastName="Mortensen", Scores= new List<int> {88, 94, 65, 85} },
                new Student { LastName="Garcia", Scores= new List<int> {97, 89, 85, 82} },
                new Student { LastName="Beebe", Scores= new List<int> {35, 72, 91, 70} } 
            };

            ILookup<char, string> lookup =
                students.ToLookup(
                    s => Convert.ToChar(s.LastName.Substring(0, 1)),
                    s => s.LastName + " " + s.Scores.ToString());

            Assert.True(4 == lookup.Count(), "LastName 첫 글자를 키로 했기 때문에 5개가 아닌 4개");

            IEnumerator<IGrouping<char, string>> etor = lookup.GetEnumerator();
            etor.MoveNext();

            Assert.Equal('O', etor.Current.Key);
            Assert.True(2 == etor.Current.Count(), "LastName이 Omelchenko, O'Donnell. 이렇게 두개");
        }
    }
}

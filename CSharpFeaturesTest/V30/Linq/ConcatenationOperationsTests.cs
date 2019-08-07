// 참고:
// Concatenation Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546141

using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class ConcatenationOperationsTests
    {
        class Student
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public List<int> Scores;
        }

        class Teacher
        {
            public string First { get; set; }
            public string Last { get; set; }
            public int ID { get; set; }
            public string City { get; set; }
        }

        [Fact]
        public void ConcatMethodTest()
        {
            List<Student> students = new List<Student>()
            {
                new Student 
                {
                    First="Svetlana",
                    Last="Omelchenko", 
                    ID=111, 
                    Street="123 Main Street",
                    City="Seattle",
                    Scores= new List<int> {97, 92, 81, 60}
                },
                new Student 
                {
                    First="Claire",
                    Last="O’Donnell", 
                    ID=112,
                    Street="124 Main Street",
                    City="Redmond",
                    Scores= new List<int> {75, 84, 91, 39}
                },
                new Student 
                {
                    First="Sven",
                    Last="Mortensen",
                    ID=113,
                    Street="125 Main Street",
                    City="Lake City",
                    Scores= new List<int> {88, 94, 65, 91}
                },
            };

            List<Teacher> teachers = new List<Teacher>()
            {                
                new Teacher { First="Ann", Last="Beebe", ID=945, City = "Seattle" },
                new Teacher { First="Alex", Last="Robinson", ID=956, City = "Redmond" },
                new Teacher { First="Michiyo", Last="Sato", ID=972, City = "Tacoma" }
            };

            // Concat()으로 query 결과를 이어 붙인다.
            var query =
                (from s in students
                 where s.City == "Seattle"
                 select s.Last).Concat
                (from t in teachers
                 where t.City == "Seattle"
                 select t.Last);

            var queryResult = query.ToArray();
            Assert.Equal(2, queryResult.Length);

            Assert.Equal("Omelchenko", queryResult[0]);
            Assert.Equal("Beebe", queryResult[1]);
        }
    }
}

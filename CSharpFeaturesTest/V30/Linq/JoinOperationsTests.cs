// 참고:
// Join Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb397908
// Basic LINQ Query Operations (C#) - msdn
// http://msdn.microsoft.com/en-us/library/bb397927
// join clause (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/bb311040
// How to: Perform Inner Joins (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb397941
// How to: Perform Grouped Joins (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb397905
// How to: Perform Left Outer Joins (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb397895
// How to: Perform Custom Join Operations (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb882533

using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class JoinOperationsTests
    {
        // ValueType인 struct를 사용하면 비었는지 검사가 불가능하므로 (null과 비교)
        // left outer join이 불가능
        class Employee
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int EmployeeID { get; set; }
        }

        class Student
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public int StudentID { get; set; }
        }

        List<Employee> employees = new List<Employee>()
            {
                new Employee { FirstName = "Terry", LastName = "Adams", EmployeeID = 522459 },
                new Employee { FirstName = "Charlotte", LastName = "Weiss", EmployeeID = 204467 },
                new Employee { FirstName = "Magnus", LastName = "Hedland", EmployeeID = 866200 },
                new Employee { FirstName = "Vernette", LastName = "Price", EmployeeID = 437139 }
            };

        List<Student> students = new List<Student>()
            {
                new Student { FirstName = "Vernette", LastName = "Price", StudentID = 9562 },
                new Student { FirstName = "Terry", LastName = "Earls", StudentID = 9870 },
                new Student { FirstName = "Terry", LastName = "Adams", StudentID = 9913 }
            };

        [Fact]
        public void InnerJoinTest()
        {
            var query =
                from e in employees
                join s in students on e.FirstName equals s.FirstName
                orderby s.StudentID
                select new { eID = e.EmployeeID, sID = s.StudentID };

            var queryResult = query.ToArray();
            Assert.Equal(3, queryResult.Length);
            Assert.Equal(437139, queryResult[0].eID);
            Assert.Equal(9562, queryResult[0].sID);
            Assert.Equal(522459, queryResult[1].eID);
            Assert.Equal(9870, queryResult[1].sID);
            Assert.Equal(522459, queryResult[2].eID);
            Assert.Equal(9913, queryResult[2].sID);
        }

        [Fact]
        public void CompositeKeyJoinTest()
        {
            var query =
                from e in employees
                join s in students
                    on new { e.FirstName, e.LastName }
                    equals new { s.FirstName, s.LastName }
                orderby e.EmployeeID
                select new { eID = e.EmployeeID, sID = s.StudentID };

            var queryResult = query.ToArray();
            Assert.Equal(2, queryResult.Count());

            Assert.Equal(437139, queryResult[0].eID);
            Assert.Equal(522459, queryResult[1].eID);
        }

        [Fact]
        public void GroupJoinTest()
        {
            // group join은 hierarchical 결과 시퀀스를 만들 수 있다.
            var query =
                from e in employees
                join s in students on e.FirstName equals s.FirstName into prodGroup
                orderby e.FirstName
                select new { firstName = e.FirstName, studentGroup = prodGroup };

            var queryResult = query.ToArray();
            Assert.Equal(4, queryResult.Length);

            Assert.Equal("Charlotte", queryResult[0].firstName);
            Assert.Empty(queryResult[0].studentGroup);

            Assert.Equal("Magnus", queryResult[1].firstName);
            Assert.Empty(queryResult[1].studentGroup);

            Assert.Equal("Terry", queryResult[2].firstName);
            Assert.True(2 == queryResult[2].studentGroup.Count(), "Terry 이름을 가진 student는 2개");

            Assert.Equal("Vernette", queryResult[3].firstName);
            Assert.Single(queryResult[3].studentGroup);
        }

        [Fact]
        public void LeftOuterJoinTest()
        {
            // DefaultIfEmpty()를 사용해 join clause 조건에 부합하지 않은 원소는 default 값을 넣는다.
            // reference type default는 null. 
            var query =
                from e in employees
                join s in students on e.FirstName equals s.FirstName into prodGroup
                from prod in prodGroup.DefaultIfEmpty()
                orderby e.EmployeeID
                select new { eId = e.EmployeeID, sId = (prod == null ? 0 : prod.StudentID) };

            var queryResult = query.ToArray();
            Assert.Equal(5, queryResult.Length);

            Assert.True(204467 == queryResult[0].eId, "Charlotte");
            Assert.True(0 == queryResult[0].sId, "Charlotte은 students에 존재하지 않는다. 그래서 sId가 0 할당");
        }

        [Fact]
        public void NonEquiJoinTest()
        {
            // let으로 임시 시퀀스를 만든 다음 거기에 포함됐는지 여부를 검사해
            // Non-Equijoin을 구현
            var query =
                from e in employees
                let firstNames = from s in students select s.FirstName
                where !firstNames.Contains(e.FirstName)
                orderby e.EmployeeID
                select e;

            var queryResult = query.ToArray();
            Assert.Equal(2, queryResult.Length);

            Assert.Equal("Weiss", queryResult[0].LastName);
            Assert.Equal("Hedland", queryResult[1].LastName);
        }
    }
}

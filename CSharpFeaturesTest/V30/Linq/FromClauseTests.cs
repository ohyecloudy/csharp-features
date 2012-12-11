// 참고:
// Basic LINQ Query Operations (C#) - msdn
// http://msdn.microsoft.com/en-us/library/bb397927
// from clause (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/bb383978

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    [TestClass]
    public class FromClauseTests
    {
        struct Customer
        {
            public string City { get; set; }
        }

        struct Student
        {
            public string LastName { get; set; }
            public List<int> Scores { get; set; }
        }

        [TestMethod]
        public void BasicTest()
        {
            Customer[] customers = new Customer[]
            {
                new Customer()
                {
                    City = "Seoul",
                },

                new Customer()
                {
                    City = "London",
                },

                new Customer()
                {
                    City = "NewYork",
                },
            };

            var allCustomers =
                from c in customers
                select c;

            Customer[] resultCustomers = allCustomers.ToArray();
            CollectionAssert.AreEqual(customers, resultCustomers);
        }

        [TestMethod]
        public void CompoundFromClauseTest()
        {
            List<Student> students = new List<Student>()
            {
                new Student { LastName="Omelchenko", Scores= new List<int> {97, 72, 81, 60} },
                new Student { LastName="O'Donnell", Scores= new List<int> {75, 84, 91, 39} },
                new Student { LastName="Mortensen", Scores= new List<int> {88, 94, 65, 85} },
                new Student { LastName="Garcia", Scores= new List<int> {97, 89, 85, 82} },
                new Student { LastName="Beebe", Scores= new List<int> {35, 72, 91, 70} } 
            };

            var scoreQuery =
                from student in students
                from score in student.Scores
                where score > 90
                select new { Last = student.LastName, score };

            Assert.AreEqual(5, scoreQuery.Count(), "97, 91, 94, 97, 91 이렇게 5개");
        }

        [TestMethod]
        public void MultipleFromClauseTest()
        {
            char[] upperCase = { 'A', 'B', 'C' };
            char[] lowerCase = { 'x', 'y', 'z' };

            // cartesian product
            var joinQuery1 =
                from upper in upperCase
                from lower in lowerCase
                select new { upper, lower };

            Assert.AreEqual(
                upperCase.Length * lowerCase.Length,
                joinQuery1.Count());

            var joinQuery2 =
                from lower in lowerCase
                where lower != 'x'
                from upper in upperCase
                select new { lower, upper };

            Assert.AreEqual(
                (lowerCase.Length - 1) * upperCase.Length,
                joinQuery2.Count());
        }

    }
}

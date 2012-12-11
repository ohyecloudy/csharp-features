// 참고:
// Converting Data Types - msdn
// http://msdn.microsoft.com/en-us/library/bb546162

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSharpFeaturesTest.V30.Linq
{
    [TestClass]
    public class ConvertingDataTypesTests
    {
        class Clump<T> : List<T>
        {
            public bool ClumpWhereMethodCalled { set; get; }

            public IEnumerable<T> Where(Func<T, bool> predicate)
            {
                ClumpWhereMethodCalled = true;
                return Enumerable.Where(this, predicate);
            }
        }

        [TestMethod]
        public void AsEnumerableMethodTest()
        {
            Clump<string> fruitClump =
                new Clump<string> { "apple", "mango", "banana" };

            Assert.IsFalse(fruitClump.ClumpWhereMethodCalled);
            fruitClump.Where(x => x.Contains("go"));
            Assert.IsTrue(fruitClump.ClumpWhereMethodCalled);

            fruitClump.ClumpWhereMethodCalled = false;
            fruitClump.AsEnumerable().Where(x => x.Contains("go"));

            Assert.IsFalse(
                fruitClump.ClumpWhereMethodCalled,
                "IEnumerable<>로 casting. 그래서 Clump:Where는 감춰진다.");
        }

        [TestMethod]
        public void AsQueryableMethodTest()
        {
            List<int> grades = new List<int> { 78, 92, 100, 37, 81 };

            IQueryable<int> queryable = grades.AsQueryable();
            Expression expressionTree = queryable.Expression;

            Assert.AreEqual("Constant", expressionTree.NodeType.ToString());
            Assert.AreEqual("EnumerableQuery`1", expressionTree.Type.Name);
        }

        [TestMethod]
        public void CastMethodTest()
        {
            ArrayList fruits = new ArrayList();
            fruits.Add("mango");
            fruits.Add("apple");
            fruits.Add("lemon");

            Assert.AreEqual("apple", fruits.Cast<string>().OrderBy(x => x).First());
            Assert.AreEqual(
                "apple",
                (from string f in fruits orderby f select f).First(),
                "from 다음에 type을 적는다. syntax로 지원");
        }

        [TestMethod]
        public void ToArrayMethodTest()
        {
            IEnumerable<int> enumerable = new int[] { 1, 2, 3, 4 };

            Assert.AreEqual("Int32[]", enumerable.ToArray().GetType().Name);
        }

        class Package
        {
            public string Company { get; set; }
            public double Weight { get; set; }
            public long TrackingNumber { get; set; }
        }

        [TestMethod]
        public void ToDictionaryMethodTest()
        {
            List<Package> packages =
                new List<Package>
                {
                    new Package { Company = "Coho Vineyard", Weight = 25.2, TrackingNumber = 89453312L },
                    new Package { Company = "Lucerne Publishing", Weight = 18.7, TrackingNumber = 89112755L },
                    new Package { Company = "Wingtip Toys", Weight = 6.0, TrackingNumber = 299456122L },
                    new Package { Company = "Adventure Works", Weight = 33.8, TrackingNumber = 4665518773L } 
                };

            Dictionary<long, Package> dic =
                packages.ToDictionary(p => p.TrackingNumber);
            
            foreach (var d in dic)
            {
                Assert.AreEqual(d.Key, d.Value.TrackingNumber);
            }
        }

        [TestMethod]
        public void ToListMethodTest()
        {
            IEnumerable<int> enumerable = new int[] { 1, 2, 3, 4 };

            Assert.AreEqual("List`1", enumerable.ToList().GetType().Name);
        }
    }
}

// 참고:
// Converting Data Types - msdn
// http://msdn.microsoft.com/en-us/library/bb546162

using Xunit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CSharpFeaturesTest.V30.Linq
{
    
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

        [Fact]
        public void AsEnumerableMethodTest()
        {
            Clump<string> fruitClump =
                new Clump<string> { "apple", "mango", "banana" };

            Assert.False(fruitClump.ClumpWhereMethodCalled);
            fruitClump.Where(x => x.Contains("go"));
            Assert.True(fruitClump.ClumpWhereMethodCalled);

            fruitClump.ClumpWhereMethodCalled = false;
            fruitClump.AsEnumerable().Where(x => x.Contains("go"));

            Assert.False(
                fruitClump.ClumpWhereMethodCalled,
                "IEnumerable<>로 casting. 그래서 Clump:Where는 감춰진다.");
        }

        [Fact]
        public void AsQueryableMethodTest()
        {
            List<int> grades = new List<int> { 78, 92, 100, 37, 81 };

            IQueryable<int> queryable = grades.AsQueryable();
            Expression expressionTree = queryable.Expression;

            Assert.Equal("Constant", expressionTree.NodeType.ToString());
            Assert.Equal("EnumerableQuery`1", expressionTree.Type.Name);
        }

        [Fact]
        public void CastMethodTest()
        {
            ArrayList fruits = new ArrayList();
            fruits.Add("mango");
            fruits.Add("apple");
            fruits.Add("lemon");

            Assert.Equal("apple", fruits.Cast<string>().OrderBy(x => x).First());

            // "from 다음에 type을 적는다. syntax로 지원"
            Assert.Equal(
                "apple",
                (from string f in fruits orderby f select f).First());
        }

        [Fact]
        public void ToArrayMethodTest()
        {
            IEnumerable<int> enumerable = new int[] { 1, 2, 3, 4 };

            Assert.Equal("Int32[]", enumerable.ToArray().GetType().Name);
        }

        class Package
        {
            public string Company { get; set; }
            public double Weight { get; set; }
            public long TrackingNumber { get; set; }
        }

        [Fact]
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
                Assert.Equal(d.Key, d.Value.TrackingNumber);
            }
        }

        [Fact]
        public void ToListMethodTest()
        {
            IEnumerable<int> enumerable = new int[] { 1, 2, 3, 4 };

            Assert.Equal("List`1", enumerable.ToList().GetType().Name);
        }
    }
}

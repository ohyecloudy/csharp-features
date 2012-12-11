// 참고:
// Getting Started with LINQ in C# - msdn
// http://msdn.microsoft.com/en-US/library/bb397933
// Introduction to LINQ Queries (C#) - msdn
// http://msdn.microsoft.com/en-US/library/bb397906
// How to: Query an ArrayList with LINQ - msdn
// http://msdn.microsoft.com/en-us/library/bb397937

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    [TestClass]
    public class LinqBasicTests
    {
        [TestMethod]
        public void LinqBasicTest()
        {
            // 1. data source
            int[] numbers = new int[] { 0, 1, 2, 3, 4, 5, 6 };

            // 2. query creation
            var numQuery =
                from num in numbers
                where (num % 2) == 0
                select num;

            Assert.AreEqual("WhereArrayIterator`1", numQuery.GetType().Name);
            
            // 3. query execution
            Assert.AreEqual(4, numQuery.Count());

            int result = 0;
            foreach (int num in numQuery)
            {
                Assert.AreEqual(result, num);
                result += 2;
            }

            {
                IEnumerator<int> enumerator = numQuery.GetEnumerator();
                enumerator.MoveNext();
                Assert.AreEqual(0, enumerator.Current);

                enumerator.MoveNext();
                Assert.AreEqual(2, enumerator.Current);
                
                enumerator.MoveNext();
                Assert.AreEqual(4, enumerator.Current);

                enumerator.MoveNext();
                Assert.AreEqual(6, enumerator.Current);
            }
        }

        // IEnumerable<> 또는 IQueryable<> 인터페이스를 구현하면 data source로 사용할 수 있다.
        class CustomCollection : IEnumerable<int>
        {
            public IEnumerator<int> GetEnumerator()
            {
                for (int i = 0; i < 10; ++i)
                {
                    yield return i;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        class CustomCollection2 : IEnumerable
        {
            IEnumerator IEnumerable.GetEnumerator()
            {
                for (int i = 0; i < 10; ++i)
                {
                    yield return i;
                }
            }
        }

        [TestMethod]
        public void CustomCollectionTest()
        {           
            CustomCollection coll = new CustomCollection();

            var query =
                from c in coll
                where (c % 2) == 1
                select c;

            int result = 1;
            foreach (int n in query)
            {
                Assert.AreEqual(result, n);
                result += 2;
            }
        }

        [TestMethod]
        public void CustomNonGenericCollectionTest()
        {
            CustomCollection2 coll = new CustomCollection2();

            // generic이 아닌 경우 from에 type을 명시해야 한다.
            // 여기서 int c.
            var query =
                from int c in coll
                where (c % 2) == 1
                select c;

            int result = 1;
            foreach (int n in query)
            {
                Assert.AreEqual(result, n);
                result += 2;
            }
        }
    }
}

// 참고:
// Object and Collection Initializers (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb384062

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharpFeaturesTest.V30.CollectionInitializers
{
    [TestClass]
    public class CollectionInitializersTest
    {
        class Cat
        {
            public int Age { get; set; }
            public string Name { get; set; }
        }

        class IntGenerator
        {
            public static int Add(int lhs, int rhs)
            {
                return lhs + rhs;
            }
        }

        [TestMethod]
        public void BasicTest()
        {
            // 연산은 물론 함수도 넣을 수 있다.
            List<int> digits = new List<int> { 0, 1, 2, 1 + 2, 12 / 3, 17 % 6, IntGenerator.Add(2, 4) };

            for (int i = 0; i < digits.Count; ++i)
            {
                Assert.AreEqual(i, digits[i]);
            }

            List<Cat> cats = new List<Cat>
            {
                new Cat() { Name = "Sylvester", Age=8 },
                new Cat() { Name = "Whiskers", Age=2 },
                null,
                new Cat() { Name = "Sasha", Age=14 }
            };

            Assert.AreEqual("Sylvester", cats[0].Name);
            Assert.AreEqual(14, cats[3].Age);
            Assert.IsNull(cats[2]);    
        }

        class FixedIntArray : IEnumerable
        {
            private int[] member = new int[10];
            private int addIdx = 0;

            // collection initializer를 사용하려면 IEnumerable 인터페이스를 구현. 
            // 그리고 Add public method가 존재해야 한다
            IEnumerator IEnumerable.GetEnumerator()
            {
                foreach (int i in member)
                {
                    yield return i;
                }
            }

            public void Add(int v)
            {
                if (addIdx >= member.Length)
                {
                    throw new OverflowException();
                }

                member[addIdx] = v;
                addIdx++;
            }

            public readonly int Length = 10;
        }

        [TestMethod]
        public void IEnumerableCollectionTest()
        {
            // custom collection도 필요한 사항을 만족하면 collection initializer 사용 가능
            FixedIntArray arr = new FixedIntArray { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            int idx = 0;
            foreach (var v in arr)
            {
                Assert.AreEqual(idx, v);
                idx++;
            }            
        }
    }
}

// 참고:
// Iterators (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/dscyy5s0(v=vs.80).aspx

using Xunit;
using System.Collections;
using System.Collections.Generic;

namespace CSharpFeaturesTest.V20.Iterators
{
    
    public class IteratorsTests
    {
        class DayOfTheWeek : IEnumerable
        {
            string[] days = { "Sun", "Mon", "Tue", "Wed", "Thr", "Fri", "Sat" };

            // IEnumerator 메서드 Current, MoveNext, Reset를 컴파일러가 알아서 자동 생성해준다. 편함.
            public IEnumerator GetEnumerator()
            {
                foreach (string day in days)
                {
                    // 현재 위치가 저장되고 iterator가 다시 호출됐을 때, 저장한 위치에서 다시 시작.
                    yield return day;
                }
            }
        }

        [Fact]
        public void IteratorsTest()
        {
            List<string> l = new List<string>();

            // foreach 문에서 new로 새로 할당할 수도 있다.
            foreach (string day in new DayOfTheWeek())
            {
                l.Add(day);
            }

            Assert.Equal(7, l.Count);
            Assert.Equal("Sun", l[0]);
            Assert.Equal("Mon", l[1]);
            Assert.Equal("Tue", l[2]);
            Assert.Equal("Wed", l[3]);
            Assert.Equal("Thr", l[4]);
            Assert.Equal("Fri", l[5]);
            Assert.Equal("Sat", l[6]);
        }

        class CustomArray<T> : IEnumerable<T>
        {
            // generics에서는 type parameter로 상수를 넘길 수 없다.
            // <T, N> 을 받고 new T[N] 과 같은 방식이 불가능.
            public T[] data = new T[10];

            public IEnumerator<T> GetEnumerator()
            {
                foreach (T d in data)
                {
                    yield return d;
                }
            }

            // public interface IEnumerable<out T> : IEnumerable 이기 때문에 IEnumerable 인터페이스도 구현 해야 함.
            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            // IEnumerable<T>를 리턴해도 컴파일러가 해당 인터페이스를 구현해 줌.
            public IEnumerable<T> ReverseIterator()
            {
                for (int i = data.Length - 1; i <= 0; --i)
                {
                    yield return data[i];
                }
            }
        }

        [Fact]
        public void GenericIteratorTest()
        {
            CustomArray<int> arr = new CustomArray<int>();
            for (int i = 0; i < 10; ++i)
            {
                arr.data[i] = i * 2;
            }

            int idx = 0;
            foreach (int i in arr)
            {
                Assert.Equal(idx * 2, i);
                idx++;
            }
        }

        [Fact]
        public void IEnumerableReturnTest()
        {
            CustomArray<int> arr = new CustomArray<int>();
            for (int i = 0; i < 10; ++i)
            {
                arr.data[i] = i;
            }

            int val = 9;
            foreach (int i in arr.ReverseIterator())
            {
                Assert.Equal(val, i);
                --val;
            }
        }

        static IEnumerable SomeNumbers()
        {
            // multiple yield return 사용.
            // 하나씩 차례로 리턴한다.
            yield return 0;
            yield return 3;
            yield return 5;
        }

        [Fact]
        public void MultipleYieldReturnTest()
        {
            int[] expectedArr = { 0, 3, 5 };
            int idx = 0;
            foreach (int i in SomeNumbers())
            {
                Assert.Equal(expectedArr[idx], i);
                idx++;
            }
        }
    }
}

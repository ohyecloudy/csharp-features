// 참고:
// Generics and Arrays - msdn
// http://msdn.microsoft.com/en-us/library/ms228502.aspx

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CSharpFeaturesTest.V20.Generics
{
    // generics에서 operator + 를 호출하기 위한 눈물겨운 노력
    // 참고 : http://www.codeproject.com/Articles/8531/Using-generics-for-calculations
    public abstract class Calcurator<T>
    {
        public abstract T Add(T a, T b);
    }

    public class Calcurator : Calcurator<int>
    {
        public override int Add(int a, int b)
        {
            return a + b;
        }
    }

    public static class ProcessItems
    {
        public static T Accumulate<T>(IList<T> items, Calcurator<T> cal)
        {
            T sum = default(T);

            foreach (T item in items)
            {
                // C#에선 불가능하다. unconstrained type은 System.Object로 가정.
                // System.Object에는 operator + 가 없기 때문. runtime instantiation을 하는 태생적 한계.
                // sum += item;

                sum = cal.Add(sum, item);
            }

            return sum;
        }
    }

    [TestClass]
    public class IListArrayTests
    {
        [TestMethod]
        public void AccumulateTest()
        {
            int[] arr = { 1, 2, 3, 4, 5 };
            List<int> list = new List<int>();
            list.AddRange(arr);

            Assert.AreEqual(
                ProcessItems.Accumulate(arr, new Calcurator()),
                ProcessItems.Accumulate(list, new Calcurator()),
                "array 내부 구현은 IList<>. 그래서 IList<> 패러매터에 인자로 그냥 넘길 수 있다.");
        }
    }
}

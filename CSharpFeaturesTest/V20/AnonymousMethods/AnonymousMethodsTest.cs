// 참고:
// Anonymous Methods (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/0yw3tz5k

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V20.AnonymousMethods
{
    [TestClass]
    public class AnonymousMethodsTest
    {
        delegate T Operator<T>(T lhs, T rhs);

        T ExecuteOperator<T>(Operator<T> op, T lhs, T rhs)
        {
            return op(lhs, rhs);
        }

        [TestMethod]
        public void BasicTest()
        {
            Operator<int> sum = delegate(int lhs, int rhs)
            {
                return lhs + rhs;
            };

            Assert.AreEqual(2 + 3, sum(2, 3));

            Assert.AreEqual(
                2 + 3,
                ExecuteOperator(
                    delegate(int lhs, int rhs) { return lhs + rhs; },
                    2, 3));
        }

        delegate int Del();

        [TestMethod]
        public void CapturedOrOuterVariablesTest()
        {
            int n = 0;

            // delegate 정의는 함수 내부에서 못한다.
            // delegate int Del();

            Del del = delegate() { return ++n; };

            Assert.AreEqual(1, del());
            Assert.AreEqual(1, n, "n은 delegate에서 capture. n을 delegate 내부에서 증가했기 때문에 n이 1이다");
            Assert.AreEqual(2, del());
            Assert.AreEqual(3, del());
            Assert.AreEqual(3, n);
        }
    }
}
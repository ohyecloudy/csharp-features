// 참고:
// Variance in Delegates (C# and Visual Basic) - msdn
// http://msdn.microsoft.com/en-us/library/dd233060

// out은 convariance
// in은 contravariance
// 그래서 out은 return 타입, in 은 input 타입으로만 사용 가능

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V40.GenericCovarianceAndContravariance
{
    [TestClass]
    public class VarianceInDelegatesTests
    {
        class BaseClass
        {
            public void Update() { }
        }

        class DerivedClass : BaseClass { }

        public delegate T ConvarianceDelegate<out T>();

        [TestMethod]
        public void ConvarianceTest()
        {
            ConvarianceDelegate<DerivedClass> d = () => new DerivedClass();

            // ConvarianceDelegate<DerivedClass> => ConvarianceDelegate<BaseClass>로 implicit conversion.
            // 반대로는 불가능.
            ConvarianceDelegate<BaseClass> b = d;
        }

        public delegate void ContravarianceDelegate<in T>(T input);

        [TestMethod]
        public void ContravarianceTest()
        {
            ContravarianceDelegate<BaseClass> b = input => input.Update();

            // ContravarianceDelegate<BaseClass> => ContravarianceDelegate<DerivedClass>로 implicit conversion.
            // 반대로는 불가능.
            ContravarianceDelegate<DerivedClass> d = b;
        }
    }
}

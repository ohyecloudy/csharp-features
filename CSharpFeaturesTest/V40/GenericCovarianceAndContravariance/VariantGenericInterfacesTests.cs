// 참고:
// Creating Variant Generic Interfaces (C# and Visual Basic) - msdn
// http://msdn.microsoft.com/en-us/library/dd997386

// out은 convariance
// in은 contravariance
// 그래서 out은 return 타입, in 은 input 타입으로만 사용 가능

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V40.GenericCovarianceAndContravariance
{
    [TestClass]
    public class VariantGenericInterfacesTests
    {
        class BaseClass { public int Id { get; set; } }
        class DerivedClass : BaseClass { }

        interface IContravariance<in T>
        {
            bool Equals(T lhs, T rhs);
        }

        class Contravariance : IContravariance<BaseClass>
        {
            public bool Equals(BaseClass lhs, BaseClass rhs)
            {
                return lhs.Id == rhs.Id;
            }
        }

        [TestMethod]
        public void ContravarianceTest()
        {
            IContravariance<BaseClass> b = new Contravariance();

            // IContravariance<BaseClass> => IContravariance<DerivedClass>로 implicit conversion.
            // 반대로는 불가능.
            IContravariance<DerivedClass> d = b;
        }

        interface IConvariance<out T>
        {
            T DefaultValue();
        }

        class Convariance : IConvariance<DerivedClass>
        {
            public DerivedClass DefaultValue()
            {
                return default(DerivedClass);
            }
        }

        [TestMethod]
        public void ConvarianceTest()
        {
            IConvariance<DerivedClass> d = new Convariance();

            // IConvariance<DerivedClass> => IConvariance<BaseClass>로 implicit conversion.
            // 반대로는 불가능.
            IConvariance<BaseClass> b = d;
        }
    }
}

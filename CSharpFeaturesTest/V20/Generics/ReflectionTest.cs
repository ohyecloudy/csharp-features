// 참고:
// Generics and Reflection - msdn
// http://msdn.microsoft.com/en-us/library/ms173128.aspx

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpFeaturesTest.V20.Generics
{
    public class Base<T, U> { }

    public class Derived<V> : Base<string, V>
    {
        public G<Derived<V>> F;

        public class Nested { }
    }

    public class G<T> { }

    public class Constraint<T, U, V> 
        where T : struct 
        where U : G<T>
        where V : class, new()
    { }

    public class Non { }

    [TestClass]
    public class ReflectionTest
    {
        [TestMethod]
        public void IsGenericTypeTest()
        {
            Assert.IsTrue(typeof(Base<,>).IsGenericType);
            Assert.IsTrue(typeof(Derived<>).IsGenericType);
            Assert.IsTrue(typeof(Derived<>).GetField("F").FieldType.IsGenericType);
            Assert.IsTrue(typeof(Derived<>.Nested).IsGenericType);
            Assert.IsTrue(typeof(G<>).IsGenericType);

            Assert.IsFalse(typeof(int).IsGenericType);
        }

        [TestMethod]
        public void GetGenericArgumentsTest()
        {
            Assert.AreEqual(2, typeof(Base<,>).GetGenericArguments().Length);
            Assert.AreEqual(2, typeof(Base<int, float>).GetGenericArguments().Length);
            Assert.AreEqual(1, typeof(Derived<>).GetGenericArguments().Length);
            Assert.AreEqual(0, typeof(Non).GetGenericArguments().Length);
        }

        [TestMethod]
        public void GetGenericTypeDefinitionTest()
        {
            Assert.IsTrue(typeof(Base<,>).IsGenericTypeDefinition);
            Assert.IsFalse(typeof(Base<int,int>).IsGenericTypeDefinition);
            Assert.IsTrue(typeof(Base<int, int>).GetGenericTypeDefinition().IsGenericTypeDefinition);
        }

        [TestMethod]
        public void GetGenericParameterConstraintsTest()
        {
            {
                Type[] args = typeof(Base<,>).GetGenericArguments();
                Assert.AreEqual(0, args[0].GetGenericParameterConstraints().Length);
                Assert.AreEqual(0, args[1].GetGenericParameterConstraints().Length);
            }

            {
                Type[] args = typeof(Constraint<,,>).GetGenericArguments();
                Assert.AreEqual(3, args.Length);
                Assert.AreEqual(
                    1, 
                    args[0].GetGenericParameterConstraints().Length,
                    "struct는 ValueType class를 상속받는다. 그래서 1개");
                Assert.AreEqual(1, args[1].GetGenericParameterConstraints().Length);
                Assert.AreEqual(
                    0, 
                    args[2].GetGenericParameterConstraints().Length,
                    "class, new 이렇게 constraint가 두개이지만 interface, base class constraint 개수만 리턴");
            }
        }

        /*
        사용할 때, 테스트하자. 하나하나 하니깐 지친다.
        ContainsGenericParameters
        GenericParameterAttributes
        GenericParameterPosition
        IsGenericParameter
        IsGenericTypeDefinition
        DeclaringMethod
        MakeGenericType

        IsGenericMethod
        GetGenericArguments
        GetGenericMethodDefinition
        ContainsGenericParameters
        IsGenericMethodDefinition
        MakeGenericMethod
        */
    }
}
// 참고:
// Generics and Reflection - msdn
// http://msdn.microsoft.com/en-us/library/ms173128.aspx

using Xunit;
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

    
    public class ReflectionTests
    {
        [Fact]
        public void IsGenericTypeTest()
        {
            Assert.True(typeof(Base<,>).IsGenericType);
            Assert.True(typeof(Derived<>).IsGenericType);
            Assert.True(typeof(Derived<>).GetField("F").FieldType.IsGenericType);
            Assert.True(typeof(Derived<>.Nested).IsGenericType);
            Assert.True(typeof(G<>).IsGenericType);

            Assert.False(typeof(int).IsGenericType);
        }

        [Fact]
        public void GetGenericArgumentsTest()
        {
            Assert.Equal(2, typeof(Base<,>).GetGenericArguments().Length);
            Assert.Equal(2, typeof(Base<int, float>).GetGenericArguments().Length);
            Assert.Single(typeof(Derived<>).GetGenericArguments());
            Assert.Empty(typeof(Non).GetGenericArguments());
        }

        [Fact]
        public void GetGenericTypeDefinitionTest()
        {
            Assert.True(typeof(Base<,>).IsGenericTypeDefinition);
            Assert.False(typeof(Base<int,int>).IsGenericTypeDefinition);
            Assert.True(typeof(Base<int, int>).GetGenericTypeDefinition().IsGenericTypeDefinition);
        }

        [Fact]
        public void GetGenericParameterConstraintsTest()
        {
            {
                Type[] args = typeof(Base<,>).GetGenericArguments();
                Assert.Empty(args[0].GetGenericParameterConstraints());
                Assert.Empty(args[1].GetGenericParameterConstraints());
            }

            {
                Type[] args = typeof(Constraint<,,>).GetGenericArguments();
                Assert.Equal(3, args.Length);
                Assert.True(
                    args[0].GetGenericParameterConstraints().Length == 1,
                    "struct는 ValueType class를 상속받는다. 그래서 1개");
                Assert.Single(args[1].GetGenericParameterConstraints());
                Assert.True(
                    args[2].GetGenericParameterConstraints().Length == 0,
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
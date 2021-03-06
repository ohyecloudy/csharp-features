﻿// 참고 : 
// Constraints on Type Parameters - msdn
// http://msdn.microsoft.com/en-us/library/d5x73970.aspx

using Xunit;
using System;
using System.Collections.Generic;

namespace CSharpFeaturesTest.V20.Generics
{
    
    public class ConstraintsTests
    {
        struct Dummy
        {
            public int v;
        }

        static void ProcessStruct<T>(ref T item) where T : struct
        {
            // item = null; struct는 value type이기 때문에 null을 할당할 수 없다.
            item = default(T);
        }

        [Fact]
        public void StructTest()
        {
            // Nullable에도 사용. value type만 type parameter로 쓰일 수 있다.
            // public struct Nullable<T> where T : struct
            Dummy d;
            d.v = 5;
            ProcessStruct(ref d);

            Assert.True(0 == d.v, "int의 default 값은 0");
        }

        static bool IsIdentityReferences<T>(T item1, T item2) where T : class
        {
            T item3;
            item3 = null; // reference type이기 때문에 null 할당 가능
            item3 = item1;

            return item1 == item2;
        }

        [Fact]
        public void ClassTest()
        {
            string s1 = "target";
            System.Text.StringBuilder sb = new System.Text.StringBuilder("target");
            string s2 = sb.ToString();

            Assert.False(
                IsIdentityReferences(s1, s2),
                "내용은 값지만 reference는 다르다. 그래서 false");
        }

        static T CreateInstance<T>() where T : class, new() // 같이 쓸때는 끝에 사용
        {
            return new T();
        }

        [Fact]
        public void NewTest()
        {
            Assert.True(CreateInstance<List<int>>() != CreateInstance<List<int>>());
        }

        static bool IsEqualUsingComparable<T>(T item1, T item2) where T : IComparable<T>
        {
            return (item1.CompareTo(item2) == 0);
        }

        [Fact]
        public void SpecifiedInterfaceTest()
        {
            string s1 = "target";
            System.Text.StringBuilder sb = new System.Text.StringBuilder("target");
            string s2 = sb.ToString();

            Assert.True(
                IsEqualUsingComparable(s1, s2),
                "TestClass에서 operator == 와 다른 결과. 여기서는 CompareTo() 함수를 호출");
        }

        class BaseClass
        {
            public virtual string Name()
            {
                return "Base";
            }
        }

        class ChildClass : BaseClass
        {
            public override string Name()
            {
                return "Child";
            }
        }

        static string GetName<T>(T obj) where T : BaseClass
        {
            return obj.Name();
        }

        [Fact]
        public void BaseClassTest()
        {
            Assert.Equal("Base", GetName(new BaseClass()));
            // "BaseClass를 상속받았기 때문에 where T : BaseClass constraint 조건을 만족"
            Assert.Equal("Child", GetName(new ChildClass()));
        }

        class CustomList<T>
        {
            // T를 constraint로 설정. U는 T이거나 T로부터 파생된 클래스여야 함.
            public bool Add<U>(CustomList<U> items) where U : T
            {
                return true;
            }
        }

        [Fact]
        public void TypeParameterAsConstraintTest()
        {
            CustomList<BaseClass> baseItems = new CustomList<BaseClass>();
            CustomList<ChildClass> childItems = new CustomList<ChildClass>();

            Assert.True(baseItems.Add(childItems));
        }
    }
}

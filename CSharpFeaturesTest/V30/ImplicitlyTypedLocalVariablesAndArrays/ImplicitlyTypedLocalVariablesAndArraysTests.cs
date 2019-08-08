// 참고:
// Implicitly Typed Local Variables (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb384061

using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.ImplicitlyTypedLocalVariablesAndArrays
{
    class Customer
    {
        public string City { get; set; }
    }

    
    public class ImplicitlyTypedLocalVariablesAndArraysTests
    {
        [Fact]
        public void ImplicitlyTypedLocalVariablesAndArraysTest()
        {
            var i = 5;
            Assert.Equal("System.Int32", i.GetType().FullName); // "int로 컴파일"

            var s = "ohyecloudy";
            Assert.Equal("System.String", s.GetType().FullName); // "string으로 컴파일"

            var ia = new[] { 0, 1, 2 };
            Assert.Equal("System.Int32[]", ia.GetType().FullName); // "int array로 컴파일"

            var istr = new[] { "ohyecloudy", "hello" };
            Assert.Equal("System.String[]", istr.GetType().FullName); // "string array로 컴파일"

            var expr =
                from c in (new List<Customer>())
                where c.City == "London"
                select c;
            Assert.Equal("WhereListIterator`1", expr.GetType().Name);

            var list = new List<int>();
            Assert.Equal("List`1", list.GetType().Name);
            Assert.True(list.GetType().IsGenericType);

            // null 초기화 X
            // var mem = null;

            // 초기화 표현에 사용 X
            // var i = (i = 20); X
            // int i = (i = 20); O

            // multiple declarator 사용 X
            // var i, j, k; X
        }

        class ClassDisposable : IDisposable
        {
            void IDisposable.Dispose()
            {
            }

            public bool IsAcceptable() { return true; }
        }

        [Fact]
        public void InContextsTest()
        {
            for (var x = 1; x < 10; x++)
            {
                Assert.Equal("System.Int32", x.GetType().FullName);
            }

            var list = new List<int>();
            list.Add(5);
            list.Add(4);

            foreach (var item in list)
            {
                Assert.Equal("System.Int32", item.GetType().FullName);
            }

            using (var i = new ClassDisposable())
            {
                Assert.True(i.IsAcceptable());
            }
        }
    }
}

// 참고:
// Using Type dynamic (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/dd264736

using Microsoft.CSharp.RuntimeBinder;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V40.DynamicSupport
{
    class ExampleClass
    {
        void ExistMethod(int input) { }
    }

    [TestClass]
    public class DynamicSupportTests
    {
        [TestMethod]
        [ExpectedException(typeof(RuntimeBinderException))]
        public void DynamicSupportTest()
        {
            dynamic ec = new ExampleClass();

            // dynamic은 type checking을 안 한다. 에러없이 컴파일. 다만 없다면 runtime 예외
            ec.NonexistentMethod();
        }
    }
}
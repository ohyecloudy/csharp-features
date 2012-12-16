// 참고:
// Partial Classes and Methods (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/wa80x488

// msdn에서 말하듯이 custom generated code에 유용할 것 같다.
// partial method를 정의하고 구현됐는지 여부에 상관없이 호출.
// -> 편해
// 지원을 안 한다면 껍데기만 만들던가, 구현했는지 여부를 확인하고 호출하는 절차를 거쳐야 한다.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V30.PartialMethodDefinitions
{
    // partial class 안에서만 정의 가능
    // PartialMethodImpl.cs 에 구현됐다.
    partial class PartialMethodImpl
    {
        // return type은 반드시 void
        // partial int MethodWithImpl();

        // out keyword는 사용 불가능.
        // ref와 차이는 넘기는 reference 초기화 여부인데, 
        // partial method는 구현이 없을 경우 컴파일러에서 제거하기 때문에 
        // 안정적인 동작을 syntax에서 최대한 보장하려는 것 같다.
        // 그래서 초기화한 변수를 넘길 수 있는 ref만 지원하는 듯.
        // partial void ReturnFiveWithImpl(out int outVal);

        partial void ReturnFive(ref int outVal);
        partial void ReturnDefaultValue<T>(ref T outVal);
        static partial void StaticReturnFive(ref int outVal);
        
        partial void ReturnTenWithoutImpl(ref int outVal);

        // partial 함수는 다 private이여. 그래서 virtual이 불가.
        public void CallReturnFive(ref int outVal) { ReturnFive(ref outVal); }
        public void CallReturnDefaultValue<T>(ref T outVal) { ReturnDefaultValue<T>(ref outVal); }
        public static void CallStaticReturnFive(ref int outVal) { StaticReturnFive(ref outVal); }
        public void CallReturnTenWithoutImpl(ref int outVal) { ReturnTenWithoutImpl(ref outVal); }
    }

    [TestClass]
    public class PartialMethodDefinitionsTests
    {
        [TestMethod]
        public void PartialMethodDefinitionsTest()
        {
            int val = 0;

            PartialMethodImpl pm = new PartialMethodImpl();
            pm.CallReturnFive(ref val);
            Assert.AreEqual(5, val);

            pm.CallReturnDefaultValue(ref val);
            Assert.AreEqual(default(int), val);

            PartialMethodImpl.CallStaticReturnFive(ref val);
            Assert.AreEqual(5, val);

            val = 1002;
            pm.CallReturnTenWithoutImpl(ref val);
            Assert.AreEqual(1002, val, "ReturnTenWithoutImpl() 이 함수는 구현부가 없다. 그래서 컴파일러가 제거. 1002 그대로 있다.");
        }
    }
}

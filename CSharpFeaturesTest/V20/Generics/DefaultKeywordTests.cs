// 참고:
// default Keyword in Generic Code - msdn
// http://msdn.microsoft.com/en-us/library/xwth0h0d.aspx

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpFeaturesTest.V20.Generics
{
    [TestClass]
    public class DefaultKeywordTests
    {
        struct DummyStruct
        {
            public int intMember;
            public float floatMember;
            public string referenceMember;
            public bool boolMember;

            public DummyStruct(int i, float f, string s, bool b)
            {
                intMember = i;
                floatMember = f;
                referenceMember = s;
                boolMember = b;
            }
        }

        static T GetDefaultValue<T>()
        {
            return default(T);
        }

        [TestMethod]
        public void DefaultKeywordTest()
        {
            Assert.AreEqual(false, GetDefaultValue<bool>());
            Assert.AreEqual(0, GetDefaultValue<int>());
            Assert.AreEqual(0.0f, GetDefaultValue<float>());
            Assert.AreEqual(null, GetDefaultValue<string>(), "reference type은 null");

            // struct인 경우 member들마다 default() 리턴값으로 할당
            DummyStruct s = GetDefaultValue<DummyStruct>();
            Assert.AreEqual(false, s.boolMember);
            Assert.AreEqual(0, s.intMember);
            Assert.AreEqual(0.0f, s.floatMember);
            Assert.AreEqual(null, s.referenceMember);

            // Nullable도 똑같은 규칙 적용
            Nullable<int> defaultNullable = GetDefaultValue<Nullable<int>>();
            Assert.AreEqual(false, defaultNullable.HasValue);
        }
    }
}
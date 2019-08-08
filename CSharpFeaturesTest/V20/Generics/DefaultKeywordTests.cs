// 참고:
// default Keyword in Generic Code - msdn
// http://msdn.microsoft.com/en-us/library/xwth0h0d.aspx

using Xunit;
using System;

namespace CSharpFeaturesTest.V20.Generics
{
    
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

        [Fact]
        public void DefaultKeywordTest()
        {
            Assert.False(GetDefaultValue<bool>());
            Assert.Equal(0, GetDefaultValue<int>());
            Assert.Equal(0.0f, GetDefaultValue<float>());
            Assert.True(null == GetDefaultValue<string>(), "reference type은 null");

            // struct인 경우 member들마다 default() 리턴값으로 할당
            DummyStruct s = GetDefaultValue<DummyStruct>();
            Assert.False(s.boolMember);
            Assert.Equal(0, s.intMember);
            Assert.Equal(0.0f, s.floatMember);
            Assert.Null(s.referenceMember);

            // Nullable도 똑같은 규칙 적용
            Nullable<int> defaultNullable = GetDefaultValue<Nullable<int>>();
            Assert.False(defaultNullable.HasValue);
        }
    }
}
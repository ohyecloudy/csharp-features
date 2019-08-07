// 참고:
// Access Modifiers (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/wxh6fsc7

using Xunit;
using System;
using System.Reflection;

namespace CSharpFeaturesTest.V1X.Modifiers
{
    // namespace에서 private access modifier는 사용 불가능.
    // private class ClassWithDefaultAccessModifier
    class ClassWithDefaultAccessModifier
    {
        class NestedClassWithDefaultAccessModifier
        {
        }

        void MethodWithDefaultAccessModifier()
        {
        }
#pragma warning disable 169
        // private이라 clas 내부에서 참조를 안해 워닝 발생. 
        // 밑에서 reflection으로 access modifier를 조사하기 위해 정의.
        int _fieldWithDefaultAccessModifier;
#pragma warning restore 169
    }

    
    public class DefaultAccessModifierTests
    {
        [Fact]
        public void DefaultModifierInNamespaceTest()
        {
            Type classType = typeof(ClassWithDefaultAccessModifier);

            Assert.False(
                classType.IsPublic,
                "public이 아니여야 한다.");
            Assert.False(
                classType.IsVisible,
                "internal이기 때문에 assembly 밖으로 노출하지 않아야 한다.");
        }

        [Fact]
        public void DefaultModifierNestedClassTest()
        {
            Type classType = typeof(ClassWithDefaultAccessModifier).
                GetNestedType("NestedClassWithDefaultAccessModifier", BindingFlags.NonPublic);

            Assert.True(
                classType.IsNestedPrivate,
                "nested class는 default access modifier가 private이여야 한다.");
        }

        [Fact]
        public void DefaultModifierMethodTest()
        {
            MethodInfo methodInfo = typeof(ClassWithDefaultAccessModifier).
                GetMethod(
                    "MethodWithDefaultAccessModifier",
                    BindingFlags.Instance | BindingFlags.NonPublic);

            Assert.True(
                methodInfo.IsPrivate,
                "method default access modifier는 private이여야 한다.");
        }

        [Fact]
        public void DefaultModifierFieldTest()
        {
            FieldInfo fieldInfo = typeof(ClassWithDefaultAccessModifier).
                GetField("_fieldWithDefaultAccessModifier", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.True(
                fieldInfo.IsPrivate,
                "field default access modifier는 private이여야 한다.");
        }
    }
}

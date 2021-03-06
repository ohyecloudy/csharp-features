﻿// 참고:
// :: Operator (C# Reference) - msdn
// http://msdn.microsoft.com/en-US/library/htccxtad

using Xunit;

// namespace alias 정의
using globalSystemAlias = global::System;

namespace CSharpFeaturesTest.V20.NamespaceAliasQualifier
{
    
    public class NamespaceAliasQualifierTests
    {
        public static class Math
        {
            public static int Abs(int input)
            {
                // 제대로 된 절대값 구하는 함수와 구분하기 위해 엉망으로 구현
                return input;
            }
        }

        [Fact]
        public void NamespaceAliasQualifierTest()
        {
            Assert.True(-5 == Math.Abs(-5), "local에 정의한 Math.Abs 때문에 System.Math.Abs가 가린다.");
            Assert.True(5 == global::System.Math.Abs(-5), "global:: 로 global namespace부터 이름 검색");
            
            // namespace alias는 :: 또는 . 로 접근 가능
            Assert.Equal(5, globalSystemAlias::Math.Abs(-5));
            Assert.Equal(5, globalSystemAlias.Math.Abs(-5));

            // global:: 로만 사용할 수 있다.
            //Assert.Equal(5, global.System.Math.Abs(-5));
        }
    }
}
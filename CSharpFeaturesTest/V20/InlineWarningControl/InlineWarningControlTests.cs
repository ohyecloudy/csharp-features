// 참고:
// #pragma warning (C# Reference) - msdn
// http://msdn.microsoft.com/en-US/library/441722ys

using Xunit;
using System;

namespace CSharpFeaturesTest.V20.InlineWarningControl
{
    
    public class InlineWarningControlTests
    {
        // 414, 3021 warning을 비활성화
#pragma warning disable 414, 3021
        [CLSCompliant(false)]
        public class C
        {
        }

        // restore로 3021 warning을 활성화. 주석을 제거하면 warning 발생
        // #pragma warning restore 3021
        [CLSCompliant(false)]  // CS3021 발생
        public class D
        {
        }
    }
}

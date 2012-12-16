// 참고:
// Fixed Size Buffers (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/zycewsya

// compile with /unsafe

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CSharpFeaturesTest.V20.FixedSizeBuffers
{
    [TestClass]
    public class FixedSizeBuffersTests
    {
        // unsafe block이나 modifier를 붙여줘야 함
        struct FixedSizeBuffer
        {
            public unsafe fixed int arr[30];
        }

        [TestMethod]
        public void FixedSizeBuffersTest()
        {
            FixedSizeBuffer buffer;
            for (int i = 0; i < 30; ++i)
            {
                // unsafe block 혹은 unsafe modifier가 붙은 method에서 접근할 수 있다.
                unsafe
                {
                    buffer.arr[i] = i;
                }
            }

            for (int i = 0; i < 30; ++i)
            {
                // unsafe block 혹은 unsafe modifier가 붙은 method에서 접근할 수 있다.
                unsafe
                {
                    Assert.AreEqual(i, buffer.arr[i]);
                }
            }
        }
    }
}

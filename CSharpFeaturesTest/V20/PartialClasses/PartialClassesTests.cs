// 참고:
// Partial Class Definitions (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/wa80x488

using Xunit;

namespace CSharpFeaturesTest.V20.PartialClasses
{
    
    public class PartialClassesTests
    {
        partial class Numbers
        {
            public int ReturnOne()
            {
                return 1;
            }

            public partial class Nested
            {
                public static int ReturnThree()
                {
                    return 3;
                }
            }
        }

        // 클래스 정의를 나눠서 할 수 있다.
        // C#에서 generation하는 코드와 사용자 코드를 분리할 수 있어서 유용.
        // (아마 이것때매 피처를 추가하지 않았을까 추측. 예. XAML code-behind)
        partial class Numbers
        {
            public int ReturnTwo()
            {
                return 2;
            }

            public partial class Nested
            {
                public static int ReturnFour()
                {
                    return 4;
                }
            }
        }

        [Fact]
        public void PartialClassesTest()
        {
            Numbers n = new Numbers();
            Assert.Equal(1, n.ReturnOne());
            Assert.Equal(2, n.ReturnTwo());
            Assert.Equal(3, Numbers.Nested.ReturnThree());
            Assert.Equal(4, Numbers.Nested.ReturnFour());
        }

        interface INumbers1
        {
            int ReturnOne();
        }

        interface INumbers2
        {
            int ReturnTwo();
        }

        partial class NumbersImpl : INumbers1
        {
            int INumbers1.ReturnOne()
            {
                return 1;
            }
        }

        partial class NumbersImpl : INumbers2
        {
            int INumbers2.ReturnTwo()
            {
                return 2;
            }
        }

        [Fact]
        public void PartialImplementationInterfaceTest()
        {
            NumbersImpl impl = new NumbersImpl();

            // partial class NumbersImpl : INumbers1
            // partial class NumbersImpl : INumbers2
            // ==>
            // class NumbersImpl : INumbers1, INumbers2
            INumbers1 n1 = (INumbers1)impl;
            Assert.Equal(1, n1.ReturnOne());

            INumbers2 n2 = (INumbers2)impl;
            Assert.Equal(2, n2.ReturnTwo());                       
        }
    }
}

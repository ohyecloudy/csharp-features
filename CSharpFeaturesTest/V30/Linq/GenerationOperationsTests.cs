// 참고:
// Generation Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546129

using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class GenerationOperationsTests
    {
        [Fact]
        public void DefaultIfEmptyMethodTest()
        {
            {
                List<int> nums = new List<int>();

                // "개수는 1개를 리턴하지만 계속 enumeration이 가능"
                Assert.Single(nums.DefaultIfEmpty());

                IEnumerator<int> etor = nums.DefaultIfEmpty().GetEnumerator();

                etor.MoveNext();
                Assert.Equal(default(int), etor.Current);

                etor.MoveNext();
                // "원소 하나만 리턴하는 게 아니라 계속 enumeration이 가능"
                Assert.Equal(default(int), etor.Current);
            }

            {
                List<IList<char>> list = new List<IList<char>>();

                IEnumerator<IList<char>> etor = list.DefaultIfEmpty().GetEnumerator();
                
                etor.MoveNext();
                Assert.Equal(default(IList<char>), etor.Current);

                etor.MoveNext();
                // "reference type default는 null"
                Assert.Null(
                    etor.Current);
            }
        }

        [Fact]
        public void EmptyMethodTest()
        {
            IEnumerable<string> em = Enumerable.Empty<string>();

            Assert.True(
                0 == em.Count(), 
                "비어있는 enumerable을 리턴. 혼자 쓰이지 않고 주로 Aggregate()같은 함수와 같이 쓰임");
        }

        [Fact]
        public void RangeMethodTest()
        {
            {
                IEnumerable<int> range = Enumerable.Range(5, 2);
                Assert.Equal(2, range.Count());

                IEnumerator<int> etor = range.GetEnumerator();
                etor.MoveNext();
                Assert.Equal(5, etor.Current);

                etor.MoveNext();
                Assert.Equal(6, etor.Current);

                Assert.False(etor.MoveNext());
            }

            {
                // 다른 method와 조합

                int[] result = 
                    Enumerable.Range(1, 4).Select(x => x * x).ToArray();

                int[] expected = { 1, 4, 9, 16 };

                Assert.Equal(expected, result);
            }
        }

        [Fact]
        public void RepeatMethodTest()
        {
            IEnumerator<string> etor =
                Enumerable.Repeat("programming", 3).GetEnumerator();

            etor.MoveNext();
            Assert.Equal("programming", etor.Current);

            etor.MoveNext();
            Assert.Equal("programming", etor.Current);

            etor.MoveNext();
            Assert.Equal("programming", etor.Current);

            Assert.False(etor.MoveNext());
        }
    }
}

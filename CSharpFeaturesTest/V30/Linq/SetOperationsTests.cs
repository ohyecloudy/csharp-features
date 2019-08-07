// 참고:
// Standard Query Operators Overview - msdn
// http://msdn.microsoft.com/en-us/library/bb397896
// Set Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546153

using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class SetOperationsTests
    {
        [Fact]
        public void DistinctMethodTest()
        {
            List<int> ages = new List<int>() { 21, 46, 21, 46 };

            IEnumerator<int> iter = ages.Distinct().GetEnumerator();
            iter.MoveNext();
            Assert.Equal(21, iter.Current);

            iter.MoveNext();
            Assert.Equal(46, iter.Current);
        }

        [Fact]
        public void ExceptMethodTest()
        {
            double[] nums1 = { 2.0, 2.1, 2.2 };
            double[] nums2 = { 2.2, 2.3 };

            IEnumerator<double> iter = nums1.Except(nums2).GetEnumerator();
            iter.MoveNext();
            Assert.Equal(2.0, iter.Current);

            iter.MoveNext();
            Assert.Equal(2.1, iter.Current);

            Assert.False(iter.MoveNext(), "nums1에서 nums2를 뺀 결과. 2.2는 nums2에 있기 때문에 제거");
        }

        [Fact]
        public void IntersectMethodTest()
        {
            int[] id1 = { 30, 44, 26, 92 };
            int[] id2 = { 26, 27, 0, 1, 30 };

            IEnumerator<int> iter = id1.Intersect(id2).GetEnumerator();
            iter.MoveNext();
            Assert.Equal(30, iter.Current);

            iter.MoveNext();
            Assert.Equal(26, iter.Current);

            Assert.False(iter.MoveNext());
        }

        [Fact]
        public void UnionMethodTest()
        {
            int[] id1 = { 30, 44, 26, 92 };
            int[] id2 = { 26, 27, 0, 1, 30 };

            List<int> union = id1.Union(id2).ToList();
            List<int> expected = new List<int>() { 30, 44, 26, 92, 27, 0, 1 };

            Assert.Equal(expected, union);
        }
    }
}

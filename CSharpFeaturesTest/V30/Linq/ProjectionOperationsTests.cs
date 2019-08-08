// 참고:
// Projection Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546168.aspx

using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class ProjectionOperationsTests
    {
        [Fact]
        public void SelectClauseTest()
        {
            List<string> words = new List<string>() { "an", "apple", "a", "day" };

            // 그냥 select word로 쓸 수도 있고 다른 연산을 할 수도 있다.
            var query =
                from word in words
                select word.Substring(0, 1);

            List<string> result = query.ToList();
            List<string> expected = new List<string>() { "a", "a", "a", "d" };

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SelectManyClauseTest()
        {
            // from을 여러개와 함께 select clause를 사용하면
            // SelectMany()가 호출

            List<string> phrases = new List<string>() { "an apple a day", "the quick brown fox" };

            var query =
                from phrase in phrases
                from word in phrase.Split(' ')
                select word;

            List<string> result = query.ToList();
            List<string> expected = new List<string>() 
            { 
                "an", "apple", "a", "day", "the", "quick", "brown", "fox"
            };

            Assert.Equal(expected, result);
        }
    }
}
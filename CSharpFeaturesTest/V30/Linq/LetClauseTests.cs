// 참고:
// Basic LINQ Query Operations (C#) - msdn
// http://msdn.microsoft.com/en-us/library/bb397927
// let clause (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/bb383976

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    [TestClass]
    public class LetClauseTests
    {
        [TestMethod]
        public void LetClauseTest()
        {
            string[] strings =
            {
                "A penny saved is a penny earned.",
                "The early bird catches the worm.",
                "The pen is mightier than the sword.",
            };

            // let으로 새로운 range 생성과 새로운 결과로 초기화 할 수 있다.
            var earlyBirdQuery =
                from sentence in strings
                let words = sentence.Split(' ')
                from word in words
                let w = word.ToLower()
                where w[0] == 'p'
                select word;

            Assert.AreEqual(3, earlyBirdQuery.Count(), "penny, penny, pen 이렇게 3개");
        }
    }
}

// 참고:
// enum (C# Reference) - msdn
// http://msdn.microsoft.com/en-us/library/sbbt4032
// Enum Class - msdn
// http://msdn.microsoft.com/en-us/library/system.enum

using Xunit;
using System;

namespace CSharpFeaturesTest.V1X.Types
{
    
    public class EnumTests
    {
        enum Days { Sat, Sun, Mon, Tue, Wed, Thu, Fri };

        [Fact]
        public void ZeroBasedIndexTest()
        {
            // "0부터 시작해야 한다"
            Assert.Equal(0, (int)Days.Sat);
        }

        [Fact]
        public void DefaultUnderlyingTypeTest()
        {
            // "명시하지 않으면 int가 underlying type이어야 한다"
            Assert.Equal(
                typeof(int),
                Enum.GetUnderlyingType(typeof(Days)));
        }

        enum Range : long { Max = 2147483648L, Min = 255L };

        [Fact]
        public void DefinedUnderlyingTypeTest()
        {
            // "enum : long으로 long을 underlying type으로 정의할 수 있어야 한다."
            Assert.Equal(
                typeof(long),
                Enum.GetUnderlyingType(typeof(Range)));
        }

        [Flags]
        enum CarOptions
        {
            SunRoof = 0x01,
            Spoiler = 0x02,
            FogLights = 0x04,
            TintedWindows = 0x08,
        }

        [Fact]
        public void EnumWithFlagsTest()
        {
            CarOptions options = CarOptions.SunRoof | CarOptions.FogLights;

            // ",를 구분자로 Flags를 출력해야 한다"
            Assert.Equal(
                "SunRoof, FogLights",
                options.ToString());

            Assert.Equal(
                (int)CarOptions.SunRoof + (int)CarOptions.FogLights,
                (int)options);
        }

        enum ArrivalStatus { Unknown = -3, Late = -1, OnTime = 0, Early = 1 };

        [Fact]
        public void IsDefinedTest()
        {
            // "-2 값은 정의되지 않았기 때문에 false를 리턴해야 한다"
            Assert.False(Enum.IsDefined(typeof(ArrivalStatus), -2));

            Assert.True(Enum.IsDefined(typeof(ArrivalStatus), -3));

            // "underlying type이 int이기 때문에 정의 안한 값으로 할당해도 문제가 없어야 한다"
            ArrivalStatus status = (ArrivalStatus)(-4);
            Assert.Equal("-4", status.ToString());

            // 그래서 안전하게 값을 받아서 enum type으로 변경을 할때에는 IsDefined() 사용
            if (!Enum.IsDefined(typeof(ArrivalStatus), status))
            {
                status = ArrivalStatus.Unknown;
            }
            Assert.Equal(ArrivalStatus.Unknown, status);
        }

        [Fact]
        public void ParseTest()
        {
            //  "Parse()는 문자열을 enum으로 바꿀 수 있어야 한다"
            Assert.Equal(
                "Late",
                Enum.Parse(typeof(ArrivalStatus), "-1").ToString());
        }

        [Fact]
        public void ParseExceptionTest()
        {
            Assert.Throws<ArgumentException>(() => Enum.Parse(typeof(ArrivalStatus), "wrongnumber"));
        }

        [Fact]
        public void TryParseTest()
        {
            ArrivalStatus status;
            Assert.False(Enum.TryParse<ArrivalStatus>("wrongnumber", out status));
        }

        [Fact]
        public void ToStringFormattingTest()
        {
            ArrivalStatus status = ArrivalStatus.Late;

            Assert.Equal("Late", status.ToString("G"));
            Assert.Equal("Late", status.ToString("F"));
            Assert.Equal("-1", status.ToString("D"));
            Assert.Equal("FFFFFFFF", status.ToString("X"));
        }

        [Fact]
        public void GetNamesTest()
        {
            string[] names = Enum.GetNames(typeof(ArrivalStatus));

            // 첫번째가 Unknown이 아니다. 즉 순회 순서를 믿어서는 안된다. 
            // 0부터 시작 +로 다 순회하고 - 순회. 뭐 이런 규칙이 보이지만 걍 순서 믿지 말자
            Assert.Equal("OnTime", names[0]);
            Assert.Equal("Late", names[3]);
        }

        [Fact]
        public void GetValuesTest()
        {
            Array values = Enum.GetValues(typeof(ArrivalStatus));
            Assert.True(values.GetEnumerator() != null, "foreach container, linq를 사용할 수 있어야 한다");

            // GetNames()와 마찬가지
            Assert.Equal(0, (int)values.GetValue(0));
        }
    }
}

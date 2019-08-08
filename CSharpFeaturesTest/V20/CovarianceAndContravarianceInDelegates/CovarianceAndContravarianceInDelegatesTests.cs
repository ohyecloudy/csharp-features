// 참고:
// Using Variance in Delegates (C# and Visual Basic) - msdn
// http://msdn.microsoft.com/en-US/library/ms173174

using Xunit;

namespace CSharpFeaturesTest.V20.CovarianceAndContravarianceInDelegates
{
    class Mammals
    {
        public virtual string Name
        {
            get
            {
                return "mammals";
            }
        }
    }

    class Dogs : Mammals
    {
        public override string Name
        {
            get
            {
                return "dogs";
            }
        }

        public bool Bark()
        {
            return true;
        }
    }

    
    public class CovarianceAndContravarianceInDelegatesTests
    {
        delegate Mammals NewMammals();
        
        [Fact]
        public void CovarianceTest()
        {
            NewMammals newMammals = delegate()
            {
                return new Mammals();
            };

            // Covariance 허용.
            // dogs -> mammals
            NewMammals newDogs = delegate()
            {
                return new Dogs();
            };

            Assert.Equal("mammals", newMammals().Name);
            Assert.Equal("dogs", newDogs().Name);
        }

        delegate bool Bark(Dogs dogs);

        static bool CantBark(Mammals m)
        {
            return false;
        }

        [Fact]
        public void ContravarianceTest()
        {
            // Contravariance일 때, 이렇게 바로 anonymous method로 delegate instance 생성은 불가능
            // Bark cantBark = delegate(Mammals m)
            // {
            //     return false;
            // };

            // Contravariance일 때, named method는 가능
            Bark cantBark = CantBark;

            Bark bark = delegate(Dogs d)
            {
                return d.Bark();
            };

            // Assert.False(cantBark(new Mammals()));
            // 불가능. delegate 정의에서 패러매터가 Dogs이기 때문
            // contravariance를 지원해서 parent 객체에 derived 객체에만 있는 method를 호출하는 위험이 있지 않을까 했는데,
            // delegate 인스턴스 생성에서만 contravariance가 적용되고 delegate 정의에 따라 인자가 넘어오기 때문에
            // delegate 호출 시 결국 covariance 형태가 되서 안전하다.
            // 예) delegate 인스턴스 생성시 Mammals -> Dogs. 호출 시 Dogs -> Mammals
           
            Assert.False(cantBark(new Dogs()));
            Assert.True(bark(new Dogs()));
        }
    }
}

// 참고
// Auto-Implemented Properties (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/bb384054

using Xunit;

namespace CSharpFeaturesTest.V30.AutoImplementedProperties
{
    
    public class AutoImplementedPropertiesTests
    {
        class Customer
        {
            // 이렇게만 해주면 컴파일러가 자동으로 구현해준다.
            public int CustomerID { get; set; }

            // 구식 방법
            private int manualProperty;
            public int ManualProperty
            {
                get { return manualProperty; }
                set { manualProperty = value; }
            }
        }

        [Fact]
        public void AutoImplementedPropertiesTest()
        {
            Customer c = new Customer();
            
            c.CustomerID = 10;
            Assert.Equal(10, c.CustomerID);

            c.ManualProperty = 5;
            Assert.Equal(5, c.ManualProperty);
        }
    }
}

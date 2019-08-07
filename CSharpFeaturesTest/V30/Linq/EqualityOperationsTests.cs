// 참고:
// Equality Operations - msdn
// http://msdn.microsoft.com/en-us/library/bb546160

using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharpFeaturesTest.V30.Linq
{
    
    public class EqualityOperationsTests
    {
        class Pet
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        struct SPet
        {
            public string Name { get; set; }
            public int Age { get; set; }
        }

        [Fact]
        public void SequenceEqualMethodTest()
        {
            {
                Pet pet1 = new Pet { Name = "Turbo", Age = 2 };
                Pet pet2 = new Pet { Name = "Peanut", Age = 8 };

                List<Pet> pets1 = new List<Pet> { pet1, pet2 };
                List<Pet> pets2 = new List<Pet> { pet1, pet2 };

                Assert.True(pets1.SequenceEqual(pets2));

                List<Pet> pets3 = new List<Pet>
                {
                    new Pet { Name = "Turbo", Age = 2 },
                    new Pet { Name = "Peanut", Age = 8 },
                };

                Assert.False(
                    pets1.SequenceEqual(pets3),
                    "identical data이지만 다른 reference이기 때문에 false를 리턴.");
            }

            {
                List<SPet> pets1 = new List<SPet> 
                { 
                    new SPet { Name = "Turbo", Age = 2 },
                    new SPet { Name = "Peanut", Age = 8 },
                };

                List<SPet> pets2 = new List<SPet> 
                { 
                    new SPet { Name = "Turbo", Age = 2 },
                    new SPet { Name = "Peanut", Age = 8 },
                };

                Assert.True(
                    pets1.SequenceEqual(pets2),
                    "ValueType인 struct는 실제 data를 비교한다. 그래서 true");
            }
        }

        class Product : IEquatable<Product>
        {
            public string Name { get; set; }
            public int Code { get; set; }

            bool IEquatable<Product>.Equals(Product other)
            {
                if (Object.ReferenceEquals(other, null)) return false;

                if (Object.ReferenceEquals(this, other)) return true;

                return Code.Equals(other.Code) && Name.Equals(other.Name);
            }
        }

        [Fact]
        public void SequenceEqualMethodWithIEquatableTest()
        {
            Product[] storeA = 
            {
                new Product { Name = "apple", Code = 9 },
                new Product { Name = "orange", Code = 4 },
            };

            Product[] storeB = 
            {
                new Product { Name = "apple", Code = 9 },
                new Product { Name = "orange", Code = 4 },
            };

            Assert.True(
                storeA.SequenceEqual(storeB),
                "IEquatable 인터페이스를 구현해서 data를 비교하게 했다.");
        }
    }
}

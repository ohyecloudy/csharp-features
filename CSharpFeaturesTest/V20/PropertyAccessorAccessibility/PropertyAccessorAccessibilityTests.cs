// 참고:
// Restricting Accessor Accessibility (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/75e8y5dd

using Xunit;

namespace CSharpFeaturesTest.V20.PropertyAccessorAccessibility
{
    
    public class PropertyAccessorAccessibilityTests
    {
        class Parent
        {
            private string name = "parent";

            public virtual string Name
            {
                get
                {
                    return name;
                }

                // get과 set을 다른 accessibility level로 정의할 수 있다
                protected set
                {
                    name = value;
                }
            }
        }

        class Child : Parent
        {
            public Child()
            {
                Name = "child";
            }

            public override string Name
            {
                // override를 한 경우 같은 accessibility level을 변경할 수 없다.
                //private get
                get
                {
                    return base.Name;
                }
                protected set
                {
                    base.Name = value;
                }
            }
        }

        [Fact]
        public void PropertyAccessorAccessibilityTest()
        {
            Parent parent = new Parent();
            Assert.Equal("parent", parent.Name);

            Child child = new Child();
            Assert.Equal("child", child.Name);
        }

        interface IPerson
        {
            string Name
            {
                // interface는 무조건 public. access modifier를 사용할 수 없다.
                // public get;
                get;
            }
        }

        class Person : IPerson
        {
            private string name;

            public string Name
            {
                get
                {
                    return name;
                }

                // interface에서 get만 선언했기 때문에 set access modifier는 마음대로 가능
                protected set
                {
                    name = value;
                }
            }
        }

        class DerivedPerson : Person
        {
            public DerivedPerson()
            {
                Name = "derived";
            }
        }

        [Fact]
        public void InterfacePropertyTest()
        {
            IPerson person = new DerivedPerson();
            Assert.Equal("derived", person.Name);
        }
    }
}

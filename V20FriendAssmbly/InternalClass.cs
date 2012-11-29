// 참고:
// Friend Assemblies (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-US/library/0tke9fxk

using System.Runtime.CompilerServices;

// CSharpFeatures assembly와 친구 먹기. 
// internal access 권한을 CSharpFeatures assembly도 가진다.
[assembly: InternalsVisibleTo("CSharpFeaturesTest")]
namespace v20FriendAssembly
{
    // access modifier default는 internal
    // internal은 같은 assembly에서만 접근할 수 있다.
    class InternalClass
    {
        public int PublicProperty
        {
            get
            {
                return 5;
            }
        }
    }

    public class PublicClass
    {
        internal static int InternalStaticField = 15;
    }
}

// 참고:
// Partial Classes and Methods (C# Programming Guide) - msdn
// http://msdn.microsoft.com/en-us/library/wa80x488

namespace CSharpFeaturesTest.V30.PartialMethodDefinitions
{
    partial class PartialMethodImpl
    {
        partial void ReturnFive(ref int outVal)
        {
            outVal = 5;
        }

        partial void ReturnDefaultValue<T>(ref T outVal)
        {
            outVal = default(T);
        }

        static partial void StaticReturnFive(ref int outVal)
        {
            outVal = 5;
        }
    }
}

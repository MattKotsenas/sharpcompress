using System;

namespace Lagan.Core
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class OwnedAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
    public class BorrowedAttribute : Attribute
    {
    }
}

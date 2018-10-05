using System.Linq;
using Lagan.Core;
using Microsoft.CodeAnalysis;

namespace Lagan.Analyzers
{
    public static class ITypeSymbolExtensions
    {
        private static readonly string OwnedAttributeNamespace = typeof(OwnedAttribute).Namespace;
        private static readonly string OwnedAttributeName = nameof(OwnedAttribute);

        private static readonly string BorrowedAttributeNamespace = typeof(BorrowedAttribute).Namespace;
        private static readonly string BorrowedAttributeName = nameof(BorrowedAttribute);

        public static bool ImplementsIDisposable(this ITypeSymbol symbol)
        {
            return symbol.AllInterfaces.Any(i => i.ContainingNamespace.Name == "System" && i.Name == "IDisposable");
        }

        public static bool IsOwnedAttribute(this ITypeSymbol symbol)
        {
            return symbol.ContainingNamespace.ToDisplayString() == OwnedAttributeNamespace && symbol.Name == OwnedAttributeName;
        }

        public static bool IsBorrowedAttribute(this ITypeSymbol symbol)
        {
            return symbol.ContainingNamespace.ToDisplayString() == BorrowedAttributeNamespace && symbol.Name == BorrowedAttributeName;
        }
    }
}
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Prism.Generators.PageConstants
{
    internal sealed class SyntaxReceiver : ISyntaxReceiver
    {
        public List<TypeDeclarationSyntax> Candidates { get; } = new List<TypeDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is TypeDeclarationSyntax typeDeclarationSyntax)
            {
                foreach (AttributeListSyntax attributeList in typeDeclarationSyntax.AttributeLists)
                {
                    foreach (AttributeSyntax attribute in attributeList.Attributes)
                    {
                        if (attribute.Name.ToString() == "PageConstant" ||
                            attribute.Name.ToString() == "PageConstantAttribute")
                        {
                            Candidates.Add(typeDeclarationSyntax);
                        }
                    }
                }
            }
        }
    }
}

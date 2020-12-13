using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Text;

namespace Prism.Generators.PageConstants
{
    [Generator]
    internal sealed class PageConstantsGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }

        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver receiver)
                return;

            Compilation compilation = context.Compilation;
            INamedTypeSymbol attributeSymbol = compilation.GetTypeByMetadataName(typeof(PageConstantAttribute).FullName);

            StringWriter writer = new StringWriter();
            IndentedTextWriter indentWriter = new IndentedTextWriter(writer, new string(' ', 4));

            // Add namespace:
            indentWriter.WriteLine("namespace Prism.Generated");
            indentWriter.WriteLine("{");
            indentWriter.Indent++;

            // Add class declaration:
            indentWriter.WriteLine("public static class PageConstants");
            indentWriter.WriteLine("{");
            indentWriter.Indent++;

            // Collect property strings:
            SortedSet<string> properties = new SortedSet<string>();

            foreach (TypeDeclarationSyntax candidateTypeNode in receiver.Candidates)
            {
                SemanticModel model = compilation.GetSemanticModel(candidateTypeNode.SyntaxTree);
                ITypeSymbol candidateTypeSymbol = model.GetDeclaredSymbol(candidateTypeNode);

                if (candidateTypeSymbol is not null)
                {
                    ImmutableArray<AttributeData> attributes = candidateTypeSymbol.GetAttributes();

                    AttributeData attributeData = attributes
                        .FirstOrDefault(x => x.AttributeClass.Equals(attributeSymbol, SymbolEqualityComparer.Default));

                    TypedConstant nameProperty = attributeData.NamedArguments
                        .FirstOrDefault(kvp => kvp.Key == $"{nameof(PageConstantAttribute.Name)}").Value;

                    // Use either the type name or the attribute property value
                    string value = candidateTypeSymbol.Name;
                    if (!nameProperty.IsNull)
                        value = nameProperty.Value.ToString();

                    properties.Add($"public const string {candidateTypeSymbol.Name} = \"{value}\";");
                }
            }

            // Add properties:
            foreach (string property in properties)
            {
                indentWriter.WriteLine(property);
            }

            // Add closing curly braces
            indentWriter.Indent--;
            indentWriter.WriteLine("}");

            indentWriter.Indent--;
            indentWriter.WriteLine("}");

            // Add the source:
            context.AddSource("PageConstants.g.cs", SourceText.From(writer.ToString(), Encoding.UTF8));
        }
    }
}

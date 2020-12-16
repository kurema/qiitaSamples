using System;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;
using System.Text;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Collections.Generic;

namespace kurema.TernaryComparisonOperator.OperatorOverloadingAttacher
{
    [Generator]
    public class OperatorOverloadingAttacher : ISourceGenerator
    {
        private const string attributeSource = @"using System;

namespace kurema.TernaryComparisonOperator.OperatorOverloadingAttacher
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class OperatorOverloadingAttachTargetAttribute : Attribute { }
}
";
        private readonly string[] Operators = new[] { "==", "!=", "<", ">", "<=", ">=" };

        public void Execute(GeneratorExecutionContext context)
        {
            context.AddSource("OperatorOverloadingAttachement.cs", SourceText.From(attributeSource, Encoding.UTF8));
            if (context.SyntaxReceiver is not SyntaxReceiver receiver) return;
            var options = (context.Compilation as CSharpCompilation)?.SyntaxTrees[0].Options as CSharpParseOptions;
            if (options is null) return;
            var compilation = context.Compilation.AddSyntaxTrees(CSharpSyntaxTree.ParseText(SourceText.From(attributeSource, Encoding.UTF8), options));
            var attributeSymbol = compilation.GetTypeByMetadataName("kurema.TernaryComparisonOperator.OperatorOverloadingAttacher.OperatorOverloadingAttachTargetAttribute");

            var codeText = new kurema.StringBuilderProvider.TextChainAutoIndent();
            codeText += $"#nullable enable";
            foreach (var candidate in receiver.CandidateClasses)
            {
                var model = compilation.GetSemanticModel(candidate.SyntaxTree);
                var typeSymbol = ModelExtensions.GetDeclaredSymbol(model, candidate);
                if (typeSymbol == null) continue;
                var attribute = typeSymbol.GetAttributes().FirstOrDefault(ad => ad.AttributeClass?.Equals(attributeSymbol, SymbolEqualityComparer.Default) == true);
                if (attribute is null) continue;
                var namespaceName = typeSymbol.ContainingNamespace.ToDisplayString();
                var className = typeSymbol.Name;

                if (!typeSymbol.ContainingNamespace.IsGlobalNamespace)
                {
                    codeText += $"namespace {namespaceName}";
                    codeText += $"{{";
                    codeText.Indent();
                }
                codeText += $"public partial class {className}";
                codeText += $"{{";
                codeText.Indent();
                foreach(var @operator in Operators)
                {
                    codeText += $"public static {className} operator {@operator}({className} left, double right) => new {className}(left.Status && left.ValueRight {@operator} right, left.ValueLeft, right);";
                    codeText += $"public static {className} operator {@operator}(double left, {className} right) => new {className}(right.Status && left {@operator} right.ValueLeft, left, right.ValueRight);";
                }
                codeText.Unindent();
                codeText += $"}}";
                if (!typeSymbol.ContainingNamespace.IsGlobalNamespace)
                {
                    codeText.Unindent();
                    codeText += $"}}";
                }
            }
            context.AddSource("OperatorOverloadingAttachement_partial.cs", SourceText.From(codeText.ToString(), Encoding.UTF8));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
        }
    }

    internal class SyntaxReceiver : ISyntaxReceiver
    {
        internal List<ClassDeclarationSyntax> CandidateClasses { get; } = new();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax @class) CandidateClasses.Add(@class);
        }
    }
}

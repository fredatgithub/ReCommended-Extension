using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;

namespace ReCommendedExtension.ContextActions;

[ContextAction(
    Group = "C#",
    Name = "Annotate method with [LinqTunnel] attribute" + ZoneMarker.Suffix,
    Description = "Annotates a method with the [LinqTunnel] attribute.")]
public sealed class AnnotateWithLinqTunnel(ICSharpContextActionDataProvider provider) : AnnotateWithCodeAnnotation(provider)
{
    protected override string AnnotationAttributeTypeName => nameof(LinqTunnelAttribute);

    protected override bool CanBeAnnotated(IDeclaredElement? declaredElement, ITreeNode context)
        => declaredElement is IMethod method
            && (method.ReturnType.IsGenericIEnumerable() && method.Parameters.Any(p => p.Type.IsGenericIEnumerable())
                || method.ReturnType.IsIAsyncEnumerable() && method.Parameters.Any(p => p.Type.IsIAsyncEnumerable()));
}
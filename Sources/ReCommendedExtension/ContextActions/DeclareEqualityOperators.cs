﻿using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.TextControl;
using JetBrains.Util;
using ReCommendedExtension.Analyzers.InterfaceImplementation;

namespace ReCommendedExtension.ContextActions;

[ContextAction(
    Group = "C#",
    Name = "Declare IEqualityOperators<T, T, bool>" + ZoneMarker.Suffix,
    Description = "Declare IEqualityOperators<T, T, bool>.")]
public sealed class DeclareEqualityOperators(ICSharpContextActionDataProvider provider) : ContextActionBase
{
    IClassLikeDeclaration? declaration;
    ITypeElement? equalityOperatorsInterface;

    [MemberNotNullWhen(true, nameof(declaration))]
    [MemberNotNullWhen(true, nameof(equalityOperatorsInterface))]
    public override bool IsAvailable(IUserDataHolder cache)
    {
        if (provider.GetSelectedElement<IClassLikeDeclaration>(true, false) is { } declaration
            && declaration.GetCSharpLanguageLevel() >= CSharpLanguageLevel.CSharp110
            && ClrTypeNames.IEqualityOperators.TryGetTypeElement(declaration.GetPsiModule()) is { } equalityOperatorsInterface
            && declaration.DeclaredElement is { })
        {
            var type = TypeFactory.CreateType(declaration.DeclaredElement);

            var (declaresEquatable, declaresEqualityOperators, _, _) =
                InterfaceImplementationAnalyzer.GetInterfaces(declaration, type, equalityOperatorsInterface, null, null);

            this.declaration = declaration switch
            {
                IClassDeclaration or IStructDeclaration when declaresEquatable && !declaresEqualityOperators => declaration,

                IRecordDeclaration when !declaresEqualityOperators => declaration,

                _ => null,
            };

            this.equalityOperatorsInterface = equalityOperatorsInterface;
        }
        else
        {
            this.declaration = null;
            this.equalityOperatorsInterface = null;
        }

        return this.declaration is { };
    }

    public override string Text
    {
        get
        {
            Debug.Assert(declaration is { });

            return $"Declare IEqualityOperators<{declaration.DeclaredName}, {declaration.DeclaredName}, bool> interface.";
        }
    }

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        try
        {
            Debug.Assert(declaration is { DeclaredElement: { } });
            Debug.Assert(equalityOperatorsInterface is { });

            var psiModule = declaration.GetPsiModule();

            var type = TypeFactory.CreateType(declaration.DeclaredElement);

            declaration.AddSuperInterface(
                TypeFactory.CreateType(equalityOperatorsInterface, new[] { type, type, PredefinedType.BOOLEAN_FQN.GetType(psiModule) }),
                false);

            return _ => { };
        }
        finally
        {
            declaration = null;
            equalityOperatorsInterface = null;
        }
    }
}
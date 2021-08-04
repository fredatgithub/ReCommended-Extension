﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Util;

namespace ReCommendedExtension.ContextActions.CodeContracts.Internal
{
    internal sealed class MethodContractInfo : ContractInfo
    {
        [CanBeNull]
        public static MethodContractInfo TryCreate(
            [NotNull] IMethodDeclaration declaration,
            TreeTextRange selectedTreeRange,
            [NotNull] Func<IType, bool> isAvailableForType)
        {
            if (declaration.GetNameRange().Contains(selectedTreeRange) && declaration.ArrowClause == null)
            {
                var method = declaration.DeclaredElement;

                if (method != null && CanAcceptContracts(method) && !method.ReturnType.IsVoid() && isAvailableForType(method.ReturnType))
                {
                    return new MethodContractInfo(declaration, method.ReturnType);
                }
            }

            return null;
        }

        [NotNull]
        readonly IMethodDeclaration declaration;

        MethodContractInfo([NotNull] IMethodDeclaration declaration, [NotNull] IType type) : base(ContractKind.Ensures, type)
            => this.declaration = declaration;

        public override string GetContractIdentifierForUI() => "result";

        public override void AddContracts(
            ICSharpContextActionDataProvider provider,
            Func<IExpression, IExpression> getContractExpression,
            out ICollection<ICSharpStatement> firstNonContractStatements)
        {
            var factory = CSharpElementFactory.GetInstance(declaration);

            IBlock body;

            var containingTypeDeclaration = declaration.GetContainingTypeDeclaration();
            Debug.Assert(containingTypeDeclaration != null);

            if (declaration.IsAbstract || containingTypeDeclaration.IsAbstract)
            {
                var contractClassDeclaration = containingTypeDeclaration.EnsureContractClass(provider.PsiModule);

                var overriddenMethodDeclaration = declaration.EnsureOverriddenMethodInContractClass(contractClassDeclaration);

                body = overriddenMethodDeclaration.Body;
            }
            else
            {
                body = declaration.Body;
            }

            if (body != null)
            {
                var contractType = TypeElementUtil.GetTypeElementByClrName(PredefinedType.CONTRACT_FQN, provider.PsiModule);

                var declaredElement = declaration.DeclaredElement;

                Debug.Assert(declaredElement != null);

                var expression = factory.CreateExpression($"$0.{nameof(Contract.Result)}<$1>()", contractType, declaredElement.ReturnType);

                AddContract(
                    ContractKind.Ensures,
                    body,
                    provider.PsiModule,
                    () => getContractExpression(expression),
                    out var firstNonContractStatement);

                firstNonContractStatements = firstNonContractStatement != null ? new[] { firstNonContractStatement } : null;
            }
            else
            {
                firstNonContractStatements = null;
            }
        }
    }
}
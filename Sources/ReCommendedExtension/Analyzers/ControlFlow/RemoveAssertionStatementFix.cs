﻿using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.ExtensionsAPI.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;

namespace ReCommendedExtension.Analyzers.ControlFlow;

[QuickFix]
public sealed class RemoveAssertionStatementFix(RedundantAssertionStatementSuggestion highlighting) : QuickFixBase
{
    public override bool IsAvailable(JetBrains.Util.IUserDataHolder cache) => true;

    public override string Text => "Remove assertion";

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        using (WriteLockCookie.Create())
        {
            var statement = ((AssertionStatement)highlighting.Assertion).Statement;

            var nextToken = statement.NextTokens().SkipWhile(token => token.IsWhitespaceToken()).FirstOrDefault();

            ModificationUtil.DeleteChildRange(
                statement,
                nextToken is { } && nextToken.GetTokenType() == CSharpTokenType.SEMICOLON ? nextToken : statement);
        }

        return _ => { };
    }
}
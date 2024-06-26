﻿using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.Util;

namespace ReCommendedExtension.Analyzers.LocalSuppression;

[RegisterConfigurableSeverity(
    SeverityId,
    null,
    HighlightingGroupIds.BestPractice,
    "Avoid local suppression" + ZoneMarker.Suffix,
    "",
    Severity.WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public sealed class LocalSuppressionWarning(
    string message,
    ICommentNode comment,
    [NonNegativeValue] int leadingWhitespaceCharacterCount,
    bool justOnce) : Highlighting(message)
{
    const string SeverityId = "LocalSuppression";

    public override DocumentRange CalculateRange()
    {
        var startOffset = comment.GetCommentRange().StartOffset.Offset + leadingWhitespaceCharacterCount;

        var endOffset = justOnce ? startOffset + "ReSharper disable once".Length : startOffset + "ReSharper disable".Length;

        return new DocumentRange(comment.GetDocumentRange().Document, new TextRange(startOffset, endOffset));
    }
}
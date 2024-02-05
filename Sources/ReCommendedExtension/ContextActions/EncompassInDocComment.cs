﻿using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;
using JetBrains.Util;

namespace ReCommendedExtension.ContextActions;

public abstract class EncompassInDocComment(ICSharpContextActionDataProvider provider) : ContextActionBase
{
    [Pure]
    static (int start, int end) GetWordBoundaries(string text, [NonNegativeValue] int position)
    {
        Debug.Assert(text != "");
        Debug.Assert(position < text.Length);

        var start = position;
        while (start > 0)
        {
            start--;

            if (char.IsLetterOrDigit(text[start]))
            {
                continue;
            }

            start++;
            break;
        }

        var end = position;
        while (end < text.Length && char.IsLetterOrDigit(text[end]))
        {
            end++;
        }

        return (start, end);
    }

    IDocCommentNode? docCommentNode;
    int start;
    int end;

    [Pure]
    protected abstract string Encompass(string text);

    [MemberNotNullWhen(true, nameof(docCommentNode))]
    public sealed override bool IsAvailable(IUserDataHolder cache)
    {
        docCommentNode = provider.GetSelectedElement<IDocCommentNode>();
        if (docCommentNode is not { })
        {
            return false;
        }

        var text = docCommentNode.GetText(); // includes the leading "///" or "/**" characters
        Debug.Assert(text != "");

        var startOffset = docCommentNode.GetDocumentStartOffset();

        var documentSelection = provider.DocumentSelection;

        if (documentSelection.IsEmpty)
        {
            var position = documentSelection.StartOffset - startOffset; // relative to the text

            (start, end) = GetWordBoundaries(text, position);

            if (start == end)
            {
                docCommentNode = null;
            }
        }
        else
        {
            (start, end) = (documentSelection.StartOffset - startOffset, documentSelection.EndOffset - startOffset);

            if (start < 0 || end < 0)
            {
                docCommentNode = null; // selection could be started before the text
            }
        }

        return docCommentNode is { }; // for performance reasons: we don't check if the result becomes a valid XML
    }

    protected sealed override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        try
        {
            Debug.Assert(docCommentNode is { });

            var text = docCommentNode.GetText();

            Debug.Assert(start >= 0 && start < text.Length);
            Debug.Assert(end >= 0 && end < text.Length);
            Debug.Assert(start < end);

            var factory = CSharpElementFactory.GetInstance(docCommentNode);

            var updatedDocCommentNode = factory.CreateComment($"{text[..start]}{Encompass(text[start..end])}{text[end..]}");

            docCommentNode.ReplaceBy(updatedDocCommentNode);

            return _ => { };
        }
        finally
        {
            docCommentNode = null;
        }
    }
}
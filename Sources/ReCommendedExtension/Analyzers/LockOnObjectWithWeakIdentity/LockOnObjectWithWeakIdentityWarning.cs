﻿using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

namespace ReCommendedExtension.Analyzers.LockOnObjectWithWeakIdentity;

[RegisterConfigurableSeverity(
    SeverityId,
    null,
    HighlightingGroupIds.CodeSmell,
    "Lock on object with weak identity" + ZoneMarker.Suffix,
    "",
    Severity.WARNING)]
[ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
public sealed record LockOnObjectWithWeakIdentityWarning : Highlighting
{
    const string SeverityId = "LockOnObjectWithWeakIdentity";

    readonly ICSharpExpression monitor;

    internal LockOnObjectWithWeakIdentityWarning(string message, ICSharpExpression monitor) : base(message) => this.monitor = monitor;

    public override DocumentRange CalculateRange() => monitor.GetDocumentRange();
}
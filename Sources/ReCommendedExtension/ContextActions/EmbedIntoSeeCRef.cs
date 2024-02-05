﻿using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;

namespace ReCommendedExtension.ContextActions;

[ContextAction(
    Group = "C#",
    Name = """Embed the word or selection into <see cref="..."/> in XML doc comments""" + ZoneMarker.Suffix,
    Description = """Embed the word or selection into <see cref="..."/> in XML doc comments.""")]
public sealed class EmbedIntoSeeCRef(ICSharpContextActionDataProvider provider) : EncompassInDocComment(provider)
{
    protected override string Encompass(string text) => $"""<see cref="{text}"/>""";

    public override string Text => """Embed into <see cref="..."/>""";
}
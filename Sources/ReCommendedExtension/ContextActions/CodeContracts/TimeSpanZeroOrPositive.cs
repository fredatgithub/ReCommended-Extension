using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Util;

namespace ReCommendedExtension.ContextActions.CodeContracts;

[ContextAction(
    Group = "C#",
    Name = "Add contract: time span is zero or positive" + ZoneMarker.Suffix,
    Description = "Adds a contract that a time span is greater than or equal to zero.")]
public sealed class TimeSpanZeroOrPositive : TimeSpan
{
    public TimeSpanZeroOrPositive(ICSharpContextActionDataProvider provider) : base(provider) { }

    protected override string GetContractTextForUI(string contractIdentifier) => $"{contractIdentifier} >= {nameof(System.TimeSpan.Zero)}";

    protected override IExpression GetExpression(CSharpElementFactory factory, IExpression contractExpression)
        => factory.CreateExpression(
            $"$0 >= $1.{nameof(System.TimeSpan.Zero)}",
            contractExpression,
            TypeElementUtil.GetTypeElementByClrName(PredefinedType.TIMESPAN_FQN, Provider.PsiModule));
}
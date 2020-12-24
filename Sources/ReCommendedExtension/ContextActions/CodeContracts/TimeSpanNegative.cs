using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.ContextActions;
using JetBrains.ReSharper.Feature.Services.CSharp.Analyses.Bulbs;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Util;

namespace ReCommendedExtension.ContextActions.CodeContracts
{
    [ContextAction(
        Group = "C#",
        Name = "Add contract: time span is negative" + ZoneMarker.Suffix,
        Description = "Adds a contract that a time span is less than zero.")]
    public sealed class TimeSpanNegative : TimeSpan
    {
        public TimeSpanNegative([NotNull] ICSharpContextActionDataProvider provider) : base(provider) { }

        protected override string GetContractTextForUI(string contractIdentifier) => $"{contractIdentifier} < {nameof(System.TimeSpan.Zero)}";

        protected override IExpression GetExpression(CSharpElementFactory factory, IExpression contractExpression)
            => factory.CreateExpression(
                $"$0 < $1.{nameof(System.TimeSpan.Zero)}",
                contractExpression,
                TypeElementUtil.GetTypeElementByClrName(PredefinedType.TIMESPAN_FQN, Provider.PsiModule));
    }
}
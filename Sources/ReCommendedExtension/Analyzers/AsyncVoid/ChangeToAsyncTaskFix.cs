using JetBrains.Application.Progress;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.QuickFixes;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Resources.Shell;
using JetBrains.TextControl;

namespace ReCommendedExtension.Analyzers.AsyncVoid;

[QuickFix]
public sealed class ChangeToAsyncTaskFix : QuickFixBase
{
    readonly AvoidAsyncVoidWarning highlighting;

    readonly IDeclaredType taskType;

    public ChangeToAsyncTaskFix(AvoidAsyncVoidWarning highlighting)
    {
        this.highlighting = highlighting;

        var psiModule = highlighting.Declaration.GetPsiModule();

        var predefinedType = psiModule.GetPredefinedType();

        taskType = predefinedType.Task;
    }

    public override bool IsAvailable(JetBrains.Util.IUserDataHolder cache) => true;

    public override string Text => $"Change return type to '{taskType.GetClrName().ShortName}'";

    protected override Action<ITextControl> ExecutePsiTransaction(ISolution solution, IProgressIndicator progress)
    {
        using (WriteLockCookie.Create())
        {
            highlighting.Declaration.SetType(taskType.ToIType());
        }

        return _ => { };
    }
}
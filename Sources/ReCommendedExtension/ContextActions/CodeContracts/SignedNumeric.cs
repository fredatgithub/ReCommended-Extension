using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.CSharp.ContextActions;
using JetBrains.ReSharper.Psi;
using ReCommendedExtension.ContextActions.CodeContracts.Internal;

namespace ReCommendedExtension.ContextActions.CodeContracts
{
    public abstract class SignedNumeric : AddContractContextAction
    {
        private protected SignedNumeric([NotNull] ICSharpContextActionDataProvider provider) : base(provider) { }

        [CanBeNull]
        private protected CSharpNumericTypeInfo NumericTypeInfo { get; private set; }

        protected sealed override bool IsAvailableForType(IType type)
        {
            NumericTypeInfo = CSharpNumericTypeInfo.TryCreate(type);

            if (NumericTypeInfo != null)
            {
                if (NumericTypeInfo.IsSigned)
                {
                    return true;
                }

                NumericTypeInfo = null;
            }

            return false;
        }
    }
}
﻿using System;
using System.IO;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace NonDisposable
{
    internal class Parent
    {
        public virtual int NotAnnotated() => 0;

        [Pure]
        public virtual int AnnotatedWithPure() => 0;

        [MustUseReturnValue]
        public virtual int AnnotatedWithMustUseReturnValue() => 0;

        [MustDisposeResource]
        public virtual int AnnotatedWithMustDisposeResource() => 0;

        [MustDisposeResource(false)]
        public virtual int AnnotatedWithMustDisposeResourceFalse() => 0;

        [MustDisposeResource(true)]
        public virtual int AnnotatedWithMustDisposeResourceTrue() => 0;
    }

    internal class Child : Parent
    {
        public override int Not{on}Annotated() => 0;

        public override int Annotated{on}WithPure() => 0;

        public override int Annotated{off}WithMustUseReturnValue() => 0;

        public override int Annotated{on}WithMustDisposeResource() => 0;

        public override int Annotated{on}WithMustDisposeResourceFalse() => 0;

        public override int Annotated{on}WithMustDisposeResourceTrue() => 0;

        public int Oth{on}er() => 0;
    }
}

namespace Disposable
{
    internal ref struct DisposableRefStruct
    {
        public void Dispose() { }
    }

    internal class Parent
    {
        public virtual IDisposable NotAnnotated() => throw new NotImplementedException();

        [Pure]
        public virtual IAsyncDisposable AnnotatedWithPure() => throw new NotImplementedException();

        [MustUseReturnValue]
        public virtual Stream AnnotatedWithMustUseReturnValue() => throw new NotImplementedException();

        [MustDisposeResource]
        public virtual Stream AnnotatedWithMustDisposeResource() => throw new NotImplementedException();

        [MustDisposeResource(false)]
        public virtual IAsyncDisposable AnnotatedWithMustDisposeResourceFalse() => throw new NotImplementedException();

        [MustDisposeResource(true)]
        public virtual IAsyncDisposable AnnotatedWithMustDisposeResourceTrue() => throw new NotImplementedException();
    }

    internal class Child : Parent
    {
        public override IDisposable Not{off}Annotated() => throw new NotImplementedException();

        public override IAsyncDisposable Annotated{off}WithPure() => throw new NotImplementedException();

        public override Stream Annotated{off}WithMustUseReturnValue() => throw new NotImplementedException();

        public override Stream Annotated{off}WithMustDisposeResource() => throw new NotImplementedException();

        public override IAsyncDisposable Annotated{off}WithMustDisposeResourceFalse() => throw new NotImplementedException();

        public override IAsyncDisposable Annotated{off}WithMustDisposeResourceTrue() => throw new NotImplementedException();

        public DisposableRefStruct Oth{off}er() => 0;
    }

    internal class ParentTaskLike
    {
        public virtual Task<IDisposable> NotAnnotated() => throw new NotImplementedException();

        [Pure]
        public virtual Task<IAsyncDisposable> AnnotatedWithPure() => throw new NotImplementedException();

        [MustUseReturnValue]
        public virtual Task<Stream> AnnotatedWithMustUseReturnValue() => throw new NotImplementedException();

        [MustDisposeResource]
        public virtual ValueTask<Stream> AnnotatedWithMustDisposeResource() => throw new NotImplementedException();

        [MustDisposeResource(false)]
        public virtual ValueTask<IAsyncDisposable> AnnotatedWithMustDisposeResourceFalse() => throw new NotImplementedException();
    }

    internal class ChildTaskLike : ParentTaskLike
    {
        public override Task<IDisposable> Not{off}Annotated() => throw new NotImplementedException();

        public override Task<IAsyncDisposable> Annotated{off}WithPure() => throw new NotImplementedException();

        public override Task<Stream> Annotated{off}WithMustUseReturnValue() => throw new NotImplementedException();

        public override ValueTask<Stream> Annotated{off}WithMustDisposeResource() => throw new NotImplementedException();

        public override ValueTask<IAsyncDisposable> Annotated{off}WithMustDisposeResourceFalse() => throw new NotImplementedException();

        public ValueTask<IDisposable> Ot{off}her() => throw new NotImplementedException();
    }

    internal class WithAnnotations
    {
        [Pure]
        public IDisposable With{off}Pure() => throw new NotImplementedException();

        [MustUseReturnValue]
        public IDisposable With{off}MustUseReturnValue() => throw new NotImplementedException();

        [MustDisposeResource]
        public IDisposable With{off}MustDisposeResource() => throw new NotImplementedException();

        [MustDisposeResource(false)]
        public IDisposable With{off}MustDisposeResourceFalse() => throw new NotImplementedException();

        [MustDisposeResource(true)]
        public IDisposable With{off}MustDisposeResourceTrue() => throw new NotImplementedException();
    }
}

namespace KindsOfMethods
{
    internal class Availability
    {
        string Meth{on}od() => null;

        void Method{off}2() => null;

        async Task Method{on}3() => await Task.Yield();

        string Proper{off}ty => null;
    }
}
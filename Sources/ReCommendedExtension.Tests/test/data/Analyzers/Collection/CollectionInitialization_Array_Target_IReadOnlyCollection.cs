﻿using System;
using System.Collections.Generic;

namespace TargetReadOnlyCollection
{
    public class NonGenericClass
    {
        IReadOnlyCollection<int> field1 = new int[] { };
        IReadOnlyCollection<int> field2 = new int[] { 1, 2, 3 };
        IReadOnlyCollection<int> field3 = new int[0] { };
        IReadOnlyCollection<int> field4 = new int[3] { 1, 2, 3 };
        IReadOnlyCollection<int> field5 = new int[0];
        IReadOnlyCollection<int> field6 = new int[3];
        IReadOnlyCollection<int> field7 = new[] { 1, 2, 3 };
        IReadOnlyCollection<int> field8 = Array.Empty<int>();

        void Method(int a, int b, int c)
        {
            IReadOnlyCollection<int> var1 = new int[] { };
            IReadOnlyCollection<int> var2 = new int[] { a, b, c };
            IReadOnlyCollection<int> var3 = new int[0] { };
            IReadOnlyCollection<int> var4 = new int[3] { a, b, c };
            IReadOnlyCollection<int> var5 = new int[0];
            IReadOnlyCollection<int> var6 = new int[3];
            IReadOnlyCollection<int> var7 = new[] { a, b, c };
            IReadOnlyCollection<int> var8 = Array.Empty<int>();

            Consumer(new int[] { });
            Consumer(new int[] { a, b, c });
            Consumer(new int[0] { });
            Consumer(new int[3] { a, b, c });
            Consumer(new int[0]);
            Consumer(new int[3]);
            Consumer(new[] { a, b, c });
            Consumer(Array.Empty<int>());

            ConsumerGeneric(new int[] { });
            ConsumerGeneric(new int[] { a, b, c });
            ConsumerGeneric<int>(new int[0] { });
            ConsumerGeneric(new int[3] { a, b, c });
            ConsumerGeneric(new int[0]);
            ConsumerGeneric(new int[3]);
            ConsumerGeneric(new[] { a, b, c });
            ConsumerGeneric(Array.Empty<int>());
        }

        void Consumer(IReadOnlyCollection<int> items) { }
        void ConsumerGeneric<T>(IReadOnlyCollection<T> items) { }

        IReadOnlyCollection<int> Property1 { get; } = new int[] { };
        IReadOnlyCollection<int> Property2 { get; } = new int[] { 1, 2, 3 };
        IReadOnlyCollection<int> Property3 { get; set; } = new int[0] { };
        IReadOnlyCollection<int> Property4 { get; set; } = new int[3] { 1, 2, 3 };
        IReadOnlyCollection<int> Property5 => new int[0];
        IReadOnlyCollection<int> Property6 => new int[3];
        IReadOnlyCollection<int> Property7 => new[] { 1, 2, 3 };
        IReadOnlyCollection<int> Property8 => Array.Empty<int>();
    }

    public class GenericClass<T> where T : new()
    {
        IReadOnlyCollection<T> field1 = new T[] { };
        IReadOnlyCollection<T> field2 = new T[] { default, default(T), new() };
        IReadOnlyCollection<T> field3 = new T[0] { };
        IReadOnlyCollection<T> field4 = new T[3] { default, default(T), new() };
        IReadOnlyCollection<T> field5 = new T[0];
        IReadOnlyCollection<T> field6 = new T[3];
        IReadOnlyCollection<T> field7 = new[] { default, default(T), new() };
        IReadOnlyCollection<T> field8 = Array.Empty<T>();

        void Method(T a, T b, T c)
        {
            IReadOnlyCollection<T> var1 = new T[] { };
            IReadOnlyCollection<T> var2 = new T[] { a, b, c };
            IReadOnlyCollection<T> var3 = new T[0] { };
            IReadOnlyCollection<T> var4 = new T[3] { a, b, c };
            IReadOnlyCollection<T> var5 = new T[0];
            IReadOnlyCollection<T> var6 = new T[3];
            IReadOnlyCollection<T> var7 = new[] { a, b, c };
            IReadOnlyCollection<T> var8 = Array.Empty<T>();

            Consumer(new T[] { });
            Consumer(new T[] { a, b, c });
            Consumer(new T[0] { });
            Consumer(new T[3] { a, b, c });
            Consumer(new T[0]);
            Consumer(new T[3]);
            Consumer(new[] { a, b, c });
            Consumer(Array.Empty<T>());
        }

        void Consumer(IReadOnlyCollection<T> items) { }

        IReadOnlyCollection<T> Property1 { get; } = new T[] { };
        IReadOnlyCollection<T> Property2 { get; } = new T[] { default, default(T), new() };
        IReadOnlyCollection<T> Property3 { get; set; } = new T[0] { };
        IReadOnlyCollection<T> Property4 { get; set; } = new T[3] { default, default(T), new() };
        IReadOnlyCollection<T> Property5 => new T[0];
        IReadOnlyCollection<T> Property6 => new T[3];
        IReadOnlyCollection<T> Property7 => new[] { default, default(T), new() };
        IReadOnlyCollection<T> Property8 => Array.Empty<T>();
    }

    internal class A { }
    internal class B(int x = 0) : A { }

    public class InferenceClass
    {
        IReadOnlyCollection<A> field1 = new B[] { };
        IReadOnlyCollection<A> field2 = new B[] { new(1), new(2), new(3) };
        IReadOnlyCollection<A> field3 = new B[0] { };
        IReadOnlyCollection<A> field4 = new B[3] { new B(1), new B(2), new B(3) };
        IReadOnlyCollection<A> field5 = new B[0];
        IReadOnlyCollection<A> field6 = new B[3];
        IReadOnlyCollection<A> field7 = new[] { new B(1), new B(2), new B(3) };
        IReadOnlyCollection<A> field8 = Array.Empty<B>();

        void Method(B a, B b, B c)
        {
            IReadOnlyCollection<A> var1 = new B[] { };
            IReadOnlyCollection<A> var2 = new B[] { a, b, c };
            IReadOnlyCollection<A> var3 = new B[0] { };
            IReadOnlyCollection<A> var4 = new B[3] { a, b, c };
            IReadOnlyCollection<A> var5 = new B[0];
            IReadOnlyCollection<A> var6 = new B[3];
            IReadOnlyCollection<A> var7 = new[] { a, b, c };
            IReadOnlyCollection<A> var8 = Array.Empty<B>();

            Consumer(new B[] { });
            Consumer(new B[] { a, b, c });
            Consumer(new B[0] { });
            Consumer(new B[3] { a, b, c });
            Consumer(new B[0]);
            Consumer(new B[3]);
            Consumer(new[] { a, b, c });
            Consumer(Array.Empty<B>());
        }

        void Consumer(IReadOnlyCollection<A> items) { }

        IReadOnlyCollection<A> Property1 { get; } = new B[] { };
        IReadOnlyCollection<A> Property2 { get; } = new B[] { new(1), new(2), new(3) };
        IReadOnlyCollection<A> Property3 { get; set; } = new B[0] { };
        IReadOnlyCollection<A> Property4 { get; set; } = new B[3] { new B(1), new B(2), new B(3) };
        IReadOnlyCollection<A> Property5 => new B[0];
        IReadOnlyCollection<A> Property6 => new B[3];
        IReadOnlyCollection<A> Property7 => new[] { new B(1), new B(2), new B(3) };
        IReadOnlyCollection<A> Property8 => Array.Empty<B>();
    }
}
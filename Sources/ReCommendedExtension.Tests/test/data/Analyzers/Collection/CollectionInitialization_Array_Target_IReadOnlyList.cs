﻿using System;
using System.Collections.Generic;

namespace TargetReadOnlyList
{
    public class NonGenericClass
    {
        IReadOnlyList<int> field1 = new int[] { };
        IReadOnlyList<int> field2 = new int[] { 1, 2, 3 };
        IReadOnlyList<int> field3 = new int[0] { };
        IReadOnlyList<int> field4 = new int[3] { 1, 2, 3 };
        IReadOnlyList<int> field5 = new int[0];
        IReadOnlyList<int> field6 = new int[3];
        IReadOnlyList<int> field7 = new[] { 1, 2, 3 };
        IReadOnlyList<int> field8 = Array.Empty<int>();

        void Method(int a, int b, int c)
        {
            IReadOnlyList<int> var1 = new int[] { };
            IReadOnlyList<int> var2 = new int[] { a, b, c };
            IReadOnlyList<int> var3 = new int[0] { };
            IReadOnlyList<int> var4 = new int[3] { a, b, c };
            IReadOnlyList<int> var5 = new int[0];
            IReadOnlyList<int> var6 = new int[3];
            IReadOnlyList<int> var7 = new[] { a, b, c };
            IReadOnlyList<int> var8 = Array.Empty<int>();

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

        void Consumer(IReadOnlyList<int> items) { }
        void ConsumerGeneric<T>(IReadOnlyList<T> items) { }

        IReadOnlyList<int> Property1 { get; } = new int[] { };
        IReadOnlyList<int> Property2 { get; } = new int[] { 1, 2, 3 };
        IReadOnlyList<int> Property3 { get; set; } = new int[0] { };
        IReadOnlyList<int> Property4 { get; set; } = new int[3] { 1, 2, 3 };
        IReadOnlyList<int> Property5 => new int[0];
        IReadOnlyList<int> Property6 => new int[3];
        IReadOnlyList<int> Property7 => new[] { 1, 2, 3 };
        IReadOnlyList<int> Property8 => Array.Empty<int>();
    }

    public class GenericClass<T> where T : new()
    {
        IReadOnlyList<T> field1 = new T[] { };
        IReadOnlyList<T> field2 = new T[] { default, default(T), new() };
        IReadOnlyList<T> field3 = new T[0] { };
        IReadOnlyList<T> field4 = new T[3] { default, default(T), new() };
        IReadOnlyList<T> field5 = new T[0];
        IReadOnlyList<T> field6 = new T[3];
        IReadOnlyList<T> field7 = new[] { default, default(T), new() };
        IReadOnlyList<T> field8 = Array.Empty<T>();

        void Method(T a, T b, T c)
        {
            IReadOnlyList<T> var1 = new T[] { };
            IReadOnlyList<T> var2 = new T[] { a, b, c };
            IReadOnlyList<T> var3 = new T[0] { };
            IReadOnlyList<T> var4 = new T[3] { a, b, c };
            IReadOnlyList<T> var5 = new T[0];
            IReadOnlyList<T> var6 = new T[3];
            IReadOnlyList<T> var7 = new[] { a, b, c };
            IReadOnlyList<T> var8 = Array.Empty<T>();

            Consumer(new T[] { });
            Consumer(new T[] { a, b, c });
            Consumer(new T[0] { });
            Consumer(new T[3] { a, b, c });
            Consumer(new T[0]);
            Consumer(new T[3]);
            Consumer(new[] { a, b, c });
            Consumer(Array.Empty<T>());
        }

        void Consumer(IReadOnlyList<T> items) { }

        IReadOnlyList<T> Property1 { get; } = new T[] { };
        IReadOnlyList<T> Property2 { get; } = new T[] { default, default(T), new() };
        IReadOnlyList<T> Property3 { get; set; } = new T[0] { };
        IReadOnlyList<T> Property4 { get; set; } = new T[3] { default, default(T), new() };
        IReadOnlyList<T> Property5 => new T[0];
        IReadOnlyList<T> Property6 => new T[3];
        IReadOnlyList<T> Property7 => new[] { default, default(T), new() };
        IReadOnlyList<T> Property8 => Array.Empty<T>();
    }

    internal class A { }
    internal class B(int x = 0) : A { }

    public class InferenceClass
    {
        IReadOnlyList<A> field1 = new B[] { };
        IReadOnlyList<A> field2 = new B[] { new(1), new(2), new(3) };
        IReadOnlyList<A> field3 = new B[0] { };
        IReadOnlyList<A> field4 = new B[3] { new B(1), new B(2), new B(3) };
        IReadOnlyList<A> field5 = new B[0];
        IReadOnlyList<A> field6 = new B[3];
        IReadOnlyList<A> field7 = new[] { new B(1), new B(2), new B(3) };
        IReadOnlyList<A> field8 = Array.Empty<B>();

        void Method(B a, B b, B c)
        {
            IReadOnlyList<A> var1 = new B[] { };
            IReadOnlyList<A> var2 = new B[] { a, b, c };
            IReadOnlyList<A> var3 = new B[0] { };
            IReadOnlyList<A> var4 = new B[3] { a, b, c };
            IReadOnlyList<A> var5 = new B[0];
            IReadOnlyList<A> var6 = new B[3];
            IReadOnlyList<A> var7 = new[] { a, b, c };
            IReadOnlyList<A> var8 = Array.Empty<B>();

            Consumer(new B[] { });
            Consumer(new B[] { a, b, c });
            Consumer(new B[0] { });
            Consumer(new B[3] { a, b, c });
            Consumer(new B[0]);
            Consumer(new B[3]);
            Consumer(new[] { a, b, c });
            Consumer(Array.Empty<B>());
        }

        void Consumer(IReadOnlyList<A> items) { }

        IReadOnlyList<A> Property1 { get; } = new B[] { };
        IReadOnlyList<A> Property2 { get; } = new B[] { new(1), new(2), new(3) };
        IReadOnlyList<A> Property3 { get; set; } = new B[0] { };
        IReadOnlyList<A> Property4 { get; set; } = new B[3] { new B(1), new B(2), new B(3) };
        IReadOnlyList<A> Property5 => new B[0];
        IReadOnlyList<A> Property6 => new B[3];
        IReadOnlyList<A> Property7 => new[] { new B(1), new B(2), new B(3) };
        IReadOnlyList<A> Property8 => Array.Empty<B>();
    }
}
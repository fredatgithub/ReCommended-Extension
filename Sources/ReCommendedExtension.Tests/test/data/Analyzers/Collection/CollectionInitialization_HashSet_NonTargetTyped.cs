﻿using System;
using System.Collections.Generic;

namespace NonTargetTyped
{
    public class NonGenericClass
    {
        void Method(int a, int b, int c, IEnumerable<int> seq, IEqualityComparer<int> comparer)
        {
            var var01 = new HashSet<int>();
            var var02 = new HashSet<int> { a, b, c };
            var var03 = new HashSet<int>(8);
            var var04 = new HashSet<int>(8) { a, b, c };
            var var05 = new HashSet<int>(seq);
            var var06 = new HashSet<int>(seq) { a, b, c };
            var var07 = new HashSet<int>(comparer);
            var var08 = new HashSet<int>(comparer) { a, b, c };
            var var09 = new HashSet<int>(8, comparer);
            var var10 = new HashSet<int>(8, comparer) { a, b, c };
            var var11 = new HashSet<int>(seq, comparer);
            var var12 = new HashSet<int>(seq, comparer) { a, b, c };
        }
    }

    public class GenericClass<T>
    {
        void Method(T a, T b, T c, IEnumerable<T> seq, IEqualityComparer<T> comparer)
        {
            var var01 = new HashSet<T>();
            var var02 = new HashSet<T> { a, b, c };
            var var03 = new HashSet<T>(8);
            var var04 = new HashSet<T>(8) { a, b, c };
            var var05 = new HashSet<T>(seq);
            var var06 = new HashSet<T>(seq) { a, b, c };
            var var07 = new HashSet<T>(comparer);
            var var08 = new HashSet<T>(comparer) { a, b, c };
            var var09 = new HashSet<T>(8, comparer);
            var var10 = new HashSet<T>(8, comparer) { a, b, c };
            var var11 = new HashSet<T>(seq, comparer);
            var var12 = new HashSet<T>(seq, comparer) { a, b, c };
        }
    }
}
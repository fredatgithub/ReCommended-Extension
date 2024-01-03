﻿using System;
using System.Collections.Generic;

namespace TargetEnumerable
{
    public class GenericClass<T> where T : new()
    {
        void Method(T a, T b, T c)
        {
            Consumer(new T{caret}[] { });
        }

        void Consumer(IEnumerable<T> items) { }
    }
}
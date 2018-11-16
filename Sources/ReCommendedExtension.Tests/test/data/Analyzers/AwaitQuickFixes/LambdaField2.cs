﻿using System;
using System.Threading.Tasks;

namespace ReCommendedExtension.Tests.test.data.Analyzers.AwaitQuickFixes
{
    public class AwaitForLambdaFields
    {
        Func<Task<int>> LambdaField2 = async () =>
        {
            if (Environment.UserInteractive)
            {
                Console.WriteLine();
            }

            return await{caret} Task.FromResult(LocalFunction());

            int LocalFunction() => 3;
        };
    }
}
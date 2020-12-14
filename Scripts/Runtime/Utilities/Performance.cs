/*
 * Copyright (C) 2020 by Evolutex - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Bogdan Nikolaev <bodix321@gmail.com>
*/

using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Evolunity.Utilities
{
    public static class Performance
    {
        private const int IterationCount = 10000;

        public static void TestAction(Action action)
        {
            TestAction(action, "Action", IterationCount);
        }

        public static void TestAction(Action action, int iterationCount)
        {
            TestAction(action, "Action", iterationCount);
        }

        public static void TestAction(Action action, string name)
        {
            TestAction(action, name, IterationCount);
        }

        public static void TestAction(Action action, string name, int iterationCount)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < iterationCount; i++)
                action();

            stopwatch.Stop();

            Debug.Log(name + ": " + stopwatch.ElapsedMilliseconds);
        }

        public static void TestFunction<T>(Func<T> function)
        {
            TestFunction(function, "Function", IterationCount);
        }

        public static void TestFunction<T>(Func<T> function, int iterationCount)
        {
            TestFunction(function, "Function", iterationCount);
        }

        public static void TestFunction<T>(Func<T> function, string name)
        {
            TestFunction(function, name, IterationCount);
        }

        public static void TestFunction<T>(Func<T> function, string name, int iterationCount)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            for (int i = 0; i < iterationCount; i++)
                GC.KeepAlive(function());

            stopwatch.Stop();

            Debug.Log(name + ": " + stopwatch.ElapsedMilliseconds);
        }
    }
}
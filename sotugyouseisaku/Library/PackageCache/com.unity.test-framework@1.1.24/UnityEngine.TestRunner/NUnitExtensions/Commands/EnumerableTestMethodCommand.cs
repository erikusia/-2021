<<<<<<< HEAD:sotugyouseisaku/Library/PackageCache/com.unity.test-framework@1.1.24/UnityEngine.TestRunner/NUnitExtensions/Commands/EnumerableTestMethodCommand.cs
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using NUnit.Framework.Internal.Execution;
using UnityEngine.TestRunner.NUnitExtensions;
using Unity.Profiling;
using UnityEngine.TestRunner.NUnitExtensions.Runner;
using UnityEngine.TestTools.TestRunner;

namespace UnityEngine.TestTools
{
    internal class EnumerableTestMethodCommand : TestCommand, IEnumerableTestMethodCommand
    {
        private readonly TestMethod testMethod;

        public EnumerableTestMethodCommand(TestMethod testMethod)
            : base(testMethod)
        {
            this.testMethod = testMethod;
        }

        public IEnumerable ExecuteEnumerable(ITestExecutionContext context)
        {
            yield return null;

            IEnumerator currentExecutingTestEnumerator;
            try
            {
                currentExecutingTestEnumerator = new TestEnumeratorWrapper(testMethod).GetEnumerator(context);
            }
            catch (Exception ex)
            {
                context.CurrentResult.RecordException(ex);
                yield break;
            }
            
            if (currentExecutingTestEnumerator != null)
            {
                var testEnumeraterYieldInstruction = new TestEnumerator(context, currentExecutingTestEnumerator);

                yield return testEnumeraterYieldInstruction;

                var enumerator = testEnumeraterYieldInstruction.Execute();

                var executingEnumerator = ExecuteEnumerableAndRecordExceptions(enumerator, context);
                while (AdvanceEnumerator(executingEnumerator))
                {
                    yield return executingEnumerator.Current;
                }
            }
            else
            {
                if (context.CurrentResult.ResultState != ResultState.Ignored)
                {
                    context.CurrentResult.SetResult(ResultState.Success);
                }
            }
        }

        private bool AdvanceEnumerator(IEnumerator enumerator)
        {
            using (new ProfilerMarker(testMethod.MethodName).Auto())
                return enumerator.MoveNext();
        }

        private static IEnumerator ExecuteEnumerableAndRecordExceptions(IEnumerator enumerator, ITestExecutionContext context)
        {
            while (true)
            {
                try
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    context.CurrentResult.RecordException(ex);
                    break;
                }

                if (enumerator.Current is IEnumerator)
                {
                    var current = (IEnumerator)enumerator.Current;
                    yield return ExecuteEnumerableAndRecordExceptions(current, context);
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
        }

        public override TestResult Execute(ITestExecutionContext context)
        {
            throw new NotImplementedException("Use ExecuteEnumerable");
        }
    }
}
=======
using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Commands;
using NUnit.Framework.Internal.Execution;
using UnityEngine.TestRunner.NUnitExtensions;
using Unity.Profiling;
using UnityEngine.TestRunner.NUnitExtensions.Runner;
using UnityEngine.TestTools.TestRunner;

namespace UnityEngine.TestTools
{
    internal class EnumerableTestMethodCommand : TestCommand, IEnumerableTestMethodCommand
    {
        private readonly TestMethod testMethod;

        public EnumerableTestMethodCommand(TestMethod testMethod)
            : base(testMethod)
        {
            this.testMethod = testMethod;
        }

        public IEnumerable ExecuteEnumerable(ITestExecutionContext context)
        {
            yield return null;

            IEnumerator currentExecutingTestEnumerator;
            try
            {
                currentExecutingTestEnumerator = new TestEnumeratorWrapper(testMethod).GetEnumerator(context);
            }
            catch (Exception ex)
            {
                context.CurrentResult.RecordException(ex);
                yield break;
            }
            
            if (currentExecutingTestEnumerator != null)
            {
                var testEnumeraterYieldInstruction = new TestEnumerator(context, currentExecutingTestEnumerator);

                yield return testEnumeraterYieldInstruction;

                var enumerator = testEnumeraterYieldInstruction.Execute();

                var executingEnumerator = ExecuteEnumerableAndRecordExceptions(enumerator, context);
                while (AdvanceEnumerator(executingEnumerator))
                {
                    yield return executingEnumerator.Current;
                }
            }
            else
            {
                if (context.CurrentResult.ResultState != ResultState.Ignored)
                {
                    context.CurrentResult.SetResult(ResultState.Success);
                }
            }
        }

        private bool AdvanceEnumerator(IEnumerator enumerator)
        {
            using (new ProfilerMarker(testMethod.MethodName).Auto())
                return enumerator.MoveNext();
        }

        private static IEnumerator ExecuteEnumerableAndRecordExceptions(IEnumerator enumerator, ITestExecutionContext context)
        {
            while (true)
            {
                try
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                catch (Exception ex)
                {
                    context.CurrentResult.RecordException(ex);
                    break;
                }

                if (enumerator.Current is IEnumerator)
                {
                    var current = (IEnumerator)enumerator.Current;
                    yield return ExecuteEnumerableAndRecordExceptions(current, context);
                }
                else
                {
                    yield return enumerator.Current;
                }
            }
        }

        public override TestResult Execute(ITestExecutionContext context)
        {
            throw new NotImplementedException("Use ExecuteEnumerable");
        }
    }
}
>>>>>>> master:sotugyouseisaku/Library/PackageCache/com.unity.test-framework@1.1.22/UnityEngine.TestRunner/NUnitExtensions/Commands/EnumerableTestMethodCommand.cs

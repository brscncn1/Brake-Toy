using System.Collections;
using System;
using NUnit.Framework;

namespace BrakeToy.Tests
{
    public sealed class CoreTests
    {
        public static IEnumerable Checks
        {
            get
            {
                foreach (var pair in CoreChecks.Create())
                    yield return new TestCaseData(pair.Value).SetName(pair.Key);
            }
        }
        [TestCaseSource(nameof(Checks))]
        public void Check(Action check) => check();
    }
}

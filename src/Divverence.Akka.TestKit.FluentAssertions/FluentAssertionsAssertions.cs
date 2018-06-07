using System;
using Akka.TestKit;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Divverence.Akka.TestKit.FluentAssertions
{
    /// <summary>
    /// This class contains several common assert patterns used throughout this testkit.
    /// </summary>
    public class FluentAssertionsAssertions : ITestKitAssertions
    {
        /// <summary>
        /// Fails an assertion without checking any conditions.
        /// </summary>
        /// <param name="format">A template string to display if the assertion fails.</param>
        /// <param name="args">An optional object array that contains zero or more objects to format.</param>
        public void Fail(string format = "", params object[] args)
        {
            false.Should().BeTrue(format, args);
        }

        /// <summary>
        /// Verifies that a specified <paramref name="condition"/> is true.
        /// </summary>
        /// <param name="condition">The condition that is being verified.</param>
        /// <param name="format">A template string to display if the assertion fails.</param>
        /// <param name="args">An optional object array that contains zero or more objects to format.</param>
        public void AssertTrue(bool condition, string format = "", params object[] args)
        {
            condition.Should().BeTrue(format, args);
        }

        /// <summary>
        /// Verifies that a specified <paramref name="condition"/> is false.
        /// </summary>
        /// <param name="condition">The condition that is being verified.</param>
        /// <param name="format">A template string to display if the assertion fails.</param>
        /// <param name="args">An optional object array that contains zero or more objects to format.</param>
        public void AssertFalse(bool condition, string format = "", params object[] args)
        {
            condition.Should().BeFalse(format, args);
        }

        /// <summary>
        /// Verifies that the two specified values (<paramref name="expected"/> and <paramref name="actual"/>
        /// are equal using the built-in comparison function"/>.
        /// </summary>
        /// <typeparam name="T">The type that is being compared.</typeparam>
        /// <param name="expected">The expected value of the object</param>
        /// <param name="actual">The actual value of the object</param>
        /// <param name="format">A template string to display if the assertion fails.</param>
        /// <param name="args">An optional object array that contains zero or more objects to format.</param>
        public void AssertEqual<T>(T expected, T actual, string format = "", params object[] args)
        {
            actual.Should().Be(actual, format, args);
        }

        /// <summary>
        /// Verifies that the two specified values (<paramref name="expected"/> and <paramref name="actual"/>
        /// are equal using a specified comparison function <paramref name="comparer"/>.
        /// </summary>
        /// <typeparam name="T">The type that is being compared.</typeparam>
        /// <param name="expected">The expected value of the object</param>
        /// <param name="actual">The actual value of the object</param>
        /// <param name="comparer">The function used to compare the two specified values.</param>
        /// <param name="format">A template string to display if the assertion fails.</param>
        /// <param name="args">An optional object array that contains zero or more objects to format.</param>
        public void AssertEqual<T>(T expected, T actual, Func<T, T, bool> comparer, string format = "", params object[] args)
        {
            Execute.Assertion
                .BecauseOf(format, args)
                .ForCondition(comparer(expected, actual))
                .FailWith($"{typeof(T).Name} was expected to be equal to `{expected}` (using a custom comparison), but was `{actual}` instead");
        }
    }
}

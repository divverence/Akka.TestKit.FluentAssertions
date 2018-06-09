using System;
using FluentAssertions;
using Xunit;

namespace Divverence.Akka.TestKit.FluentAssertions.Tests
{
    public class FluentAssertionsAssertionsTests
    {
        [Fact]
        public void FailThrows()
        {
            Action x = () => new FluentAssertionsAssertions().Fail("{0} {1}", "a", "b");
            x.Should().Throw<Exception>();
        }

        [Fact]
        public void AssertTrueThrowsWhenPassingFalse()
        {
            Action x = () => new FluentAssertionsAssertions().AssertTrue(false);
            x.Should().Throw<Exception>();
        }

        [Fact]
        public void AssertTrueDoesNotThrowWhenPassingTrue()
        {
            Action x = () => new FluentAssertionsAssertions().AssertTrue(true);
            x.Should().NotThrow();
        }

        [Fact]
        public void AssertFalseThrowsWhenPassingTrue()
        {
            Action x = () => new FluentAssertionsAssertions().AssertFalse(true);
            x.Should().Throw<Exception>();
        }

        [Fact]
        public void AssertFalseDoesNotThrowWhenPassingFalse()
        {
            Action x = () => new FluentAssertionsAssertions().AssertFalse(false);
            x.Should().NotThrow();
        }

        [Fact]
        public void AssertEqualDoesNotThrowWhenPassingIdenticalObjects()
        {
            var poes = "Cat";
            Action x = () => new FluentAssertionsAssertions().AssertEqual("TheCat", $"The{poes}" );
            x.Should().NotThrow();
        }

        [Fact]
        public void AssertEqualThrowsWhenPassingDifferentObjects() {
            Action x = () => new FluentAssertionsAssertions().AssertEqual("cat", "dog");
            x.Should().Throw<Exception>();
        }

        [Fact]
        public void AssertEqualFancyDoesNotThrowWhenPassingDifferentObjectsButAComparerThatReturnsTrue()
        {
            Action x = () =>
                new FluentAssertionsAssertions().AssertEqual("Cat", "cat", (_, __) => _.Equals(__, StringComparison.InvariantCultureIgnoreCase));
            x.Should().NotThrow();
        }

        [Fact]
        public void AssertEqualFancyThrowsWhenPassingIdenticalObjectsButAComparerThatReturnsFalse()
        {
            Action x = () =>
                new FluentAssertionsAssertions().AssertEqual("Cat", "Cat", (_, __) => false);
            x.Should().Throw<Exception>();
        }
    }
}

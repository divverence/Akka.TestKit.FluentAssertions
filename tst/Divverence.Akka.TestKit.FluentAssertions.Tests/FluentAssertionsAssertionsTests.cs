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
        public void AssetTrueThrowsWhenPassingFalse()
        {
            Action x = () => new FluentAssertionsAssertions().AssertTrue(false);
            x.Should().Throw<Exception>();
        }

        [Fact]
        public void AssetTrueDoesNotThrowWhenPassingTrue()
        {
            Action x = () => new FluentAssertionsAssertions().AssertTrue(true);
            x.Should().NotThrow();
        }

        [Fact]
        public void AssetFalseThrowsWhenPassingTrue()
        {
            Action x = () => new FluentAssertionsAssertions().AssertFalse(true);
            x.Should().Throw<Exception>();
        }

        [Fact]
        public void AssetFalseDoesNotThrowWhenPassingFalse()
        {
            Action x = () => new FluentAssertionsAssertions().AssertFalse(false);
            x.Should().NotThrow();
        }

        [Fact]
        public void AssetEqualDoesNotThrowWhenPassingIdenticalObjects()
        {
            var poes = "Cat";
            Action x = () => new FluentAssertionsAssertions().AssertEqual("TheCat", $"The{poes}" );
            x.Should().NotThrow();
        }

        [Fact]
        public void AssetEqualFancyDoesNotThrowWhenPassingDifferentObjectsButAComparerThatReturnsTrue()
        {
            Action x = () =>
                new FluentAssertionsAssertions().AssertEqual("Cat", "cat", (_, __) => _.Equals(__, StringComparison.InvariantCultureIgnoreCase));
            x.Should().NotThrow();
        }

        [Fact]
        public void AssetEqualFancyThrowsWhenPassingIdenticalObjectsButAComparerThatReturnsFalse()
        {
            Action x = () =>
                new FluentAssertionsAssertions().AssertEqual("Cat", "Cat", (_, __) => false);
            x.Should().Throw<Exception>();
        }
    }
}

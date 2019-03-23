using FluentAssertions;
using persistantNumbers;
using System;
using Xunit;

namespace persistendNumbers.Tests
{
    public class PersistenceTests
    {
        [Fact]
        public void PersistNumberphileNumber_ShouldHavePersistenceOf11()
        {
            var persistence = new Persistence();

            persistence.CalculatePersistence(277777788888899).Should().Be(11);
        }

        [Theory]
        [InlineData(5248, 2)]
        [InlineData(327, 2)]
        public void CalculatePersistence_OnKnowns_ShouldReturnProperResults(ulong input, int expected)
        {
            var persistence = new Persistence();

            persistence.CalculatePersistence(input).Should().Be(expected);
        }
    }
}

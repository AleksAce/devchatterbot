using DevChatter.Bot.Core.Commands;
using DevChatter.Bot.Core.Data.Specifications;
using FluentAssertions;
using Xunit;

namespace UnitTests.Core.Data.Specifications.CommandWordPolicyTests
{
    public class CacheKeyShould
    {
        [Fact]
        public void ContainRelevantDataFromSpecification_GivenByWordSpecification()
        {
            var policy = CommandWordPolicy.ByWord("Foo");


            policy.CacheKey.Should().Be("CommandWordEntity-ByWord-Foo");
        }

        [Fact]
        public void ContainRelevantDataFromSpecification_GivenByTypeSpecification()
        {
            var typeToCheck = typeof(CoinsCommand);
            var policy = CommandWordPolicy.ByType(typeof(CoinsCommand));


            policy.CacheKey.Should().Be($"CommandWordEntity-ByType-{typeToCheck.FullName}");
        }
        [Fact]
        public void ContainRelevantDataFromSpecification_GivenOnlyPrimariesSpecification()
        {
            var policy = CommandWordPolicy.OnlyPrimaries();

            policy.CacheKey.Should().Be("CommandWordEntity-OnlyPrimaries-");
        }
    }
}

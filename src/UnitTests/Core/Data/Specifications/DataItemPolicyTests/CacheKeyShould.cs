using DevChatter.Bot.Core.Commands;
using DevChatter.Bot.Core.Data.Model;
using DevChatter.Bot.Core.Data.Specifications;
using FluentAssertions;
using Xunit;

namespace UnitTests.Core.Data.Specifications.DataItemPolicyTests
{
    public class CacheKeyShould
    {
        [Fact]
        public void ContainTypeAndAll_GivenAllSpecification()
        {
            var policy = DataItemPolicy<CommandWordEntity>.All();


            policy.CacheKey.Should().Be("CommandWordEntity-All-");
        }

    }
}

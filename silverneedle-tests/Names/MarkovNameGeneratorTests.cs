// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Names
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Names;

    public class MarkovNameGeneratorTests
    {
        [Fact]
        public void UsesASeedOfDataToBuildChainsInLowerCase()
        {
            /*
            A => A
            A => B
            A => Terminator
            B => A
            B => B
            B => C
            C => Terminator
             */
            var names = new string [] { "AAA", "ABA", "ABB", "ABC"};
            var gen = new MarkovNameGenerator(names, 1);
            var aToken = gen.States.First(x => x.Key == "a");
            Assert.NotNull(aToken);
            Assert.Equal(3, aToken.Tokens.Keys.Count);
            Assert.Equal(3, aToken.Tokens["b"]); //Score for A=>B should be 3

            var bToken = gen.States.First(x => x.Key == "b");
            Assert.NotNull(bToken);
            Assert.Equal(4, bToken.Tokens.Keys.Count);
        }

        [Theory]
        [Repeat(100)]
        public void ShouldBuildAStringBasedOnValuesNotToExceedASpecifiedLength()
        {
            var names = new string[] { "Alpha", "Bravo", "Charlie", "Delta" };
            var gen = new MarkovNameGenerator(names, 1);
            var name = gen.Generate(6);
            Assert.True(name.Length <= 6);
        }

        [Fact]
        public void OrderCanBeIncreasedToProvideMoreConsistencyOfTokens()
        {
            /*
                Given abacad with an order of 2
                Then we should generate tokens like
                __ => a, _a => b, ab => a, ba => c, ac => a, ca => d, ad
             */
            var names = new string[] { "abacad" };
            var gen = new MarkovNameGenerator(names, 2);
            var tok = gen.States.First(x => x.Key == "ab");
            Assert.NotNull(tok);
            Assert.Equal("a", tok.Tokens.Keys.First());

            //With this model, the only valid generation is the input string
            Assert.Equal("Abacad", gen.Generate(6));
        }
    }
}
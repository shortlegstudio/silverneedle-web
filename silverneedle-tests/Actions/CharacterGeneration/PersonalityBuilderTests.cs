// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using Xunit;
using SilverNeedle.Actions.CharacterGeneration;
using SilverNeedle.Characters;

namespace Tests.Actions.CharacterGeneration
{
    
    public class PersonalityBuilderTests : RequiresDataFiles
    {
        [Fact]
        public void AssignsARandomPersonalityType()
        {
            var builder = new PersonalityBuilder();
            var cs = new CharacterSheet(CharacterStrategy.Default());

            builder.Random(cs);
            Assert.NotNull(cs.PersonalityType);
            Assert.IsType<PersonalityType>(cs.PersonalityType);            
        }

        [Fact]
        public void AssignsSomeRandomIdeals()
        {
            var builder = new PersonalityBuilder();
            var cs = new CharacterSheet(CharacterStrategy.Default());

            builder.Random(cs);
            Assert.NotNull(cs.Ideal);
            Assert.IsType<Ideal>(cs.Ideal);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using NUnit.Framework;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Characters;

namespace Actions.CharacterGenerator
{
    [TestFixture]
    public class PersonalityBuilderTests
    {
        [Fact]
        public void AssignsARandomPersonalityType()
        {
            var builder = new PersonalityBuilder();
            var cs = new CharacterSheet();

            builder.Random(cs);
            Assert.IsNotNull(cs.PersonalityType);
            Assert.IsInstanceOf(typeof(PersonalityType), cs.PersonalityType);            
        }

        [Fact]
        public void AssignsSomeRandomIdeals()
        {
            var builder = new PersonalityBuilder();
            var cs = new CharacterSheet();

            builder.Random(cs);
            Assert.IsNotNull(cs.Ideal);
            Assert.IsInstanceOf(typeof(Ideal), cs.Ideal);
        }
    }
}
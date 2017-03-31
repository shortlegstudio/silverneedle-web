// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Beastiary;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    [TestFixture]
    public class ConfigureSummonFamiliarTests
    {
        [Test]
        public void ChoosesAFamiliarForTheCharacter()
        {
            var familiars = new List<Familiar>();
            var bat = new Familiar("Bat");
            familiars.Add(bat);
            var subject = new ConfigureSummonFamiliar(new EntityGateway<Familiar>(familiars));
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());

            var summon = character.SpecialQualities.SpecialAbilities.First() as SummonFamiliar;
            Assert.That(summon.Familiar, Is.EqualTo(bat));
        }
    }
}
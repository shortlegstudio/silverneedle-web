// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Beastiary;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Utility;

    [TestFixture]
    public class ConfigureSummonFamiliarTests
    {
        Familiar bat;
        ConfigureSummonFamiliar subject;
        [SetUp]
        public void SetUp()
        {
            var familiars = new List<Familiar>();
            bat = new Familiar("Bat");
            familiars.Add(bat);
            subject = new ConfigureSummonFamiliar(new EntityGateway<Familiar>(familiars));
        }

        [Test]
        public void ChoosesAFamiliarForTheCharacter()
        {
            var character = new CharacterSheet();
            subject.Process(character, new CharacterBuildStrategy());

            var summon = character.SpecialQualities.SpecialAbilities.First() as SummonFamiliar;
            Assert.That(summon.Familiar, Is.EqualTo(bat));
        }

        [Test]
        public void SummonFamiliarModifiesTheCharacterStats()
        {
            bat.Modifiers.Add(new BasicStatModifier("Perception", 5, "bonus", "Familiar (Bat)"));
            var character = new CharacterSheet(new Skill[] { new Skill("Perception", AbilityScoreTypes.Wisdom, false) });
            var baseValue = character.GetSkillValue("Perception");
            subject.Process(character, new CharacterBuildStrategy());
            Assert.That(character.GetSkillValue("Perception"), Is.EqualTo(baseValue + 5));
            
        }
    }
}
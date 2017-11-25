// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Characters.Prerequisites
{
    using Xunit;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Characters.Magic;

    public class CasterLevelPrerequisiteTests
    {
        [Fact]
        public void RequiresASpellCastingAbilityWithCorrectCasterLevel()
        {
            var character = CharacterTestTemplates.DruidDonna();
            var spellCasting = new DivineCasting(character.Get<ClassLevel>(), "Druid");
            character.Add(spellCasting);
            var prereq = new CasterLevelPrerequisite("3");
            Assert.False(prereq.IsQualified(character));
            character.SetLevel(3);
            Assert.Equal(3, spellCasting.CasterLevel);
            Assert.True(prereq.IsQualified(character));
        }

        [Fact]
        public void NoSpellCastingNoQualification()
        {
            var character = CharacterTestTemplates.DruidDonna();
            character.SetLevel(10);
            var prereq = new CasterLevelPrerequisite("3");
            Assert.False(prereq.IsQualified(character));
        }

    }
}
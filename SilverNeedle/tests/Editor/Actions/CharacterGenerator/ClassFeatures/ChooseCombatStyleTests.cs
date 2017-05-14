// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;

    [TestFixture]
    public class ChooseCombatStyleTests
    {
        [Test]
        public void ChooseACombatStyle()
        {
            var character = new CharacterSheet();
            var step = new ChooseCombatStyle();
            step.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Get<CombatStyle>(), Is.Not.Null); 
        }
    }
}
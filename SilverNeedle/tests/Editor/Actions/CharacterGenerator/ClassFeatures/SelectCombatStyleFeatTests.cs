// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator.ClassFeatures
{
    using System.Linq;
    using NUnit.Framework;
    using SilverNeedle.Actions.CharacterGenerator.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    [TestFixture]
    public class SelectCombatStyleFeatTests
    {
        [Test]
        public void ChoosesAFeatFromTheAvailableFeats()
        {
            var someFeat = new Feat();
            someFeat.Name = "Feat One";
            var feats = new Feat[] { someFeat };
            var gateway = new EntityGateway<Feat>(feats);
            var yaml = @"
- name: Archery
  bonus-feats:
    - level: 1
      feats: feat one".ParseYaml().Children.First();
            var combatStyle = new CombatStyle(yaml);
            var character = new CharacterSheet();
            var cls = new Class();
            character.SetClass(cls);
            character.SetLevel(2);
            character.Add(combatStyle);
            var step = new SelectCombatStyleFeat(gateway);
            step.Process(character, new CharacterBuildStrategy());
            Assert.That(character.Feats, Contains.Item(someFeat));
        }
    }

}
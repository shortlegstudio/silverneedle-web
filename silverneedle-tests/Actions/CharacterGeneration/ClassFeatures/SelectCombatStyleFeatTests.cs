// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration.ClassFeatures;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class SelectCombatStyleFeatTests
    {
        [Fact]
        public void ChoosesAFeatFromTheAvailableFeats()
        {
            var someFeat = new Feat();
            someFeat.Name = "Feat One";
            var gateway = EntityGateway<Feat>.LoadWithSingleItem(someFeat);
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
            step.ExecuteStep(character, new CharacterBuildStrategy());
            Assert.Contains(someFeat, character.Feats);
        }
    }

}
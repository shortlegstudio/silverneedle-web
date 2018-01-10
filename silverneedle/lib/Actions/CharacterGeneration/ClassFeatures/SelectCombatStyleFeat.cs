// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectCombatStyleFeat : ICharacterDesignStep, ICharacterFeatureCommand
    {

        public SelectCombatStyleFeat()
        {
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var combatStyle = components.Get<CombatStyle>();
            var rangerLevel = components.Get<ClassLevel>();
            var token = new FeatToken(combatStyle.GetFeats(rangerLevel.Level), true);
            components.Add(token);
        }
    }
}
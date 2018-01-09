// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    public class ConfigureDivineBond : ICharacterDesignStep, ICharacterFeatureCommand
    {
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var list = new SpecialAbility[] { new DivineBondMount(), new DivineBondWeapon() };
            var option = list.ChooseOne();
            components.Add(option);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    public class ConfigureDivineBond : ICharacterDesignStep
    {
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var list = new SpecialAbility[] { new DivineBondMount(), new DivineBondWeapon() };
            var option = list.ChooseOne();
            character.Add(option);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGenerator.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class ConfigureHuntersBond : ICharacterDesignStep
    {
        private string[] bonds;
        public ConfigureHuntersBond(IObjectStore options)
        {
            this.bonds = options.GetList("bonds");
        }
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var hunterBond = new HuntersBond(this.bonds.ChooseOne());
            character.Add(hunterBond);
        }
    }
}
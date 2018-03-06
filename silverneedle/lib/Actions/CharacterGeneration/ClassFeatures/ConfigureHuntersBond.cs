// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class ConfigureHuntersBond : ICharacterDesignStep, IFeatureCommand
    {
        private string[] bonds;
        public ConfigureHuntersBond(IObjectStore options)
        {
            this.bonds = options.GetList("bonds");
        }

        public void Execute(ComponentContainer components)
        {
            var hunterBond = new HuntersBond(this.bonds.ChooseOne());
            components.Add(hunterBond);
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Bestiary;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class ConfigureSummonFamiliar : ICharacterDesignStep, IFeatureCommand
    {
        private EntityGateway<Familiar> familiars;
        public ConfigureSummonFamiliar()
        {
            familiars = GatewayProvider.Get<Familiar>();
        }

        public ConfigureSummonFamiliar(EntityGateway<Familiar> familiars)
        {
            this.familiars = familiars;
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var familiar = familiars.ChooseOne();
            var summon = new SummonFamiliar(familiar);
            components.Add(summon);
        }
    }
}

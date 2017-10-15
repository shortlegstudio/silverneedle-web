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

    public class ConfigureSummonFamiliar : ICharacterDesignStep
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

        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var familiar = familiars.ChooseOne();
            var summon = new SummonFamiliar(familiar);
            character.Add(summon);
        }
    }
}

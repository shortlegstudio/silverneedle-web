// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.Background
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Serialization;

    public class BirthCircumstanceSelector : ICharacterDesignStep
    {
        private EntityGateway<BirthCircumstance> circumstances;

        public BirthCircumstanceSelector()
        {
            circumstances = GatewayProvider.Get<BirthCircumstance>();
        }

        public BirthCircumstanceSelector(EntityGateway<BirthCircumstance> circumstances)
        {
            this.circumstances = circumstances;
        }

        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var birth = circumstances.All().CreateWeightedTable().ChooseRandomly();
            var history = character.Get<History>();
            history.BirthCircumstance = birth;
        }

    }
}
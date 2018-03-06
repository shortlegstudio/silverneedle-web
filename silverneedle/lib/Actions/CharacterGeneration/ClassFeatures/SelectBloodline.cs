// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectBloodline : ICharacterDesignStep, IFeatureCommand
    {
        private EntityGateway<Bloodline> bloodlines;


        public SelectBloodline() : this(GatewayProvider.Get<Bloodline>())
        {

        }
        public SelectBloodline(EntityGateway<Bloodline> bloodlines)
        {
            this.bloodlines = bloodlines;
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            components.Add(bloodlines.ChooseOne());
        }
    }
}
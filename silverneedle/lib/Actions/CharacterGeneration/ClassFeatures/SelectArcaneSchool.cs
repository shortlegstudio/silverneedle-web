// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Magic;
    using SilverNeedle.Serialization;
    
    public class SelectArcaneSchool : ICharacterDesignStep, ICharacterFeatureCommand
    {
        private EntityGateway<ArcaneSchool> arcaneGateway;
        public SelectArcaneSchool() : this(GatewayProvider.Get<ArcaneSchool>())
        {

        }

        public SelectArcaneSchool(EntityGateway<ArcaneSchool> arcaneSchoolGateway)
        {
            this.arcaneGateway = arcaneSchoolGateway;
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var wizardCasting = components.Get<WizardCasting>();
            var school = arcaneGateway.ChooseOne();
            wizardCasting.SetFocusSchool(school);
            components.Add(school);
        }
    }
}
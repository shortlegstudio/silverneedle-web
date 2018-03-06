// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectRogueTalent : ICharacterDesignStep, IFeatureCommand
    {
        private EntityGateway<RogueTalent> rogueTalentGateway;
        private bool selectAdvanced;
        public SelectRogueTalent(IObjectStore configuration) : this(configuration, GatewayProvider.Get<RogueTalent>())
        {
        }

        public SelectRogueTalent(IObjectStore configuration, EntityGateway<RogueTalent> talents)
        {
            this.rogueTalentGateway = talents;
            selectAdvanced = configuration.GetBoolOptional("advanced-talents");
        }

        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            RogueTalent selected = null;
            
            var characterTalents = components.GetAll<RogueTalent>();
            var availableTalents = this.rogueTalentGateway.Where(talent => 
                (!talent.IsAdvancedTalent ||
                talent.IsAdvancedTalent == selectAdvanced) && 
                characterTalents.None(x => x.Matches(talent.Name)));
            
            //Prefer Advanced Talents
            
            var advanced = availableTalents.Where(x => x.IsAdvancedTalent);
            if(advanced.Count() > 0)
                selected = advanced.ChooseOne();
            else
                selected = availableTalents.ChooseOne();

            components.Add(selected);
        }
    }
}
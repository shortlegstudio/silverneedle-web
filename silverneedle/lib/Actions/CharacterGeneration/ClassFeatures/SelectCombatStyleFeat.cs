// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectCombatStyleFeat : ICharacterDesignStep
    {
        EntityGateway<Feat> featGateway;

        public SelectCombatStyleFeat()
        {
            featGateway = GatewayProvider.Get<Feat>();
        }

        public SelectCombatStyleFeat(EntityGateway<Feat> featGateway)
        {
            this.featGateway = featGateway;
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var combatStyle = character.Get<CombatStyle>();
            var rangerLevel = character.Get<ClassLevel>();
            var options = combatStyle.GetFeats(rangerLevel.Level);
            var chosen = featGateway.FindAll(options).Where(x => !character.Feats.Contains(x)).ChooseOne();
            character.Add(chosen);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectCombatStyle : ICharacterDesignStep, ICharacterFeatureCommand
    {
        EntityGateway<CombatStyle> combatStyleGateway;
        public SelectCombatStyle()
        {
            combatStyleGateway = GatewayProvider.Get<CombatStyle>();
        }
        public void ExecuteStep(CharacterSheet character)
        {
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var combat = combatStyleGateway.ChooseOne();
            components.Add(combat);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class ChooseCombatStyle : ICharacterDesignStep
    {
        EntityGateway<CombatStyle> combatStyleGateway;
        public ChooseCombatStyle()
        {
            combatStyleGateway = GatewayProvider.Get<CombatStyle>();
        }
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var combat = combatStyleGateway.ChooseOne();
            character.Add(combat);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectRagePower : ICharacterDesignStep
    {
        private EntityGateway<RagePower> ragePowers;
        public SelectRagePower()
        {
            this.ragePowers = GatewayProvider.Get<RagePower>();
        }

        public SelectRagePower(EntityGateway<RagePower> ragePowers)
        {
            this.ragePowers = ragePowers;
        }
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            var options = ragePowers.All().GetAllQualified<RagePower>(character);
            var power = options.ChooseOne();
            character.Add(power);
        }
    }
}
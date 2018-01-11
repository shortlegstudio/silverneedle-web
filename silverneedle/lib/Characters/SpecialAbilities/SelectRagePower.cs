// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using System;
    using SilverNeedle.Characters.Prerequisites;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class SelectRagePower : ICharacterFeatureCommand
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
        public void Execute(ComponentContainer components)
        {
            var options = ragePowers.All().GetAllQualified<RagePower>(components);
            var power = options.ChooseOne();
            components.Add(power);
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters.SpecialAbilities
{
    using Prerequisites;
    using Serialization;
    using Utility;

    public class SelectRagePower : ICharacterFeatureCommand
    {
        private readonly EntityGateway<RagePower> _ragePowers;
        public SelectRagePower()
        {
            _ragePowers = GatewayProvider.Get<RagePower>();
        }

        public SelectRagePower(EntityGateway<RagePower> ragePowers)
        {
            _ragePowers = ragePowers;
        }
        public void Execute(ComponentContainer components)
        {
            var options = _ragePowers
                .All()
                .GetAllQualified<RagePower>(components)
                .Exclude(components.GetAll<RagePower>());
            var power = options.ChooseOne();
            components.Add(power);
        }
    }
}
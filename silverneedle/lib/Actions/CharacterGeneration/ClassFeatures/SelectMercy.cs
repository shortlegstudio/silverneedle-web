// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectMercy : ICharacterDesignStep
    {
        private EntityGateway<Mercy> mercyGateway;
        public SelectMercy() 
        {
            this.mercyGateway = GatewayProvider.Get<Mercy>(); 
        }

        public SelectMercy(EntityGateway<Mercy> mercyGateway)
        {
            this.mercyGateway = mercyGateway;
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var mercies = character.Get<Mercies>();
            if(mercies == null)
            {
                mercies = new Mercies();
                character.Add(mercies);
            }

            var paladinLevel = character.Get<ClassLevel>();

            var selected = mercyGateway.Where(x => x.Level <= paladinLevel.Level && !mercies.MercyList.Contains(x)).ChooseOne();
            mercies.Add(selected);
        }
    }
}
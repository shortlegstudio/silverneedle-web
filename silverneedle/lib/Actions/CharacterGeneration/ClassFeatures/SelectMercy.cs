// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.ClassFeatures
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;

    public class SelectMercy : ICharacterDesignStep, ICharacterFeatureCommand
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
            Execute(character.Components);
        }

        public void Execute(Utility.ComponentContainer components)
        {
            var mercies = components.Get<Mercies>();
            if(mercies == null)
            {
                mercies = new Mercies();
                components.Add(mercies);
            }

            var paladinLevel = components.Get<ClassLevel>();

            var selected = mercyGateway.Where(x => x.Level <= paladinLevel.Level && !mercies.MercyList.Contains(x)).ChooseOne();
            mercies.Add(selected);
        }
    }
}
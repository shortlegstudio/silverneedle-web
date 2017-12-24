// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ApplyFavoredClassBonus : ICharacterDesignStep
    {
        private EntityGateway<FavoredClassOption> options;
        public ApplyFavoredClassBonus() : this(GatewayProvider.Get<FavoredClassOption>())
        {

        }

        public ApplyFavoredClassBonus(EntityGateway<FavoredClassOption> options)
        {
            this.options = options;
        }
        public void ExecuteStep(CharacterSheet character)
        {
            var choose = options.ChooseOne();
            character.Add(choose.CreateModifier());
        }
    }
}
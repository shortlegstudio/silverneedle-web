// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration.Background
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;
    public class CharacterDrawbackSelector : ICharacterDesignStep
    {
        EntityGateway<Drawback> drawbacks;

        public CharacterDrawbackSelector(EntityGateway<Drawback> drawbacks)
        {
            this.drawbacks = drawbacks;
        }

        public CharacterDrawbackSelector()
        {
            this.drawbacks = GatewayProvider.Get<Drawback>();
        }

        public void ExecuteStep(CharacterSheet character)
        {
            character.History.Drawback = SelectDrawback();
        }

        public Drawback SelectDrawback() 
        {
            var table = new WeightedOptionTable<Drawback>(drawbacks.All());
            return table.ChooseRandomly();
        }
    }
}


// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.CharacterGenerator
{
    public class ShopForCharacter : ICharacterDesignStep
    {
        public void Process(CharacterSheet character, CharacterBuildStrategy strategy)
        {
            var equiper = GatewayProvider.Find<CharacterDesigner>(strategy.EquipmentDesigner);
            if (equiper == null)
                equiper = GatewayProvider.Find<CharacterDesigner>("equip-default");
            equiper.Process(character, strategy);
        }
    }
}
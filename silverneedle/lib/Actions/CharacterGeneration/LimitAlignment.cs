// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using System;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class LimitAlignment : ICharacterDesignStep
    {
        string[] denyAlignments; 
        public LimitAlignment(IObjectStore configuration)
        {
            denyAlignments = configuration.GetList("deny");
        }
        public void ExecuteStep(CharacterSheet character, CharacterStrategy strategy)
        {
            foreach(var deny in denyAlignments)
            {
                var align = deny.EnumValue<CharacterAlignment>();
                strategy.FavoredAlignments.Disable(align);
            }
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Utility;
    using SilverNeedle.Serialization;

    public class LimitAlignment : IComponent
    {
        public ComponentContainer Parent { get; set; }
        string[] denyAlignments; 
        public LimitAlignment(IObjectStore configuration)
        {
            denyAlignments = configuration.GetList("deny");
        }
        public void Initialize(ComponentContainer components)
        {
            var strategy = components.Get<CharacterStrategy>();
            foreach(var deny in denyAlignments)
            {
                var align = deny.EnumValue<CharacterAlignment>();
                strategy.FavoredAlignments.Disable(align);
            }
        }
    }
}
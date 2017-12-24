// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    using System.Collections.Generic;
    using SilverNeedle.Characters.SpecialAbilities.BloodlinePowers;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class ElementalBloodline : Bloodline, IComponent
    {
        private ElementalType elementalType;
        public ElementalBloodline(IObjectStore configuration) : base(configuration)
        {

        }

        private ElementalBloodline(string name, string[] classSkillOptions, Dictionary<int, string> powers, Dictionary<int, string> bonusSpells, string[] bonusFeats)
            : base(name, classSkillOptions, powers, bonusSpells, bonusFeats)
        {
        }

        public override void Initialize(ComponentContainer components)
        {
            base.Initialize(components);
            this.elementalType = GatewayProvider.All<ElementalType>().ChooseOne();
            components.Add(elementalType);
        }
        public static ElementalBloodline Create(string name, string[] classSkillOptions, Dictionary<int, string> powers, Dictionary<int, string> bonusSpells, string[] bonusFeats)
        {
            return new ElementalBloodline(name, classSkillOptions, powers, bonusSpells, bonusFeats);
        }

        public override string Name
        {
            get
            {
                return "{0} ({1})".Formatted(base.Name, elementalType.Name);
            }
        }
    }
}
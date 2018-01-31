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

    public class DraconicBloodline : Bloodline, IComponent, IDraconicBloodline
    {
        public DragonType DragonType { get; private set; }
        public DraconicBloodline(IObjectStore configuration) : base(configuration)
        {

        }

        public override void Initialize(ComponentContainer components)
        {
            base.Initialize(components);
            this.DragonType = GatewayProvider.All<DragonType>().ChooseOne();
        }

        private DraconicBloodline(string name, string[] classSkillOptions, Dictionary<int, string> powers, Dictionary<int, string> bonusSpells, string[] bonusFeats)
            : base(name, classSkillOptions, powers, bonusSpells, bonusFeats)
        {
        }

        public static DraconicBloodline Create(string name, string[] classSkillOptions, Dictionary<int, string> powers, Dictionary<int, string> bonusSpells, string[] bonusFeats)
        {
            return new DraconicBloodline(name, classSkillOptions, powers, bonusSpells, bonusFeats);
        }

        public override string DisplayString()
        {
            return "Draconic Bloodline ({0})".Formatted(this.DragonType.Name);
        }
    }

    public interface IDraconicBloodline 
    {
        DragonType DragonType { get; }
    }
}
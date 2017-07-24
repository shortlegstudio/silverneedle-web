// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Actions;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Level 
    {
        public IList<IStatModifier> Modifiers { get; private set; }

        public IList<ICharacterDesignStep> Steps { get; private set; }
        public IList<FeatToken> FeatTokens { get; private set; }
        public IList<object> Abilities { get; private set; }
        public int Number { get; private set; }

        public Level()
        {
            Modifiers = new List<IStatModifier>();
            Steps = new List<ICharacterDesignStep>();
            FeatTokens = new List<FeatToken>();
            Abilities = new List<object>();
        }

        public Level(IObjectStore objectStore) : this()
        {
            Load(objectStore);
        }

        public Level(int number) : this()
        {
            Number = number;
        }

        private void Load(IObjectStore objectStore)
        {
            Number = objectStore.GetInteger("level");
            var featTokens = objectStore.GetObjectOptional("bonus-feats");
            if(featTokens != null)
            {
                foreach(var f in featTokens.Children)
                {
                    var tags = f.GetStringOptional("tags");
                    FeatTokens.Add(new FeatToken(tags));
                }
            }

            //Verbose and probably could be simplified
            var modifiers = objectStore.GetObjectOptional("modifiers");
            if (modifiers != null)
            {
                var mods = ParseStatModifiersYaml.ParseYaml(modifiers, string.Format("Level ({0})", Number));
                foreach(var m in mods) 
                {
                    Modifiers.Add(m);
                }
            }

            var steps = objectStore.GetObjectOptional("class-feature-steps");
            if(steps != null)
            {
                foreach(var step in steps.Children)
                {
                    var stepName = step.GetString("step");
                    Steps.Add(stepName.Instantiate<ICharacterDesignStep>(step));
                }
            }

            var abilities = objectStore.GetObjectOptional("abilities");
            if(abilities != null)
            {
                foreach(var abl in abilities.Children)
                {
                    var abilityName = abl.GetString("ability");
                    this.Abilities.Add(abilityName.Instantiate<object>(abl));
                }
            }
        }
    }
}
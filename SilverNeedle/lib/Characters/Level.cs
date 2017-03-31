// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Actions;
    using SilverNeedle.Utility;

    public class Level : IProvidesSpecialAbilities, IModifiesStats
    {
        public IList<SpecialAbility> SpecialAbilities { get; private set; }
        public IList<BasicStatModifier> Modifiers { get; private set; }

        public IList<ICharacterDesignStep> Steps { get; private set; }
        public int Number { get; private set; }

        public Level()
        {
            SpecialAbilities = new List<SpecialAbility>();
            Modifiers = new List<BasicStatModifier>();
            Steps = new List<ICharacterDesignStep>();
        }

        public Level(IObjectStore objectStore) : this()
        {
            Load(objectStore);
        }

        public Level(int number, IEnumerable<LevelAbility> abilities) : this(number)
        {
            foreach(var a in abilities) {
                SpecialAbilities.Add(a);
            }
        }

        public Level(int number) : this()
        {
            Number = number;
        }

        private void Load(IObjectStore objectStore)
        {
            Number = objectStore.GetInteger("level");
            var specials = objectStore.GetObjectOptional("special");
            if (specials != null)
            {
                foreach(var s in specials.Children)
                {
                    LevelAbility ability;
                    // If a special implementation is available
                    if (s.HasKey("implementation")) {
                        ability = s.GetString("implementation").Instantiate<LevelAbility>(s);
                    } else {
                        ability = new LevelAbility(
                        s.GetString("name"),
                        s.GetString("condition"),
                        s.GetString("type"));
                    }
                    SpecialAbilities.Add(ability);                    
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
                    Steps.Add(stepName.Instantiate<ICharacterDesignStep>());
                }
            }
        }
    }
}
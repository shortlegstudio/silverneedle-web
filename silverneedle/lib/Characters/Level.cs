namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Yaml;

    public class Level : IProvidesSpecialAbilities, IModifiesStats
    {
        public IList<SpecialAbility> SpecialAbilities { get; private set; }
        public IList<BasicStatModifier> Modifiers { get; private set; }
        public int Number { get; private set; }

        public Level(YamlNodeWrapper yaml)
        {
            SpecialAbilities = new List<SpecialAbility>();
            Modifiers = new List<BasicStatModifier>();
            Load(yaml);
        }

        public Level(int number, IEnumerable<LevelAbility> abilities) : this(number)
        {
            foreach(var a in abilities) {
                SpecialAbilities.Add(a);
            }
        }

        public Level(int number)
        {
            SpecialAbilities = new List<SpecialAbility>();
            Modifiers = new List<BasicStatModifier>();
            Number = number;
        }

        private void Load(YamlNodeWrapper yaml)
        {
            Number = yaml.GetInteger("level");
            var specials = yaml.GetNodeOptional("special");
            if (specials != null)
            {
                foreach(var s in specials.Children())
                {
                    SpecialAbilities.Add(new LevelAbility(
                        s.GetString("name"),
                        s.GetString("condition"),
                        s.GetString("type")
                    ));
                }
            }

            //Verbose and probably could be simplified
            var modifiers = yaml.GetNodeOptional("modifiers");
            if (modifiers != null)
            {
                var mods = ParseStatModifiersYaml.ParseYaml(modifiers, string.Format("Level ({0})", Number));
                foreach(var m in mods) 
                {
                    Modifiers.Add(m);
                }
            }
        }
    }
}
namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Yaml;

    public class Level : IProvidesSpecialAbilities
    {
        public IList<SpecialAbility> SpecialAbilities { get; private set; }
        
        public int Number { get; private set; }
        public Level(YamlNodeWrapper yaml)
        {
            SpecialAbilities = new List<SpecialAbility>();
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
        }
    }
}
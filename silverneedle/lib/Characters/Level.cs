namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Yaml;

    public class Level
    {
        public IList<LevelAbility> Special { get; set; }
        public int Number { get; private set; }
        public Level(YamlNodeWrapper yaml)
        {
            Special = new List<LevelAbility>();
            Load(yaml);
        }

        private void Load(YamlNodeWrapper yaml)
        {
            Number = yaml.GetInteger("level");
            var specials = yaml.GetNodeOptional("special");
            if (specials != null)
            {
                foreach(var s in specials.Children())
                {
                    Special.Add(new LevelAbility(
                        s.GetString("name"),
                        s.GetString("condition"),
                        s.GetString("type")
                    ));
                }
            }
        }
    }
}
namespace SilverNeedle.Characters
{
    using System;
    using SilverNeedle.Utility;

    public class LevelAbility : SpecialAbility
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public LevelAbility(string name, string condition, string type) : base(condition, type)
        {
            Name = name;
        }

        public LevelAbility(IObjectStore data)
        {
            this.Type = data.GetString("type");
            this.Condition = data.GetStringOptional("condition");
            this.Name = data.GetString("name");
        }

        public LevelAbility()
        {

        }

        public static LevelAbility InstatiateFromType(string typeName, IObjectStore data)
        {
            return Activator.CreateInstance(System.Type.GetType(typeName), data) as LevelAbility;
        }
    }
}
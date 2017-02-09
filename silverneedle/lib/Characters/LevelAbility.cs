namespace SilverNeedle.Characters
{
    using System;

    public class LevelAbility : SpecialAbility
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public LevelAbility(string name, string condition, string type) : base(condition, type)
        {
            Name = name;
        }

        public LevelAbility()
        {

        }

        public static LevelAbility InstatiateFromType(string typeName, string name, string condition, string type, int level)
        {
            var a = Activator.CreateInstance(System.Type.GetType(typeName));
            var ability = (LevelAbility)a;
            ability.Name = name;
            ability.Condition = condition;
            ability.Type = type;
            ability.Level = level;
            return a as LevelAbility;
        }
    }
}
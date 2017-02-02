namespace SilverNeedle.Characters
{
    public class LevelAbility : SpecialAbility
    {
        public string Name { get; set; }
        public LevelAbility(string name, string condition, string type) : base(condition, type)
        {
            Name = name;
        }
    }
}
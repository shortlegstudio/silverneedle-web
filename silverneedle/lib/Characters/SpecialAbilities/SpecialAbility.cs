// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class SpecialAbility : IAbility
    {
        public SpecialAbility()
        {
            this.Name = Inflector.Inflector.Titleize(this.GetType().Name);
        }

        public SpecialAbility(string condition, string type) : this()
        {
            this.Condition = condition;
            this.Type = type;
        }

        public string Condition { get; set; }
        public string Type { get; set; }

        public virtual string Name { get; protected set; }

        public virtual string DisplayString() { return Name; }
    }
}


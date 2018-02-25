// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Utility;

    public class CharacterLevelStatistic : IValueStatistic, IComponent, INameByType
    {
        private ComponentContainer _components;
        public int TotalValue
        {
            get
            {
                var levels = _components.GetAll<ClassLevel>();
                return levels.Sum(x => x.Level);
            }
        }

        public string Name { get { return this.Name(); } }

        public IEnumerable<IStatisticModifier> Modifiers => throw new System.NotImplementedException();

        public void AddModifier(IStatisticModifier modifier)
        {
            throw new System.NotImplementedException();
        }

        public int GetConditionalValue(string condition)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<string> GetConditions()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize(ComponentContainer components)
        {
            _components = components;
        }

        public bool Matches(string name)
        {
            return this.Name.EqualsIgnoreCase(name);
        }

        public void RemoveModifier(IStatisticModifier modifier)
        {
            throw new System.NotImplementedException();
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using System.Linq;
    using SilverNeedle.Actions;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Level : CharacterFeature
    {
        public IList<IStatModifier> Modifiers { get; private set; }

        public IList<ICharacterDesignStep> Steps { get; private set; }
        public IEnumerable<object> Abilities 
        { 
            get
            {
                return abilityClassNames.Select(keyValue =>
                    keyValue.Key.Instantiate<object>(keyValue.Value)
                );
            } 
        }
           
        public int Number { get; private set; }

        public Level()
        {
            Modifiers = new List<IStatModifier>();
            Steps = new List<ICharacterDesignStep>();
            abilityClassNames = new Dictionary<string, IObjectStore>();
        }

        public Level(IObjectStore objectStore) : base(objectStore)
        {
            Modifiers = new List<IStatModifier>();
            Steps = new List<ICharacterDesignStep>();
            abilityClassNames = new Dictionary<string, IObjectStore>();
            Load(objectStore);
        }

        public Level(int number) : this()
        {
            Number = number;
        }

        private IDictionary<string, IObjectStore> abilityClassNames;

        private void Load(IObjectStore objectStore)
        {
            Number = objectStore.GetInteger("level");

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
                    this.abilityClassNames.Add(abilityName, abl);
                }
            }
        }

        public void AddAbility(string instanceName, IObjectStore configuration)
        {
            abilityClassNames.Add(instanceName, configuration);
        }
    }
}
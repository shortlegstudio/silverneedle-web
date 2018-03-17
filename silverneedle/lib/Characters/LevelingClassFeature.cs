// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Characters
{
    using System.Collections.Generic;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class LevelingClassFeature : ClassFeature, IImprovesWithLevels
    {
        private IList<Level> levels = new List<Level>();

        public LevelingClassFeature(IObjectStore configuration) : base(configuration)
        {
            var levelConfiguration = configuration.GetObjectListOptional("levels");
            if(levelConfiguration != null)
            {
                foreach(var l in levelConfiguration)
                {
                    levels.Add(new Level(l));
                }
            }

        }

        public override void Initialize(ComponentContainer components)
        {
            base.Initialize(components);
            LeveledUp(components);
        }

        public void LeveledUp(ComponentContainer components)
        {
            var currentLevel = components.Get<ClassLevel>().Level;
            foreach(var l in levels)
            {
                if(l.Number == currentLevel)
                    components.Add(l);
            }
        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Domains
{
    using System;
    using SilverNeedle.Characters.SpecialAbilities;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    public class Animal : Domain, IImprovesWithLevels, IComponent
    {
        private SpeakWithAnimals speakWithAnimals;
        private AnimalCompanion animalCompanion;
        private ClassLevel sourceClass;
        public Animal(IObjectStore data) : base(data)
        {
            
        }

        public void Initialize(ComponentContainer components)
        {
            sourceClass = components.Get<ClassLevel>();
            speakWithAnimals = new SpeakWithAnimals();
            components.Add(speakWithAnimals);
        }

        public void LeveledUp(ComponentContainer components)
        {
            if(sourceClass.Level == 4)
            {
                animalCompanion = new AnimalCompanion();
                components.Add(animalCompanion);
            }
        }
    }
}
// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Actions
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Actions.CharacterGeneration;
    using SilverNeedle.Characters;
    using SilverNeedle.Dice;
    using SilverNeedle.Serialization;
    using SilverNeedle.Utility;

    
    public class ClassSelectorTests
    {
        public EntityGateway<Class> classGateway;

        public ClassSelector subject;

        public ClassSelectorTests()
        {
            var classes = new List<Class>();
            var hero = Class.CreateForTesting("Fighter", DiceSides.d10);
            var bartender = Class.CreateForTesting("Bartender", DiceSides.d4);

            classes.Add(hero);
            classes.Add(bartender);

            classGateway = EntityGateway<Class>.LoadFromList(classes);
            subject = new ClassSelector(classGateway);
        }

        [Fact]
        public void SelectARandomClassForACharacter()
        {
            var character = CharacterTestTemplates.AverageBob();
            subject.ChooseAny(character);
            Assert.NotNull(character.Class.Name);
        }

        [Fact]
        public void ChoosingClassFromWeightedOptionTableSelectsFromThoseClasses()
        {
            var character = CharacterTestTemplates.AverageBob();
            var choices = new WeightedOptionTable<string>();
            choices.AddEntry("Fighter", 10);

            subject.ChooseClass(character, choices);

            Assert.Equal("Fighter", character.Class.Name);
        }

        [Fact]
        public void EmptyOptionTableChoosesFromAnyOfTheClasses()
        {
            var character = CharacterTestTemplates.AverageBob();
            var choices = new WeightedOptionTable<string>();
            
            Assert.Null(character.Class);
            subject.ChooseClass(character, choices);
            Assert.NotNull(character.Class.Name);
        }

        [Fact]
        public void AddSpecialAbilitiesFromFirstLevelForClass()
        {
            var cls = Class.CreateForTesting();
            var lvl1 = new Level(1);
            cls.Levels.Add(lvl1);
            var character = CharacterTestTemplates.AverageBob();
            subject.AssignClass(character, cls);
        }
    }
}
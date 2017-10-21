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
            var hero = new Class();
            hero.Name = "Fighter";
            hero.HitDice = DiceSides.d10;
            var bartender = new Class();
            bartender.Name = "Bartender";
            bartender.HitDice = DiceSides.d10;

            classes.Add(hero);
            classes.Add(bartender);

            classGateway = EntityGateway<Class>.LoadFromList(classes);
            subject = new ClassSelector(classGateway);
        }

        [Fact]
        public void SelectARandomClassForACharacter()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            subject.ChooseAny(character);
            Assert.NotNull(character.Class.Name);
        }

        [Fact]
        public void ChoosingClassFromWeightedOptionTableSelectsFromThoseClasses()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var choices = new WeightedOptionTable<string>();
            choices.AddEntry("Fighter", 10);

            subject.ChooseClass(character, choices);

            Assert.Equal("Fighter", character.Class.Name);
        }

        [Fact]
        public void EmptyOptionTableChoosesFromAnyOfTheClasses()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            var choices = new WeightedOptionTable<string>();
            
            Assert.Null(character.Class);
            subject.ChooseClass(character, choices);
            Assert.NotNull(character.Class.Name);
        }

        [Fact]
        public void SettingClassAssignsClassSkills()
        {
            var character = new CharacterSheet(CharacterStrategy.Default());
            character.SkillRanks.AddSkill(new Skill("Climb", AbilityScoreTypes.Strength, false));
            
            var cls = new Class();
            cls.AddClassSkill("Climb");

            subject.AssignClass(character, cls);

            Assert.True(character.SkillRanks.GetSkill("Climb").ClassSkill);
        }

        [Fact]
        public void AddSpecialAbilitiesFromFirstLevelForClass()
        {
            var cls = new Class();
            var lvl1 = new Level(1);
            cls.Levels.Add(lvl1);
            var character = new CharacterSheet(CharacterStrategy.Default());
            subject.AssignClass(character, cls);
        }
    }
}
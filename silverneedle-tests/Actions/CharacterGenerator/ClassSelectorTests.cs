// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

namespace Tests.Actions
{
    using System.Collections.Generic;
    using Xunit;
    using SilverNeedle.Actions.CharacterGenerator;
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

            classGateway = new EntityGateway<Class>(classes);
            subject = new ClassSelector(classGateway);
        }

        [Fact]
        public void SelectARandomClassForACharacter()
        {
            var character = new CharacterSheet();
            subject.ChooseAny(character);
            Assert.NotNull(character.Class.Name);
        }

        [Fact]
        public void ChoosingClassFromWeightedOptionTableSelectsFromThoseClasses()
        {
            var character = new CharacterSheet();
            var choices = new WeightedOptionTable<string>();
            choices.AddEntry("Fighter", 10);

            subject.ChooseClass(character, choices);

            Assert.Equal("Fighter", character.Class.Name);
        }

        [Fact]
        public void EmptyOptionTableChoosesFromAnyOfTheClasses()
        {
            var character = new CharacterSheet();
            var choices = new WeightedOptionTable<string>();
            
            Assert.Null(character.Class);
            subject.ChooseClass(character, choices);
            Assert.NotNull(character.Class.Name);
        }

        [Fact]
        public void SettingClassAssignsClassSkills()
        {
            var skills = new List<Skill>();
            skills.Add(new Skill("Climb", AbilityScoreTypes.Strength, false));
            var character = new CharacterSheet(skills);
            
            var cls = new Class();
            cls.AddClassSkill("Climb");

            subject.AssignClass(character, cls);

            Assert.True(character.GetSkill("Climb").ClassSkill);

        }

        [Fact]
        public void AddSpecialAbilitiesFromFirstLevelForClass()
        {
            var cls = new Class();
            var lvl1 = new Level(1);
            lvl1.FeatTokens.Add(new FeatToken());
            cls.Levels.Add(lvl1);
            var character = new CharacterSheet();
            subject.AssignClass(character, cls);
            Assert.Equal(1, character.FeatTokens.Count);
        }
    }
}
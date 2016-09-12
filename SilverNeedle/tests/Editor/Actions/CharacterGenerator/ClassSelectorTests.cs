// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System;
using System.Collections.Generic;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Characters;
using SilverNeedle.Dice;
using Moq;

namespace Actions
{
    [TestFixture]
    public class ClassSelectorTests
    {
        public IClassGateway classGateway;
        [SetUp]
        public void SetUp()
        {
            var moq = new Mock<IClassGateway>();
            var classes = new List<Class>();
            var hero = new Class();
            hero.Name = "Fighter";
            hero.HitDice = DiceSides.d10;
            var bartender = new Class();
            bartender.Name = "Bartender";
            bartender.HitDice = DiceSides.d10;

            classes.Add(hero);
            classes.Add(bartender);

            moq.Setup(x => x.All()).Returns(classes);
            moq.Setup(x => x.GetByName("Fighter")).Returns(hero);
            classGateway = moq.Object;
        }

        [Test]
        public void SelectARandomClassForACharacter()
        {
            var character = new CharacterSheet();
            
            Assert.IsNull(character.Class);
            var selector = new ClassSelector(classGateway);
            selector.ChooseAny(character);
            Assert.IsNotNull(character.Class);
        }

        [Test]
        public void ChoosingAClassUpdatesHitPoints()
        {
            var character = new CharacterSheet();
            var selector = new ClassSelector(classGateway);
            selector.ChooseAny(character);

            Assert.Greater(character.MaxHitPoints, 0);
            Assert.Greater(character.CurrentHitPoints, 0);
        }

        [Test]
        public void ChoosingClassFromWeightedOptionTableSelectsFromThoseClasses()
        {
            var character = new CharacterSheet();
            var selector = new ClassSelector(classGateway);
            var choices = new WeightedOptionTable<string>();
            choices.AddEntry("Fighter", 10);

            selector.ChooseClass(character, choices);

            Assert.AreEqual("Fighter", character.Class.Name);
        }

        [Test]
        public void EmptyOptionTableChoosesFromAnyOfTheClasses()
        {
            var character = new CharacterSheet();
            var selector = new ClassSelector(classGateway);
            var choices = new WeightedOptionTable<string>();
            
            Assert.IsNull(character.Class);
            selector.ChooseClass(character, choices);
            Assert.IsNotNull(character.Class);
        }

        [Test]
        public void SettingClassAssignsClassSkills()
        {
            var skills = new List<Skill>();
            skills.Add(new Skill("Climb", AbilityScoreTypes.Strength, false));
            var character = new CharacterSheet(skills);
            
            var cls = new Class();
            cls.AddClassSkill("Climb");

            var selector = new ClassSelector(classGateway);
            selector.AssignClass(character, cls);

            Assert.IsTrue(character.GetSkill("Climb").ClassSkill);

        }

        
    }
}
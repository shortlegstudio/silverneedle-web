// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using System;
using NUnit.Framework;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Characters;
using SilverNeedle.Dice;

namespace Actions
{
    [TestFixture]
    public class ClassSelectorTests
    {
        [Test]
        public void SelectARandomClassForACharacter()
        {
            var character = new CharacterSheet();
            
            Assert.IsNull(character.Class);
            var selector = new ClassSelector(new ClassYamlGateway());
            selector.ChooseClass(character);
            Assert.IsNotNull(character.Class);
        }

        [Test]
        public void ChoosingAClassUpdatesHitPoints()
        {
            var character = new CharacterSheet();
            var selector = new ClassSelector(new ClassYamlGateway());
            selector.ChooseClass(character);

            Assert.Greater(character.MaxHitPoints, 0);
            Assert.Greater(character.CurrentHitPoints, 0);
        }
    }
}
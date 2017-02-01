// Copyright (c) 2016 Trevor Redfern
// 
// This software is released under the MIT License.
// http://opensource.org/licenses/mit-license.php

using Moq;
using NUnit.Framework;
using SilverNeedle;
using SilverNeedle.Actions.CharacterGenerator;
using SilverNeedle.Characters;

namespace Actions
{
    [TestFixture]
    public class FeatSelectorTests
    {
        Feat powerattack;
        Feat cleave;
        Feat empowerspell;

        FeatSelector selector;
        Mock<IFeatGateway> gateway;

        [SetUp]
        public void SetUp()
        {
            powerattack = new Feat();
            powerattack.Name = "Power Attack";

            cleave = new Feat();
            cleave.Name = "Cleave";
            cleave.Prerequisites.Add(new Prerequisites.FeatPrerequisite("power attack"));

            empowerspell = new Feat();
            empowerspell.Name = "Empower Spell";
            empowerspell.Tags.Add("metamagic");

            gateway = new Mock<IFeatGateway>();
            gateway.Setup(x => x.GetByName("power attack")).Returns(powerattack);
            gateway.Setup(x => x.GetByName("cleave")).Returns(cleave);
            gateway.Setup(x => x.GetByName("empower spell")).Returns(empowerspell);            

            selector = new FeatSelector(gateway.Object);
        }
        [Test]
        public void ChooseFeatBasedOnStrategy()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("power attack", 5000000);
            strategy.AddEntry("cleave", 1);
            
            var character = new CharacterSheet();
            character.FeatTokens.Add(new FeatToken());
            
            selector.SelectFeats(character, strategy);
            Assert.AreEqual(powerattack, character.Feats[0]); 

        }

        [Test]
        public void OnlySelectsFeatsThatCharacterQualifiesFor()
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("power attack", 1);
            strategy.AddEntry("cleave", 5000000);
            
            for(int i = 0; i < 1000; i++)
            {
                var character = new CharacterSheet();
                character.FeatTokens.Add(new FeatToken());

                selector.SelectFeats(character, strategy);
                Assert.AreEqual(powerattack, character.Feats[0]);
            }            
        } 

        [Test]
        public void SelectFeatsBasedOnTokensAvailable() 
        {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("power attack", 5000000);
            strategy.AddEntry("empower spell", 1);


            for(int i = 0; i < 1000; i++)
            {
                var character = new CharacterSheet();
                character.FeatTokens.Add(new FeatToken("metamagic"));

                selector.SelectFeats(character, strategy);
                Assert.AreEqual(empowerspell, character.Feats[0]);
            }            
        }

        [Test]
        public void SelectsAFeatForEachOutstandingToken() {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("power attack", 5000000);
            strategy.AddEntry("empower spell", 1);
        
            for(int i = 0; i < 1000; i++)
            {
                var character = new CharacterSheet();
                character.FeatTokens.Add(new FeatToken("metamagic"));
                character.FeatTokens.Add(new FeatToken());

                selector.SelectFeats(character, strategy);
                Assert.IsTrue(character.Feats.Contains(empowerspell));
                Assert.IsTrue(character.Feats.Contains(powerattack));
            }        
        }

        [Test]
        public void FeatTokensAreUsedUpAfterSelection() {
            var strategy = new WeightedOptionTable<string>();
            strategy.AddEntry("power attack", 5000000);
            var character = new CharacterSheet();
            character.FeatTokens.Add(new FeatToken());
            selector.SelectFeats(character, strategy);
            Assert.AreEqual(0, character.FeatTokens.Count);
        }

        [Test]
        public void IfNoPreferredFeatsArePossibleJustSelectRandomlyFromAnyPossible()
        {
            var strategy = new WeightedOptionTable<string>();
            var character = new CharacterSheet();
            gateway.Setup(x => x.GetQualifyingFeats(character)).Returns(new Feat[] {powerattack, empowerspell});
            character.FeatTokens.Add(new FeatToken());
            selector.SelectFeats(character, strategy);
            Assert.IsTrue(character.Feats[0] == powerattack || character.Feats[0] == empowerspell);
        }
    }
}
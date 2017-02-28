// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Actions.CharacterGenerator
{
    using NUnit.Framework;
    using SilverNeedle;
    using SilverNeedle.Actions.CharacterGenerator;
    using SilverNeedle.Actions.CharacterGenerator.Abilities;
    using SilverNeedle.Actions.NamingThings;
    using SilverNeedle.Characters;
    using SilverNeedle.Names;
    
    
    [TestFixture]
    public class CharacterBuilderTests
    {
        [Test]
        public void CanCreateARandomCaracter()
        {
            var characterBuilder = new CharacterBuilder(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageGateway()),
                new RaceSelector(new RaceGateway(), new TraitGateway()),
                new NameCharacter(new CharacterNamesGateway()),
                new FeatSelector(new FeatGateway()),
                new GatewayProvider()            
            );
            var c = characterBuilder.GenerateRandomCharacter();
            Assert.IsNotNull(c);
        }
    }
}
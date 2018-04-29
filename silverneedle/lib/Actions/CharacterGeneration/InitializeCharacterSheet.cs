// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Actions.CharacterGeneration
{
    using SilverNeedle.Characters;
    using SilverNeedle.Characters.Appearance;
    using SilverNeedle.Characters.Attacks;
    using SilverNeedle.Characters.Background;
    using SilverNeedle.Characters.Personalities;
    using SilverNeedle.Serialization;

    public class InitializeCharacterSheet : ICharacterDesignStep
    {
        EntityGateway<Skill> skills;
        EntityGateway<CharacterSheetSetup> setups;

        public InitializeCharacterSheet()
        {
            skills = GatewayProvider.Get<Skill>();

            setups = GatewayProvider.Get<CharacterSheetSetup>();
        }

        public void ExecuteStep(CharacterSheet character)
        {
            var configure = setups.Find("default");
            character.Components.AddNoInitialize(new object[] { 
                configure,
                new MeleeAttackBonus(),
                new RangeAttackBonus(),
                new DefenseStats(), 
                new MovementStats(),
                new CharacterAppearance(),
                new SkillRanks(),
                new Likes()
            });
            character.InitializeComponents();
            character.SkillRanks.FillSkills(skills.All());
        }
    }
}
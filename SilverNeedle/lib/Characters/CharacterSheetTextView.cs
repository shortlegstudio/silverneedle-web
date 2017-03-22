// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using SilverNeedle;
    using System.Linq;
    
    public class CharacterSheetTextView
    {
        public CharacterSheetTextView(CharacterSheet character)
        {
            Name = character.Name;
            Gender = character.Gender.ToString();
            Pronoun = character.Gender.Pronoun();
            PossessivePronoun = character.Gender.PossessivePronoun();
            Race = character.Race.Name;
            Class = character.Class.Name;
            Alignment = character.Alignment.ShortString();
            Initiative = character.Initiative.TotalValue.ToModifierString();
            Senses = FormatSenses(character);
            Perception = character.GetSkillValue("Perception").ToModifierString();
            Size = character.Size.Size.ToString();
            Level = character.Level.ToString();
            ArmorClass = character.Defense.ArmorClass().ToString();
            TouchArmorClass = character.Defense.TouchArmorClass().ToString();
            FlatFootedArmorClass = character.Defense.FlatFootedArmorClass().ToString();
            HitPoints = character.MaxHitPoints.ToString();
            FortitudeSave = character.Defense.FortitudeSave.ToString();
            ReflexSave = character.Defense.ReflexSave.ToString();
            WillSave = character.Defense.WillSave.ToString();
            MovementSpeed = character.Movement.BaseMovement.TotalValue.ToString();
            MovementSquares = character.Movement.BaseSquares.ToString();
            AttackTypes = character.Offense.Attacks().Select(x => x.AttackType.ToString()).ToArray();
            Attacks = character.Offense.Attacks().Select(x => string.Format("{0} {1} ({2}) {3}", 
                x.Weapon.Name, 
                x.AttackBonus.ToModifierString(), 
                x.Damage.ToString(), 
                x.Weapon.IsRanged ? x.Weapon.Range.ToRangeString() : "")).ToArray();
            SpecialAttacks = character.Offense.OffensiveAbilities.Select(x => x.Condition).ToArray();
            Strength = character.AbilityScores.GetScore(AbilityScoreTypes.Strength).ToString();
            StrengthModifier = character.AbilityScores.GetModifier(AbilityScoreTypes.Strength).ToModifierString();
            
            Dexterity = character.AbilityScores.GetScore(AbilityScoreTypes.Dexterity).ToString();
            DexterityModifier = character.AbilityScores.GetModifier(AbilityScoreTypes.Dexterity).ToModifierString();

            Constitution = character.AbilityScores.GetScore(AbilityScoreTypes.Constitution).ToString();
            ConstitutionModifier = character.AbilityScores.GetModifier(AbilityScoreTypes.Constitution).ToModifierString();

            Intelligence = character.AbilityScores.GetScore(AbilityScoreTypes.Intelligence).ToString();
            IntelligenceModifier = character.AbilityScores.GetModifier(AbilityScoreTypes.Intelligence).ToModifierString();

            Wisdom = character.AbilityScores.GetScore(AbilityScoreTypes.Wisdom).ToString();
            WisdomModifier = character.AbilityScores.GetModifier(AbilityScoreTypes.Wisdom).ToModifierString();

            Charisma = character.AbilityScores.GetScore(AbilityScoreTypes.Charisma).ToString();
            CharismaModifier = character.AbilityScores.GetModifier(AbilityScoreTypes.Charisma).ToModifierString();

            BaseAttackBonus = character.Offense.BaseAttackBonus.TotalValue.ToModifierString();
            CombatManeuverBonus = character.Offense.CombatManeuverBonus().ToModifierString();
            CombatManeuverDefense = character.Offense.CombatManeuverDefense().ToModifierString();

            Feats = character.Feats.Select(x => x.Name).ToArray();
            CapableSkills = character.SkillRanks.GetRankedSkills().Select(x => x.ToString()).ToArray();
            AllSkills = character.SkillRanks.GetSkills().Select(x => x.ToString()).ToArray();
            
            Languages = character.Languages.Select(x => x.Name).ToArray();
            Gear = character.Inventory.ToStringArray();

            Father = character.History.FamilyTree.Father;
            Mother = character.History.FamilyTree.Mother;
            ClassOrigin = character.History.ClassOriginStory.Name;
            Drawback = character.History.Drawback.Name;
            Homeland = character.History.Homeland.Location;
            if(character.PersonalityType.Descriptors.HasChoices())
                Personality = character.PersonalityType.Descriptors.Choose(3).ToArray();
            if(character.PersonalityType.Weaknesses.HasChoices())
                PersonalityWeaknesses = character.PersonalityType.Weaknesses.Choose(3).ToArray();
            IdealName = character.Ideal.Name;
            IdealDescription = character.Ideal.Description;
            HairColor = character.Appearance.HairColor.ToString();
            HairStyle = character.Appearance.HairStyle.ToString();
            FacialHair = character.Appearance.FacialHair.ToString();
            EyeColor = character.Appearance.EyeColor.ToString();
            Age = character.Age.ToString();
            Height = character.Size.Height.ToInchesAndFeet();
            Weight = character.Size.Weight.ToPoundsString();
            Money = character.Inventory.CoinPurse.ToString();
        }

        public string Name { get; private set; }
        public string Gender { get; private set; }
        public string Race { get; private set; }
        public string Class { get; private set; }
        public string Level { get; private set; }
        public string Size { get; private set; }
        public string Alignment { get; private set; }
        public string Initiative { get; private set; }
        public string Senses { get; private set; }
        public string Perception { get; private set; }
        public string ArmorClass { get; private set; }
        public string TouchArmorClass { get; private set; }
        public string FlatFootedArmorClass { get; private set; }
        public string HitPoints { get; private set; }
        public string FortitudeSave { get; private set; }
        public string WillSave { get; private set; }
        public string ReflexSave { get; private set; }
        public string MovementSpeed { get; private set; }
        public string MovementSquares { get; private set; }
        public string[] AttackTypes { get; private set; }
        public string[] Attacks { get; private set; }
        public string[] SpecialAttacks { get; private set; }
        public string Strength { get; private set; }
        public string StrengthModifier { get; private set; }
        public string Dexterity { get; private set; }
        public string DexterityModifier { get; private set; }
        public string Constitution { get; private set; }
        public string ConstitutionModifier { get; private set; }
        public string Intelligence { get; private set; }
        public string IntelligenceModifier { get; private set; }
        public string Wisdom { get; private set; }
        public string WisdomModifier { get; private set; }
        public string Charisma { get; private set; }
        public string CharismaModifier { get; private set; }
        public string BaseAttackBonus { get; private set; }
        public string CombatManeuverBonus { get; private set; }
        public string CombatManeuverDefense { get; private set; }

        public string[] Feats { get; private set; }
        public string[] CapableSkills { get; private set; }
        public string[] AllSkills { get; private set; }
        public string[] Languages { get; private set; }

        public string[] Gear { get; private set; }

        public string Father { get; private set; }
        public string Mother { get; private set; }

        public string ClassOrigin { get; private set; }
        public string Drawback { get; private set; }
        public string Homeland { get; private set; }

        public string[] Personality { get; private set; }

        public string[] PersonalityWeaknesses { get; private set; }

        public string IdealName { get; private set; }
        public string IdealDescription { get; private set; }

        public string HairColor { get; private set; }
        public string HairStyle { get; private set; }

        public string FacialHair { get; private set; }
        public string EyeColor { get; private set; }

        public string Age { get; private set; }
        public string Height { get; private set; }
        public string Weight { get; private set; }

        public string Money { get; private set; }

        public string Pronoun { get; private set; }
        public string PossessivePronoun { get; private set; }

        private string FormatSenses(CharacterSheet character)
        {
            string senses = string.Join(
                ", ",
                character.SpecialQualities.SightAbilities.Select(x => x.Condition).ToArray());
            return senses;
        }
    }

    
}
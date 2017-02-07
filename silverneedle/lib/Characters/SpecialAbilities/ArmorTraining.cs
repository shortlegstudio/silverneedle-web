namespace SilverNeedle.Characters
{
    using SilverNeedle.Equipment;

    /// <summary>
    /// Provides the ability to move at full speed with heavier armors and 
    /// reduces the maximum dex penalty of armor
    /// </summary>
    public class ArmorTraining : SpecialAbility
    {
        public int ArmorTrainingLevel { get; private set; }

        public ArmorTraining(int level)
        {
            this.Type = "Armor Training";
            ArmorTrainingLevel = level;            
        }

        public int GetMaximumDexterityBonus(Armor armor)
        {
            return armor.MaximumDexterityBonus + ArmorTrainingLevel;
        }

        public int GetArmorCheckPenalty(Armor armor)
        {
            return (armor.ArmorCheckPenalty - ArmorTrainingLevel).AtLeast(0);
        }

        public int GetMovementSpeed(int baseSpeed, Armor armor)
        {
            if(ArmorTrainingLevel >= 1 && armor.ArmorType == ArmorType.Medium) {
                return baseSpeed;
            }

            if(ArmorTrainingLevel >= 2 && armor.ArmorType == ArmorType.Heavy) {
                return baseSpeed;
            }

            return baseSpeed - armor.MovementSpeedPenalty;
        }
    }    
}
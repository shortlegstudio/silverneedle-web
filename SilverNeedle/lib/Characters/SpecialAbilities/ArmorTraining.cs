namespace SilverNeedle.Characters
{
    using SilverNeedle.Equipment;
    using SilverNeedle.Serialization;

    /// <summary>
    /// Provides the ability to move at full speed with heavier armors and 
    /// reduces the maximum dex penalty of armor
    /// </summary>
    public class ArmorTraining : LevelAbility
    {
        public ArmorTraining()
        {
        }

        public ArmorTraining(IObjectStore data) : base(data)
        {
            Level = data.GetInteger("level");
        }

        public int GetMaximumDexterityBonus(Armor armor)
        {
            return armor.MaximumDexterityBonus + Level;
        }

        public int GetArmorCheckPenalty(Armor armor)
        {
            return (armor.ArmorCheckPenalty - Level).AtLeast(0);
        }

        public int GetMovementSpeed(int baseSpeed, Armor armor)
        {
            if(Level >= 1 && armor.ArmorType == ArmorType.Medium) {
                return baseSpeed;
            }

            if(Level >= 2 && armor.ArmorType == ArmorType.Heavy) {
                return baseSpeed;
            }

            return baseSpeed - armor.MovementSpeedPenalty;
        }
    }    
}
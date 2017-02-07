namespace SilverNeedle.Characters
{
    /// <summary>
    /// Represents the ability to allocate ability points to an attribute of 
    /// the character's choosing. 
    /// </summary>
    public class AbilityScoreToken
    {
        public AbilityScoreAdjustment Modifier { get; private set; }
        public AbilityScoreToken(AbilityScoreAdjustment modifier) 
        {
            Modifier = modifier;
        }
    }
}
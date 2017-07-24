// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.SpecialAbilities
{
    public class SmiteEvil : SpecialAbility
    {
        public int UsesPerDay { get; private set; }

        public SmiteEvil(int uses)
        {
            this.UsesPerDay = uses;
            SetName();
        }

        public void UpdateUsesPerDay(int newAmount)
        {
            this.UsesPerDay = newAmount;
            this.SetName();
        }

        private void SetName()
        {
            this.Name = string.Format("Smite Evil {0}/day", UsesPerDay);
        }
    }
}
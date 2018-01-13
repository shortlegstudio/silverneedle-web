// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters.Attacks
{
    public interface IAttack
    {
        string Name { get; }
        Dice.Cup Damage { get; } 
        AttackTypes AttackType { get; }
        string DisplayString();
    }
}
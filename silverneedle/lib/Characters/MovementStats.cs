// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Characters
{
    using System;
    using System.Collections.Generic;
    using SilverNeedle.Utility;

    /// <summary>
    /// Handles movement stats like how to work with armor or flying etc...
    /// </summary>
    public class MovementStats : IComponent
    {
        public ComponentContainer Parent { get; set; }
        private const int SquareSize = 5;

        public void Initialize(ComponentContainer components) { }

        public IValueStatistic BaseMovement { get { return Parent.FindStat<IValueStatistic>(StatNames.BaseMovementSpeed); } }
        public IValueStatistic ArmorMovementPenalty { get { return Parent.FindStat<IValueStatistic>(StatNames.ArmorMovementPenalty); } }
        public int MovementSpeed { get { return BaseMovement.TotalValue; } }
        public bool UseBase30MoveSpeed { get { return Parent.FindStat<BasicStat>(StatNames.BaseMovementSpeed).BaseValue == 30; } }
        public int BaseSquares
        { 
            get { return this.BaseMovement.TotalValue / SquareSize; }
        }

        public void SetBaseSpeed(int speed)
        {
            Parent.FindStat<BasicStat>(StatNames.BaseMovementSpeed).SetValue(speed);
        }
    }
}
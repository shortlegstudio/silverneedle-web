// //-----------------------------------------------------------------------
// // <copyright file="RaceMaturityYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle;
using System.Collections.Generic;
using SilverNeedle.Dice;
using System.Linq;




namespace SilverNeedle.Characters
{
	public interface IRaceMaturityGateway
	{
        Maturity Get(Race race);
        IEnumerable<Maturity> All();
	}

}


// //-----------------------------------------------------------------------
// // <copyright file="NameCharacter.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using SilverNeedle.Names.Gateways;
using SilverNeedle.Characters;

namespace SilverNeedle.Actions.NamingThings
{
	using System;

	public interface INameCharacter
	{
        string CreateFullName(Gender gender, string race);
	}

}


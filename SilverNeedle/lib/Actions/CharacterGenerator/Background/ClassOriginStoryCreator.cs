﻿// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginStoryCreator.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using SilverNeedle.Characters.Background;

namespace SilverNeedle.Actions.CharacterGenerator.Background
{
    public class ClassOriginStoryCreator
    {
        IClassOriginGateway classOrigins;

        public ClassOriginStoryCreator(IClassOriginGateway classOrigins)
        {
            this.classOrigins = classOrigins;
        }

        public ClassOrigin CreateStory(string cls)
        {
            return classOrigins.ChooseOne(cls);
        }
    }
}


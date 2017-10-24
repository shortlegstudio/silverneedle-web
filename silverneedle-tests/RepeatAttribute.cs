// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Sdk;
using Xunit.Extensions;
using Xunit.Abstractions;
using System.Reflection;
using System.Linq;

public class RepeatAttribute : DataAttribute
{
    readonly int _count;

    public RepeatAttribute(int count)
    {
        _count = count;
    }

    public override IEnumerable<object[]> GetData(MethodInfo testMethod)
    {
        return Enumerable.Repeat(new object[0], _count);
    }
}
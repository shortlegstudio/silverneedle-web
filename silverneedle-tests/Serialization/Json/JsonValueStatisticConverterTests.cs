// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace Tests.Serialization
{
    using Xunit;
    using SilverNeedle;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;
    using Newtonsoft.Json;

    public class JsonValueStatisticConverterTests
    {
        [Fact]
        public void WritesOutTheValueAndConditionalValuesForAStatistic()
        {
            var aStat = new BasicStat("Stat Name", 10);
            var mod = new ValueStatModifier("Stat Name", 3, "foobar");
            aStat.AddModifier(mod);

            var json = JsonConvert.SerializeObject(aStat, Formatting.Indented, new JsonValueStatisticConverter() );
            Assert.Contains("Stat Name", json);
            Assert.Contains("13", json);

        }
    }
}
// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Treasure
{
    using System;
    using System.Text.RegularExpressions;
    using System.Linq;

    public static class TreasureExtensions
    {
        public static CoinsBase ToCoins(this string coinString)
        {
            if(string.IsNullOrEmpty(coinString))
            {
                ShortLog.Warn("Cannot parse value. CoinString was null or empty");
                return new CopperPieces();
            }

            string[] output = Regex.Matches(coinString, "[0-9]+|[A-Za-z]+")
                .Cast<Match>()
                .Select(match => match.Value)
                .ToArray();

            if (output.Length != 2)
                throw new ArgumentException();
            
            int value = int.Parse(output[0]);
            switch(output[1])
            {
                case PlatinumPieces.ABBR:
                    return new PlatinumPieces(value);
                case GoldPieces.ABBR:
                    return new GoldPieces(value);
                case SilverPieces.ABBR:
                    return new SilverPieces(value);
                case CopperPieces.ABBR:
                    return new CopperPieces(value);
            }

            return null;
        }

        public static int ToCoinValue(this string coinString)
        {
            return coinString.ToCoins().Value;
        }
    }
}
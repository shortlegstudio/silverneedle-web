// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT


namespace SilverNeedle.Treasure
{ 
    public class CopperPieces : CoinsBase
    {
        private const int COPPER_VALUE = 1; 
        public CopperPieces() : base(COPPER_VALUE)
        {

        }

        public CopperPieces(int pieces) : base(pieces, COPPER_VALUE)
        {

        }
    }
}
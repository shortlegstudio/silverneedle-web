// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Treasure
{
    public class CoinPurse
    {
        public int Value { get { return TotalValue(); } }
        public PlatinumPieces Platinum { get; private set; }
        public GoldPieces Gold { get; private set; }
        public SilverPieces Silver { get; private set; }
        public CopperPieces Copper { get; private set; }

        public CoinPurse()
        {
            Platinum = new PlatinumPieces();
            Gold = new GoldPieces();
            Silver = new SilverPieces();
            Copper = new CopperPieces();
        }

        public void AddPlatinum(int pieces)
        {
            Platinum.Pieces += pieces;
        }

        public void AddGold(int pieces)
        {
            Gold.Pieces += pieces;
        }

        public void AddSilver(int pieces)
        {
            Silver.Pieces += pieces;
        }

        public void AddCopper(int pieces)
        {
            Copper.Pieces += pieces;
        }

        public void SetValue(int value)
        {
            var gp = new GoldPieces(value / Gold.CoinValue);
            var sp = new SilverPieces((value - gp.Value) / Silver.CoinValue);
            var cp = new CopperPieces((value - gp.Value - sp.Value) / Copper.CoinValue);
            AddGold(gp.Pieces);
            AddSilver(sp.Pieces);
            AddCopper(cp.Pieces);
        }

        public override string ToString()
        {
            return string.Format("pp: {0} gp: {1} sp: {2} cp: {3}", Platinum.Pieces, Gold.Pieces, Silver.Pieces, Copper.Pieces);
        }

        private int TotalValue()
        {
            return Platinum.Value +
                Gold.Value +
                Silver.Value + 
                Copper.Value;
        }
    }
}
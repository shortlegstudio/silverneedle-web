// Copyright (c) 2018 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Names
{
    using System.Collections.Generic;
    using System.Linq;
    public class MarkovNameGenerator
    {
        public const string TERMINATOR = "&&terminate&&";
        public IEnumerable<MarkovState> States { get { return states.Values; } }
        public int Order { get; private set; }
        private Dictionary<string, MarkovState> states = new Dictionary<string, MarkovState>();

        public MarkovNameGenerator(IEnumerable<string> names, int order)
        {
            this.Order = order;
            GenerateChains(names);
        }

        private void GenerateChains(IEnumerable<string> names)
        {
            foreach(var n in names)
            {
                var key = InitialKey();
                foreach(char c in n.ToLower().ToCharArray())
                {
                    var token = c.ToString();
                    IncrementChain(key, token);
                    key = NextKey(key, token);
                }
                IncrementChain(key, TERMINATOR);
            }
        }

        private void IncrementChain(string key, string token)
        {
            if(!states.ContainsKey(key))
            {
                states.Add(key, new MarkovState(key));
            }

            states[key].IncrementToken(token);
        }

        private string NextKey(string key, string newToken)
        {
            return key.Substring(1) + newToken;
        }

        private string InitialKey()
        {
            return new System.String('_', Order);
        }

        public string Generate(int maxLength)
        {
            var name = "";
            var currentKey = InitialKey();
            while(name.Length < maxLength)
            {
                if(!states.ContainsKey(currentKey))
                    break;
                
                var state = states[currentKey];
                if(state.Tokens.Empty())
                    break;

                var next = state.ChooseOne();
                if(next == TERMINATOR)
                    break;

                name += next;
                currentKey = NextKey(currentKey, next);
            }
            ShortLog.DebugFormat("Markov Name Generator: {0}", name);
            return name.Titlize();
        }




        public class MarkovState
        {
            public string Key { get; private set; }
            public Dictionary<string, float> Tokens = new Dictionary<string, float>();

            public void IncrementToken(string token)
            {
                if(!Tokens.ContainsKey(token))
                {
                    Tokens.Add(token, 1);
                }
                else
                {
                    Tokens[token]++;
                }
            }

            public MarkovState(string key)
            {
                this.Key = key;
            }

            public string ChooseOne()
            {
                var max = Tokens.Values.Sum();
                var v = Randomly.Range(0, max);
                var idx = 0f;
                foreach(var t in Tokens)
                {
                    idx += t.Value;
                    if(v < idx)
                        return t.Key;
                }

                return Tokens.Last().Key;
            }
        }
    }
}
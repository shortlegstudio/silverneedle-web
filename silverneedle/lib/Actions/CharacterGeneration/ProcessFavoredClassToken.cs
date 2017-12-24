// Copyright (c) 2017 Trevor Redfern
// 
// This software is released under the MIT License.
// https://opensource.org/licenses/MIT

namespace SilverNeedle.Actions.CharacterGeneration
{
    using System.Linq;
    using SilverNeedle.Characters;
    using SilverNeedle.Serialization;

    public class ProcessFavoredClassToken : TokenProcessor<FavoredClassToken>
    {
        private EntityGateway<Class> classes;

        public ProcessFavoredClassToken() : this(GatewayProvider.Get<Class>())
        {
        }

        public ProcessFavoredClassToken(EntityGateway<Class> classGateway)
        {
            this.classes = classGateway;
        }

        protected override void ProcessToken(CharacterSheet character, FavoredClassToken token)
        {
            var chosen = character.GetAll<FavoredClass>();
            if(!chosen.Any(x => x.Qualifies(character.Class)))
            {
                character.Add(new FavoredClass(character.Class.Name));
            }
            else
            {
                var options = this.classes.Where(cls => !chosen.Any(fav => fav.Qualifies(cls)));
                var selected = options.ChooseOne();
                character.Add(new FavoredClass(selected.Name));
            }
        }
    }
}
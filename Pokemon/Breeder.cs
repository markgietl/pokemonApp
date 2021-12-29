using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApp
{
    public class Breeder : Person
    {
        private List<Pokemon> pokemonList;
        public Breeder(string name, int age, string hometown) 
        { 
            this.name = name;
            this.age = age; 
            this.hometown = hometown;
            pokemonList = new List<Pokemon>(); 
        } 
        public void AddPokemon(Pokemon pokemon)
        {
            pokemonList.Add(pokemon);
        }
        public string GetName()
        {
            return this.name;
        }
        public int GetAge()
        {
            return this.age;
        }
        public string GetHometown()
        {
            return this.hometown;
        }

    }


}

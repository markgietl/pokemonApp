using System;
using System.Collections.Generic;

namespace PokemonApp
{
	public class Trainer : Person
	{
		private List<Pokemon> pokemonList;
		public Trainer(string name, int age, string hometown)
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

using System;

namespace PokemonApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PokemonCenter paletTown = new PokemonCenter("Palet Town");
            paletTown.AddPokemon("Charmander", PokemonType.Fire);
            paletTown.PrintPokemonList();
        }
    }
}

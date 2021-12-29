using System;

namespace PokemonApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            PokemonCenter paletTown = new PokemonCenter("Palet Town");
            paletTown.AddPokemon("Charmander", PokemonType.Fire);
            paletTown.AddTrainer("Mafyoo", 30, "Brakpan");
            paletTown.AddBreeder("Mark", 25, "Sandton");
            paletTown.PrintPokemonList();
            paletTown.PrintBreederList();
            paletTown.PrintTrainerList();
            
        }

    }
}

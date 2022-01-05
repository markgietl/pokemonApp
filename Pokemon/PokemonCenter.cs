using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonApp
{
    public class PokemonCenter
    {
        private List<Pokemon> pokemonList;
        private List<Trainer> trainerList;
        private List<Breeder> breederList;
        private string town;
        public PokemonCenter(string town)
        {
            this.town = town;
            pokemonList = new List<Pokemon>();
            trainerList = new List<Trainer>();  
            breederList = new List<Breeder>();
        }
        public PokemonCenter(string town, List<Pokemon> pokemonList, List<Trainer> trainerList, List<Breeder> breederList)
        {
            this.town = town;
            this.pokemonList = pokemonList;
            this.trainerList = trainerList;
            this.breederList = breederList;
        }
        public List<Trainer> GetTrainerList()
        {
            return trainerList;
        }
        public List<Pokemon> GetPokemonList()
        {
            return pokemonList;
        }
        public List<Breeder> GetBreederList()
        {
            return breederList;
        }
        public string GetTown()
        {
            return town;
        }
        public void AddPokemon(string name, PokemonType type)
        {
            Pokemon newPokemon = new Pokemon(name, type);
            pokemonList.Add(newPokemon);    
        }
        public void AddTrainer(string name, int age, string hometown)
        {
            Trainer newTrainer = new Trainer(name, age, hometown);
            trainerList.Add(newTrainer);
        }
        public void AddBreeder(string name, int age, string hometown)
        {
            Breeder newBreeder = new Breeder(name, age, hometown);
            breederList.Add(newBreeder);
        }
        public void PrintTrainerList()
        {
            List<Trainer> trainers = GetTrainerList();
            Console.WriteLine("List of trainers:");
            for(int index = 0; index < trainers.Count; index++)
            {
                Console.WriteLine($"{index + 1}. Name: {trainers[index].GetName()} Age: {trainers[index].GetAge()} Hometown: {trainers[index].GetHometown()} ");
                Console.WriteLine("Pokemon List:");
                foreach (Pokemon pokemon in trainers[index].GetPokemonList())
                {
                    Console.WriteLine($"Name:{pokemon.GetName()} Type: {pokemon.GetType()}");
                }

            }
        }
        public void PrintBreederList()
        {
            List<Breeder> breeders = GetBreederList();
            Console.WriteLine("List of breeders:");
            for (int index = 0; index < breeders.Count; index++)
            {
                Console.WriteLine($"{index + 1}. Name: {breeders[index].GetName()} Age: {breeders[index].GetAge()} Hometown: {breeders[index].GetHometown()} ");
                Console.WriteLine("Pokemon List:");
                foreach (Pokemon pokemon in breeders[index].GetPokemonList())
                {
                    Console.WriteLine($"Name:{pokemon.GetName()} Type: {pokemon.GetType()}");
                }
            }
        }
        public void PrintPokemonList()
        {
            List<Pokemon> pokemon = GetPokemonList();
            Console.WriteLine("List of pokemon:");
            for (int index = 0; index < pokemon.Count; index++)
            {
                Console.WriteLine($"{index + 1}. Name: {pokemon[index].GetName()} Type: {pokemon[index].GetType()}");
            }
        }
    }
}

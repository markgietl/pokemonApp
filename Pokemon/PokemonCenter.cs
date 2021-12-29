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
        private List<Trainer> GetTrainerList()
        {
            return trainerList;
        }
        private List<Pokemon> GetPokemonList()
        {
            return pokemonList;
        }
        private List<Breeder> GetBreederList()
        {
            return breederList;
        }
        public void AddPokemon(string name, PokemonType type)
        {
            Pokemon newPokemon = new Pokemon(name, type);
            pokemonList.Add(newPokemon);    
        }
        public void addTrainer(string name, int age, string hometown)
        {
            Trainer newTrainer = new Trainer(name, age, hometown);
            trainerList.Add(newTrainer);
        }
        public void addBreeder(string name, int age, string hometown)
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
                Console.WriteLine($"{index + 1}. {trainers[index]}");
            }
        }
        public void PrintBreederList()
        {
            List<Breeder> breeders = GetBreederList();
            Console.WriteLine("List of breeders:");
            for (int index = 0; index < breeders.Count; index++)
            {
                Console.WriteLine($"{index + 1}. {breeders[index]}");
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

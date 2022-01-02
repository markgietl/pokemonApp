using System;
using System.Collections.Generic;

namespace PokemonApp
{
    internal class Program
    {
        static List<PokemonCenter> pokemonCenterList = new List<PokemonCenter>();
        static string town = null;
        static List<Pokemon> pokemonList = new List<Pokemon>();
        static List<Trainer> trainerList = new List<Trainer>();
        static List<Breeder> breederList = new List<Breeder>();
        static void Main(string[] args)
        {
            LoadData(@"C:\Users\Mark\Documents\PokemonAppData.txt");
            pokemonCenterList[0].PrintBreederList();

        }

        private static void LoadData(string dir)
        {


            string[] lines = System.IO.File.ReadAllLines(dir);
            foreach (string line in lines)
            {
                string[] pokemonCenterData = line.Split(",");
                foreach (string data in pokemonCenterData)
                {
                    //Console.WriteLine(data + "\n");
                    LoadPokemonCenterTown(data);
                    LoadPokemonCenterPokemonList(data, pokemonList);
                    LoadPokemonCenterTrainerList(data, trainerList);
                    LoadPokemonCenterBreederList(data, breederList);


                }

                PokemonCenter center = new PokemonCenter(town, pokemonList, trainerList, breederList);
                pokemonCenterList.Add(center);
            }

        }
        private static void LoadPokemonCenterPokemonList(string data, List<Pokemon> pokemonList)
        {
            if (data.Contains("pokemonCenterPokemonList:"))
            {
                string[] arr = data.Split(" ");
                for (int i = 1; i < arr.Length; i++)
                {
                    string pokemonName = arr[i].Split(".")[0].Split(":")[1];
                    PokemonType pokemonType = (PokemonType)Int32.Parse(arr[i].Split(".")[1].Split(":")[1]);
                    Pokemon pokemon = new Pokemon(pokemonName, pokemonType);
                    pokemonList.Add(pokemon);

                }
            }
        }
        private static void LoadPokemonCenterTrainerList(string data, List<Trainer> trainerList)
        {
            List<Pokemon> trainerPokemonList = new List<Pokemon>();
            if (data.Contains("trainerList"))
            {
                //Console.WriteLine(data);
                string[] arr = data.Split("trainerList:")[1].Split("/");
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Contains("."))
                    {
                        string trainerName = arr[i].Split(".")[0].Split(":")[1];
                        int age = Int32.Parse(arr[i].Split(".")[1].Split(":")[1]);
                        string town = arr[i].Split(".")[2].Split(":")[1];
                        string[] pokemonList = arr[i].Split(".")[3].Split(":")[1].Split("$");
                        foreach (string pokemon in pokemonList)
                        {
                            //Console.WriteLine(pokemon);
                            if (pokemon.Contains("-"))
                            {
                                string pokemonName = pokemon.Split(" ")[0].Split("-")[1];
                                PokemonType pokemonType = (PokemonType)Int32.Parse(pokemon.Split(" ")[1].Split("-")[1]);
                                //Console.WriteLine(pokemonType);
                                Pokemon newPokemon = new Pokemon(pokemonName, pokemonType);
                                trainerPokemonList.Add(newPokemon);
                            }
                            
                            
                        }
                        Trainer trainer = new Trainer(trainerName, age, town,trainerPokemonList);
                        trainerList.Add(trainer);


                    }

                    //Console.WriteLine(arr[i]);
                }
            }
        }
        private static void LoadPokemonCenterBreederList(string data, List<Breeder> breederList)
        {
            List<Pokemon> breederPokemonList = new List<Pokemon>();
            if (data.Contains("breederList"))
            {
                //Console.WriteLine(data);
                string[] arr = data.Split("breederList:")[1].Split("/");
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Contains("."))
                    {
                        string breederName = arr[i].Split(".")[0].Split(":")[1];
                        int age = Int32.Parse(arr[i].Split(".")[1].Split(":")[1]);
                        string town = arr[i].Split(".")[2].Split(":")[1];
                        string[] pokemonList = arr[i].Split(".")[3].Split(":")[1].Split("$");
                        foreach (string pokemon in pokemonList)
                        {
                            //Console.WriteLine(pokemon);
                            if (pokemon.Contains("-"))
                            {
                                string pokemonName = pokemon.Split(" ")[0].Split("-")[1];
                                PokemonType pokemonType = (PokemonType)Int32.Parse(pokemon.Split(" ")[1].Split("-")[1]);
                                //Console.WriteLine(pokemonType);
                                Pokemon newPokemon = new Pokemon(pokemonName, pokemonType);
                                breederPokemonList.Add(newPokemon);
                            }


                        }
                        Breeder breeder = new Breeder(breederName, age, town, breederPokemonList);
                        breederList.Add(breeder);


                    }
                    //Console.WriteLine(arr[i]);
                }
            }
        }
        private static void LoadPokemonCenterTown(string data)
        {
            if (data.Contains("town:"))
            {
                town = data.Split(":")[1];
                //Console.WriteLine(town);
            }

        }
    }
}
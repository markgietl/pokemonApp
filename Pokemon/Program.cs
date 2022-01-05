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
            string filePath = @"C:\Users\Mark\Documents\PokemonAppData.txt";
            LoadData(filePath);
            Console.WriteLine("List of Pokemon centers:");
            for (int index = 0;index < pokemonCenterList.Count;index++)
            {
                Console.WriteLine($"{index+1}. {pokemonCenterList[index].GetTown()}");
            }
            // NEED TO CHECK IF VALUE SUPPLIED BY USER IS AN INT AND IS IN RANGE
            int inputPokemonCenter = Int32.Parse(Console.ReadLine())-1;
            PokemonCenter center = pokemonCenterList[inputPokemonCenter];
            Console.WriteLine($"Welcome to {center.GetTown()} pokemon center administration panel. What action would you like to perform?");
            Console.WriteLine("1. Add trainer");
            Console.WriteLine("2. Add breeder");
            Console.WriteLine("Press any other key to exit.");
            
            string input = Console.ReadLine();
            while (input == "1" || input == "2")
            { 
                switch (input)
                {
                    
                }
            }
            pokemonCenterList[0].PrintBreederList();
            Save(filePath);
        }

        private static void LoadData(string dir)
        {
            // TODO: check to see if file exists and is not empty, if it does, read. Else create file
            string[] lines = System.IO.File.ReadAllLines(dir);
            if (lines.Length > 0)
            {
                foreach (string line in lines)
                {
                    if (line.Contains(","))
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
            }

        }
        private static void Save(string dir)
        {
            
            foreach (PokemonCenter pokemonCenter in pokemonCenterList)
            {
                string pokemonCenterTown = $"town:{pokemonCenter.GetTown()}";
                string pokemonCenterPokemonList = "pokemonCenterPokemonList: ";
                string pokemonCenterBreederList = "breederList:";
                string pokemonCenterTrainerList = "trainerList:";


               
               
                //Getting pokemon center's pokemon list
                foreach (Pokemon pokemon in pokemonCenter.GetPokemonList())
                {
                    string pokemonName = pokemon.GetName();
                    string pokemonType = ((int)pokemon.GetType()).ToString();
                    pokemonCenterPokemonList += $"name:{pokemonName}.PokemonType:{pokemonType}";
                }
               
                
                //Getting pokemon center's breeder list
                foreach (Breeder breeder in pokemonCenter.GetBreederList())
                {
                    string breederName = breeder.GetName();
                    int breederAge = breeder.GetAge();
                    string breederHomeTown = breeder.GetHometown();
                    string pokemonName = null;
                    string pokemonType = null;
                        foreach (Pokemon pokemon in breeder.GetPokemonList())
                        {
                            pokemonName = pokemon.GetName();
                            pokemonType = ((int)pokemon.GetType()).ToString();
                        }
                    pokemonCenterBreederList += $"name:{breederName}.age:{breederAge}.home:{breederHomeTown}.breederPokemonList:name-{pokemonName} PokemonType-{pokemonType}$";
                }
               
                
                // Getting pokemon center's trainer list
                foreach (Trainer trainer in pokemonCenter.GetTrainerList())
                {
                // TODO : yes
                }
                string pokemonCenterLine = $"{pokemonCenterTown},{pokemonCenterPokemonList},{pokemonCenterTrainerList},{pokemonCenterBreederList}";
                Console.WriteLine(pokemonCenterLine);
                System.IO.File.WriteAllText(dir, pokemonCenterLine);
            }
        }
        private static void LoadPokemonCenterPokemonList(string data, List<Pokemon> pokemonList)
        {
            if (data.Contains("pokemonCenterPokemonList:"))
            {
                string[] arr = data.Split(" ");
                for (int i = 1; i < arr.Length; i++)
                {
                    if (arr[i].Contains("."))
                    {
                        string pokemonName = arr[i].Split(".")[0].Split(":")[1];
                        PokemonType pokemonType = (PokemonType)Int32.Parse(arr[i].Split(".")[1].Split(":")[1]);
                        Pokemon pokemon = new Pokemon(pokemonName, pokemonType);
                        pokemonList.Add(pokemon);
                    }

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
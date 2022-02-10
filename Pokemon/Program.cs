using System;
using System.Collections.Generic;

namespace PokemonApp
{
    internal class Program
    {
        private static List<PokemonCenter> pokemonCenterList = new List<PokemonCenter>();
        private static string town = null;
        private static List<Pokemon> pokemonList = new List<Pokemon>();
        private static List<Trainer> trainerList = new List<Trainer>();
        private static List<Breeder> breederList = new List<Breeder>();

        private static void Main(string[] args)
        {
            string filePath = @"..\..\..\..\..\..\PokemonAppData.txt";
            LoadData(filePath);

            Console.WriteLine("Welcome. Please choose a center from the list or add a new center");
            for (int i = 0; i < pokemonCenterList.Count; i++)
            {
                Console.WriteLine($"{i + 1}.{pokemonCenterList[i].GetTown()}");
            };
            Console.WriteLine($"{pokemonCenterList.Count + 1} Press x to add a new Pokemon center to the database");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "x":
                    {
                        addPokemonCenter();
                        break;
                    }
                default:
                    {
                        int index = Int32.Parse(choice);
                        PokemonCenter center = pokemonCenterList[index - 1];
                        Console.WriteLine($"Welcome to {center.GetTown()} pokemon center administration panel. What action would you like to perform?");
                        Console.WriteLine("1. Add trainer");
                        Console.WriteLine("2. Add breeder");
                        Console.WriteLine("Press any other key to exit.");

                        string input = Console.ReadLine();
                        do
                        {
                            switch (input)
                            {
                                case "1":
                                    addTrainer();
                                    break;

                                case "2":
                                    addBreeder();
                                    break;
                            }
                            Console.WriteLine("Would you like to add another trainer or breeder?");
                            Console.WriteLine("1. Add trainer");
                            Console.WriteLine("2. Add breeder");
                            Console.WriteLine("Press any other key to exit.");

                            input = Console.ReadLine();
                        } while (input == "1" || input == "2");
                        break;
                    }
            }

            Save(filePath);
        }

        private static void addTrainer()
        {
            string trainerName;
            string trainerHomeTown;
            int trainerAge;
            List<Pokemon> trainerPokemonList = new List<Pokemon>();
            Console.WriteLine("Please enter the new trainer's name");
            trainerName = Console.ReadLine();
            Console.WriteLine("Please enter the new trainer's home town");
            trainerHomeTown = Console.ReadLine();
            Console.WriteLine("Please enter the new trainer's age");
            trainerAge = Int32.Parse(Console.ReadLine());
            addPokemonToTrainer(trainerPokemonList);

            Trainer newTrainer = new Trainer(trainerName, trainerAge, trainerHomeTown, trainerPokemonList);
            trainerList.Add(newTrainer);
        }

        private static void addPokemonToTrainer(List<Pokemon> trainerPokemonList)
        {
            Console.WriteLine("Do you want to add a pokemon for this trainer? Y/N");
            string input2 = Console.ReadLine();
            do
            {
                string pokemonName;
                PokemonType pokemonType;
                Console.WriteLine("Please enter pokemon name");
                pokemonName = Console.ReadLine();
                Console.WriteLine("Please enter pokemon type");
                Console.WriteLine("1. Fire");
                Console.WriteLine("2. Grass");
                Console.WriteLine("3. Water");
                Console.WriteLine("4. Flying");
                Console.WriteLine("5. Dragon");
                Console.WriteLine("6. Steel");
                Console.WriteLine("7. Rock");
                Console.WriteLine("8. Electric");
                Console.WriteLine("9. Poison");
                pokemonType = (PokemonType)(Int32.Parse(Console.ReadLine()) - 1);

                while (pokemonType < (PokemonType)0 || pokemonType > (PokemonType)9)
                {
                    Console.WriteLine("Invalid entry. Please enter pokemon type");
                    Console.WriteLine("1. Fire");
                    Console.WriteLine("2. Grass");
                    Console.WriteLine("3. Water");
                    Console.WriteLine("4. Flying");
                    Console.WriteLine("5. Dragon");
                    Console.WriteLine("6. Steel");
                    Console.WriteLine("7. Rock");
                    Console.WriteLine("8. Electric");
                    Console.WriteLine("9. Poison");
                    pokemonType = (PokemonType)(Int32.Parse(Console.ReadLine()) - 1);
                }
                Pokemon newPokemon = new Pokemon(pokemonName, pokemonType);
                trainerPokemonList.Add(newPokemon);
                Console.WriteLine("Do you want to add another pokemon for this trainer? Y/N");
                input2 = Console.ReadLine();
            } while (input2.ToLower() == "y");
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
            using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(System.IO.Path.Combine(@"..\..\..\..\..\", "PokemonAppData.txt"), false))
            {
                outputFile.WriteLine("");
            }
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
                    string trainerName = trainer.GetName();
                    int trainerAge = trainer.GetAge();
                    string trainerHomeTown = trainer.GetHometown();
                    string pokemonName = null;
                    string pokemonType = null;
                    foreach (Pokemon pokemon in trainer.GetPokemonList())
                    {
                        pokemonName = pokemon.GetName();
                        pokemonType = ((int)pokemon.GetType()).ToString();
                    }
                    pokemonCenterTrainerList += $"name:{trainerName}.age:{trainerAge}.home:{trainerHomeTown}.breederPokemonList:name-{pokemonName} PokemonType-{pokemonType}$";
                }
                string pokemonCenterLine = $"{pokemonCenterTown},{pokemonCenterPokemonList},{pokemonCenterTrainerList},{pokemonCenterBreederList}";
                Console.WriteLine(pokemonCenterLine);

                using (System.IO.StreamWriter outputFile = new System.IO.StreamWriter(System.IO.Path.Combine(@"../", "PokemonAppData.txt"), true))
                {
                    outputFile.WriteLine(pokemonCenterLine + "\n");
                }
                // System.IO.File.AppendText(dir);
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
                        Trainer trainer = new Trainer(trainerName, age, town, trainerPokemonList);
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
            }
        }

        private static void addBreeder()
        {
            string breederName;
            string breederHomeTown;
            int breederAge;
            List<Pokemon> breederPokemonList = new List<Pokemon>();
            Console.WriteLine("Please enter the new breeder's name");
            breederName = Console.ReadLine();
            Console.WriteLine("Please enter the new breeder's home town");
            breederHomeTown = Console.ReadLine();
            Console.WriteLine("Please enter the new breeder's age");
            breederAge = Int32.Parse(Console.ReadLine());
            addPokemonToBreeder(breederPokemonList);

            Breeder newBreeder = new Breeder(breederName, breederAge, breederHomeTown, breederPokemonList);
            breederList.Add(newBreeder);
        }

        private static void addPokemonToBreeder(List<Pokemon> breederPokemonList)
        {
            Console.WriteLine("Do you want to add a pokemon for this breeder? Y/N");
            string input2 = Console.ReadLine();
            do
            {
                string pokemonName;
                PokemonType pokemonType;
                Console.WriteLine("Please enter pokemon name");
                pokemonName = Console.ReadLine();
                Console.WriteLine("Please enter pokemon type");
                Console.WriteLine("1. Fire");
                Console.WriteLine("2. Grass");
                Console.WriteLine("3. Water");
                Console.WriteLine("4. Flying");
                Console.WriteLine("5. Dragon");
                Console.WriteLine("6. Steel");
                Console.WriteLine("7. Rock");
                Console.WriteLine("8. Electric");
                Console.WriteLine("9. Poison");
                pokemonType = (PokemonType)(Int32.Parse(Console.ReadLine()) - 1);

                while (pokemonType < (PokemonType)0 || pokemonType > (PokemonType)9)
                {
                    Console.WriteLine("Invalid entry. Please enter pokemon type");
                    Console.WriteLine("1. Fire");
                    Console.WriteLine("2. Grass");
                    Console.WriteLine("3. Water");
                    Console.WriteLine("4. Flying");
                    Console.WriteLine("5. Dragon");
                    Console.WriteLine("6. Steel");
                    Console.WriteLine("7. Rock");
                    Console.WriteLine("8. Electric");
                    Console.WriteLine("9. Poison");
                    pokemonType = (PokemonType)(Int32.Parse(Console.ReadLine()) - 1);
                }
                Pokemon newPokemon = new Pokemon(pokemonName, pokemonType);
                breederPokemonList.Add(newPokemon);
                Console.WriteLine("Do you want to add another pokemon for this breeder? Y/N");
                input2 = Console.ReadLine();
            } while (input2.ToLower() == "y");
        }

        private static void addPokemonCenter()
        {
            string town;
            Console.WriteLine("Please enter the Pokemon Center's town name");
            town = Console.ReadLine();
            PokemonCenter newPokemonCenter = new PokemonCenter(town);
            pokemonCenterList.Add(newPokemonCenter);
        }
    }
}
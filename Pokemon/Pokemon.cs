namespace PokemonApp
{
    public class Pokemon
    {
        private string name;
        private PokemonType type;

        public Pokemon(string name, PokemonType type)
        {
            this.name = name;
            this.type = type;
        }

        public string GetName()
        {
            return this.name;
        }

        public PokemonType GetType()
        {
            return this.type;
        }
    }
}
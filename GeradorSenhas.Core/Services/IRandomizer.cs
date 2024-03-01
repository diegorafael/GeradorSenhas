namespace GeradorSenhas.Core.Services
{
    public interface IRandomizer
    {
        int Sortear(int length);
    }
    public class Randomizer : IRandomizer
    {
        private readonly Random _random;

        public Randomizer()
        {
            _random = new Random();
        }

        public int Sortear(int length)
            => _random.Next(length);
    }
}
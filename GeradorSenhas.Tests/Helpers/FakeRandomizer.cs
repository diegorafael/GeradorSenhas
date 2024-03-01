using GeradorSenhas.Core.Services;

namespace GeradorSenhas.Tests.Helpers
{
    internal class FakeRandomizer : IRandomizer
    {
        int[] sequencia = [1, 2, 3, 4, 5, 6];
        private int atual = -1;
        private Random rnd = new Random();

        public int Sortear(int length)
        {
            return rnd.Next(length);

            if (atual >= sequencia.Length)
                atual = 0;
            else
                atual++;

            return sequencia[atual];
        }
    }
}

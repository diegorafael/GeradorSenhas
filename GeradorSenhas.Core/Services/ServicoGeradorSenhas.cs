using GeradorSenhas.Core.Models;
using System.Text;

namespace GeradorSenhas.Core.Services
{
    public class ServicoGeradorSenhas
    {
        const int QUANTIDADE_MINIMA_TIPOS_CARACTERES = 2;
        const int QUANTIDADE_MINIMA_CARACTERES = 5;

        private readonly IRandomizer _randomizer;

        private readonly string LetrasMinusculas = "abcdefghijklmnopqrstuvwxyz";
        private readonly string LetrasMaiusculas = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private readonly string CaracteresEspeciais = "!@#$%^&*()-_=+[{]};:'\",<.>/?\\|`~";
        private readonly string LetrasAcentuadas = "ÀÁÂÃÄÅÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖØÙÚÛÜÝàáâãäåçèéêëìíîïðñòóôõöøùúûüýÿ";
        private readonly string Numeros = string.Join("", Enumerable.Range(0,10).Select(x => x.ToString()));

        public ServicoGeradorSenhas(IRandomizer randomizer)
        {
            _randomizer = randomizer;
        }

        public SenhaGerada GerarSenha(RequisicaoSenha requisicaoSenha)
        {
            Validar(requisicaoSenha);
            
            var chars = ObterChars(requisicaoSenha);

            var randomStr = new string(
                Enumerable
                .Repeat(chars, requisicaoSenha.QuantidadeCaracteres)
                .Select(x => x[_randomizer.Sortear(x.Length)])
                .ToArray());

            var senhaGerada = new SenhaGerada()
            {
                Senha = randomStr
            };

            return senhaGerada;
        }

        private char[] ObterChars(RequisicaoSenha requisicaoSenha)
        {
            var sb = new StringBuilder();

            if (requisicaoSenha.PodeConterCaracteresEspeciais)
                sb.Append(CaracteresEspeciais);
            if (requisicaoSenha.PodeConterLetrasMaiusculas)
                sb.Append(LetrasMaiusculas);
            if (requisicaoSenha.PodeConterLetrasMinúsculas)
                sb.Append(LetrasMinusculas);
            if (requisicaoSenha.PodeConterNumeros)
                sb.Append(Numeros);
            if (requisicaoSenha.PodeConterLetrasAcentuadas)
                sb.Append(LetrasAcentuadas);

            return sb.ToString().ToCharArray();
        }   

        private void Validar(RequisicaoSenha requisicaoSenha)
        {
            var opcaoTiposCaracteres = new Dictionary<string, bool>
            {
                { nameof(RequisicaoSenha.PodeConterLetrasMaiusculas), requisicaoSenha.PodeConterLetrasMaiusculas },
                { nameof(RequisicaoSenha.PodeConterCaracteresEspeciais), requisicaoSenha.PodeConterCaracteresEspeciais },
                { nameof(RequisicaoSenha.PodeConterLetrasMinúsculas), requisicaoSenha.PodeConterLetrasMinúsculas },
                { nameof(RequisicaoSenha.PodeConterLetrasAcentuadas), requisicaoSenha.PodeConterLetrasAcentuadas },
                { nameof(RequisicaoSenha.PodeConterNumeros), requisicaoSenha.PodeConterNumeros }
            };

            if (opcaoTiposCaracteres.Count(x => x.Value == true) < QUANTIDADE_MINIMA_TIPOS_CARACTERES)
                throw new ArgumentException($"Pelo menos {QUANTIDADE_MINIMA_TIPOS_CARACTERES} tipos decaracteres devem ser selecionado");

            if (requisicaoSenha.QuantidadeCaracteres < QUANTIDADE_MINIMA_CARACTERES)
                throw new ArgumentException($"A senha deve ter {QUANTIDADE_MINIMA_CARACTERES} caracteres ou mais");
        }
    }
}
using GeradorSenhas.Core;
using GeradorSenhas.Tests.Helpers;

namespace GeradorSenhas.Tests
{
    public class ServicoGeradorSenhasTests
    {
        [Theory]
        [MemberData(nameof(RequisicoesComUmTipoDeCaractere))]
        public void Deve_retornar_erro_caso_menos_de_dois_tipos_de_caracteres_seja_ativados_na_requisicao(RequisicaoSenha requisicaoSenha)
        {
            var sut = new ServicoGeradorSenhas();

            var senhaGerada = sut.GerarSenha(requisicaoSenha);

        }

        public static IEnumerable<object[]> RequisicoesComUmTipoDeCaractere() {
            var builder = new RequisicaoSenhaBuilder();

            yield return new[] { builder.Reset().ComCaracteresEspeciais().Build() };
            yield return new[] { builder.Reset().ComLetrasAcentuadas().Build() };
            yield return new[] { builder.Reset().ComLetrasMaiusculas().Build() };
            yield return new[] { builder.Reset().ComLetrasMinusculas().Build() };
            yield return new[] { builder.Reset().ComNumeros().Build() };
            yield return new[] { builder.Reset().Build() };
        }
    }
}

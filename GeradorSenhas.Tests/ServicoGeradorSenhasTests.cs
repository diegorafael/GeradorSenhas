using FluentAssertions;
using GeradorSenhas.Core.Models;
using GeradorSenhas.Core.Services;
using GeradorSenhas.Tests.Helpers;
using NSubstitute;

namespace GeradorSenhas.Tests
{
    public class ServicoGeradorSenhasTests
    {
        private ServicoGeradorSenhas _sut;
        public ServicoGeradorSenhasTests()
        {
            // ToDo: Substituir pelo mock
            _sut = new ServicoGeradorSenhas(new Randomizer());
        }

        [Theory]
        [MemberData(nameof(RequisicoesComMenosDeDoisTiposDeCaractere))]
        public void Deve_retornar_erro_caso_menos_de_dois_tipos_de_caracteres_sejam_ativados_na_requisicao(RequisicaoSenha requisicaoSenha)
        {
            // Arrange / Act / Assert
            _sut.Invoking(s => s.GerarSenha(requisicaoSenha)).Should().ThrowExactly<ArgumentException>("Pelo menos dois tipos decaracteres devem ser selecionados");
        }

        [Theory]
        [MemberData(nameof(RequisicoesComDoisOuMaisTiposDeCaractere))]
        public void Nao_Deve_retornar_erro_caso_dois_ou_mais_tipos_de_caracteres_sejam_ativados_na_requisicao(RequisicaoSenha requisicaoSenha)
        {
            // Arrange / Act / Assert
            _sut.Invoking(s => s.GerarSenha(requisicaoSenha))
                .Should()
                .NotThrow<Exception>("Pelo menos dois tipos decaracteres foram ser selecionados");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Deve_retornar_erro_caso_a_requisicao_seja_para_senha_com_menos_de_cinco_caracteres(int qtdeCaracteres)
        {
            // Arrange 
            var requisicaoSenha = new RequisicaoSenhaBuilder()
                .ComCaracteresEspeciais()
                .ComNumeros()
                .ComNumeroCaracteres(qtdeCaracteres)
                .Build();

            // Act / Assert
            _sut
                .Invoking(s => s.GerarSenha(requisicaoSenha))
                .Should()
                .ThrowExactly<ArgumentException>("A senha deve ter pelo menos 5 caracteres.");
        }

        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(6)]
        [InlineData(400)]
        public void Nao_deve_retornar_erro_caso_a_requisicao_seja_para_senha_com_cinco_caracteres_ou_mais(int qtdeCaracteres)
        {
            // Arrange 
            var requisicaoSenha = new RequisicaoSenhaBuilder()
                .ComCaracteresEspeciais()
                .ComNumeros()
                .ComNumeroCaracteres(qtdeCaracteres)
                .Build();

            // Act / Assert
            _sut
                .Invoking(s => s.GerarSenha(requisicaoSenha))
                .Should()
                .NotThrow<Exception>("A senha possui cinco caracteres ou mais.");
        }

        [Fact]
        public void Deve_gerar_senha_com_o_numero_de_caracteres_de_acordo_com_a_requisicao()
        {
            // Arrange
            var mockedRandomizer = Substitute.For<IRandomizer>();
            
            var sut = new ServicoGeradorSenhas(mockedRandomizer);
            var requisicaoSenha = new RequisicaoSenhaBuilder()
                .ComLetrasMaiusculas()
                .ComLetrasMinusculas()
                .ComNumeros()
                .ComLetrasAcentuadas()
                .ComCaracteresEspeciais()
                .ComNumeroCaracteres(10)
                .Build();

            mockedRandomizer.Sortear(Arg.Any<int>()).Returns(1,19,5,20,66);

            // Act
            var senhasGeradas = sut.GerarSenha(requisicaoSenha);

            // Assert
            //senhaGerada.Should().NotBeNull("Uma senha deve ser gerada");
            //senhaGerada.Senha.Should().NotBeNullOrEmpty().And.HaveLength(requisicaoSenha.QuantidadeCaracteres);
        }

        public static IEnumerable<object[]> RequisicoesComMenosDeDoisTiposDeCaractere() {
            var builder = new RequisicaoSenhaBuilder();

            yield return new[] { builder.Reset().ComCaracteresEspeciais().Build() };
            yield return new[] { builder.Reset().ComLetrasAcentuadas().Build() };
            yield return new[] { builder.Reset().ComLetrasMaiusculas().Build() };
            yield return new[] { builder.Reset().ComLetrasMinusculas().Build() };
            yield return new[] { builder.Reset().ComNumeros().Build() };
            yield return new[] { builder.Reset().Build() };
        }

        public static IEnumerable<object[]> RequisicoesComDoisOuMaisTiposDeCaractere() {
            var builder = new RequisicaoSenhaBuilder();

            yield return new[] { builder.Reset().ComCaracteresEspeciais().ComLetrasMaiusculas().Build() };
            yield return new[] { builder.Reset().ComLetrasAcentuadas().ComNumeros().Build() };
            yield return new[] { builder.Reset().ComLetrasMaiusculas().ComLetrasMinusculas().Build() };
            yield return new[] { builder.Reset().ComLetrasMinusculas().ComCaracteresEspeciais().Build() };
            yield return new[] { builder.Reset().ComNumeros().ComLetrasMaiusculas().Build() };
            yield return new[] 
            { 
                builder
                    .Reset()
                    .ComLetrasMinusculas()
                    .ComLetrasMaiusculas()
                    .ComNumeros()
                    .ComLetrasAcentuadas()
                    .ComCaracteresEspeciais()
                    .Build() 
            };
        }
    }
}

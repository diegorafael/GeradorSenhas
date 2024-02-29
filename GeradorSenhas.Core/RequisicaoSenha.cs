
namespace GeradorSenhas.Core
{
    public class RequisicaoSenha
    {
        public bool PodeConterLetrasMaiusculas { get; set; }
        public bool PodeConterLetrasMinúsculas { get; set; }
        public bool PodeConterLetrasAcentuadas { get; set; }
        public bool PodeConterNumeros { get; set; }
        public bool PodeConterCaracteresEspeciais { get; set; }
    }
}
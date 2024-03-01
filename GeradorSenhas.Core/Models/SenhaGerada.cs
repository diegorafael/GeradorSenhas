
namespace GeradorSenhas.Core
{
    public class SenhaGerada
    {
        public string Senha { get; set; }
        public Guid? Id { get; set; }
        public DateTime? DataCriacao { get; set; }
        public string? HashParcial { get; set; }
    }
}
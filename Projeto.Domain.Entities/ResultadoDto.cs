namespace Projeto.Domain.Entities
{
    public class ResultadoDto
    {
        public ResultadoDto(bool sucesso)
        {
            Sucesso = sucesso;
        }

        public ResultadoDto(bool sucesso, string mensagem) : this(sucesso)
        {
            Mensagem = mensagem;
        }

        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
    }
}

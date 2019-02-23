namespace Projeto.Domain.Entities
{
    public class PromocaoDto
    {
        public int Codigo { get; set; }
        public string NomeProduto { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public bool Finalizada { get; set; }
        public string CaminhoImagemProduto { get; set; }
    }
}

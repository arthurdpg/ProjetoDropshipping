using Projeto.CrossCutting;

namespace Projeto.Domain.Entities
{
    public class PromocaoDto
    {
        public PromocaoDto() { }

        public PromocaoDto(Promocao promocao)
        {
            Codigo = promocao.Codigo;
            NomeProduto = promocao.Produto.Nome;
            DataInicio = promocao.DataInicio.ToString(Formatos.FormatoDataPtBr);
            DataFim = promocao.DataFim.HasValue ? promocao.DataFim.Value.ToString(Formatos.FormatoDataPtBr) : string.Empty;
            Finalizada = promocao.DataFim.HasValue;
            Desconto = Formatos.FormatarValor(promocao.Desconto.ToString(), Formatos.FormatoMoeda);
            Preco = Formatos.FormatarValor(promocao.Produto.Preco.ToString(), Formatos.FormatoMoeda);
            PrecoComDesconto = Formatos.FormatarValor((promocao.Produto.Preco - promocao.Desconto).ToString(), Formatos.FormatoMoeda);
            Avaliacao = promocao.Produto.Avaliacao;
        }

        public int Codigo { get; set; }
        public string NomeProduto { get; set; }
        public string Preco { get; set; }
        public decimal Avaliacao { get; set; }
        public string PrecoComDesconto { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public string Desconto { get; set; }
        public bool Finalizada { get; set; }
        public string CaminhoImagemProduto { get; set; }
    }
}

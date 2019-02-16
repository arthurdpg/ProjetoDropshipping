using System;

namespace Projeto.Domain.Entities
{
    public class Promocao
    {
        public int Codigo { get; set; }
        public Produto Produto { get; set; }
        public decimal Desconto { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}

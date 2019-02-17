using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("PROMOCAO")]
    public class Promocao
    {
        [Column("CD_PROMOCAO")]
        [Key]
        public int Codigo { get; set; }

        [ForeignKey("CD_PRODUTO")]
        [Required]
        public Produto Produto { get; set; }

        [Column("NR_DESCONTO")]
        [Required]
        public decimal Desconto { get; set; }

        [Column("DT_INICIO")]
        [Required]
        public DateTime DataInicio { get; set; }

        [Column("DT_FIM")]
        public DateTime? DataFim { get; set; }
    }
}

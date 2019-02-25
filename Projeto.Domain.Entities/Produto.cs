using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("PRODUTO")]
    public class Produto
    {
        [Column("CD_PRODUTO")]
        [Key]
        public int Codigo { get; set; }

        [Column("NM_PRODUTO")]
        [Required(AllowEmptyStrings = false)]
        public string Nome { get; set; }

        [Column("DS_PRODUTO")]
        public string Descricao { get; set; }

        [ForeignKey("CD_CATEGORIA")]
        [Required]
        public Categoria Categoria { get; set; }

        [Column("NR_PRECO")]
        [Required]
        public decimal Preco { get; set; }

        [Column("NR_AVALIACAO")]
        [Required]
        public decimal Avaliacao { get; set; }
    }
}

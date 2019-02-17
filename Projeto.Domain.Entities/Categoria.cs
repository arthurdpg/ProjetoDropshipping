using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto.Domain.Entities
{
    [Table("CATEGORIA")]
    public class Categoria
    {
        [Column("CD_CATEGORIA")]
        [Key]
        public int Codigo { get; set; }

        [Column("DS_CATEGORIA")]
        [Required(AllowEmptyStrings = false)]
        public string Descricao { get; set; }
    }
}

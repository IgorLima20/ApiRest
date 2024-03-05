using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiRest.Models
{
    [Table("products")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Nome do Produto")]
        [StringLength(100, ErrorMessage = "O Nome do Produto deve possuir no máximo 100 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Por favor, informe o Preço do Produto")]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Por favor, informe a Descrição do Produto")]
        [StringLength(800, ErrorMessage = "A Descrição do Produto deve possuir no máximo 800 caracteres")]
        public string Description { get; set; }

        public Product(int id, string name, decimal price, string description)
        {
            Id = id;
            Name = name;
            Price = price;
            Description = description;
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string title { get; set; }

        [MaxLength(1000, ErrorMessage = "Este campo deve conter até 1000 caracteres")]
        public string description { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Categoria inválida")]
        public int CategoryId { get; set;}
        public Category Category { get; set; }
    }
}
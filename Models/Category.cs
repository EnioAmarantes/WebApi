using System.ComponentModel.DataAnnotations;

namespace webapi.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(60, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        [MinLength(3, ErrorMessage = "Este campo deve conter entre 3 e 60 caracteres")]
        public string Title { get; set; }
    }
}
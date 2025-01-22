using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Entites
{
    public class User
    {
        [Range(1, 100)]
        public int Id { get; set; }
        
        [DisplayName("User Name")]
        [Required]
        public string Name { get; set; }

        [DisplayName("User Surname")]
        [Required]
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Image { get; set; }
    }
}

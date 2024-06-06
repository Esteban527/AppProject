using System.ComponentModel.DataAnnotations;

namespace AppProject.Web.Data.Entities
{
    public class TaskList
    {
        [Key]
        public int Id { get; set; }


        [MaxLength(128, ErrorMessage = "El campo  {0} debe tener máximo {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Tarea: ")]
        public string Description { get; set; }

        [Display(Name = "¿Está completada?")]
        public bool Iscompleted { get; set; } = false;
    }
}

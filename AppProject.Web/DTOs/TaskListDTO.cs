using System.ComponentModel.DataAnnotations;

namespace AppProject.Web.DTOs
{
    public class TaskListDTO
    {
        public int Id { get; set; }


        [MaxLength(128, ErrorMessage = "El campo  {0} debe tener máximo {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Agrega tu tarea")]
        public string Description { get; set; }

        [Display(Name = "¿Está completada?")]
        public bool Iscompleted { get; set; } = false;
    }
}

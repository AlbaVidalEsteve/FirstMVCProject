using System.ComponentModel.DataAnnotations;
namespace FirstMVCProject.Models
{
    public class Animal
    {
        public int IdAnimal { get; set; }

        [Required(ErrorMessage = "El nombre del animal es obligatorio.")]
        public string NombreAnimal { get; set; }
        public string Raza { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un tipo de animal.")]
        public int RIdTipoAnimal { get; set; } // Este campo se utiliza para el ComboBox
        public DateTime? FechaNacimiento { get; set; }

        //Navegación hacia TipoAnimal
        public TipoAnimal TipoAnimal { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;

namespace FirstMVCProject.Models.ViewModels
{
    public class CreateAnimalViewModel
    {
        public Animal AnimalCreate { get; set; } = new Animal();
        
    }
}

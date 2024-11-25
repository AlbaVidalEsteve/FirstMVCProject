using FirstMVCProject.DAL;

namespace FirstMVCProject.Models.ViewModels
{
    public class AnimalesViewModel
    {
        public List<Animal> Animals { get; set; } = new List<Animal>(); 
        public AnimalesViewModel()
        {           
            
        }
    }
}

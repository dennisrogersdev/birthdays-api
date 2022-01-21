using System.ComponentModel.DataAnnotations;
namespace teste.ViewModels
{
    public class PersonViewModel
    {
        [RequiredAttribute]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
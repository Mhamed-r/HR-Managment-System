using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class PublicHoliday
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; } =string.Empty;
        public DateOnly Date { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
namespace WebNotes.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }
    }
}

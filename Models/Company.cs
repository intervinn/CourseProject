using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CourseProject.Models
{
    [LocalizedColumn("Компания")]
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [LocalizedColumn("Id")]
        public int Id { get; set; }

        [NotNull]
        [LocalizedColumn("Название")]
        public string Name { get; set; } = string.Empty;

        [NotNull]
        [LocalizedColumn("Страна")]
        public string Country { get; set; } = string.Empty;

        public override string ToString()
        {
            return Name;
        }
    }
}

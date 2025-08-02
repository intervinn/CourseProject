using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CourseProject.Models
{
    [LocalizedColumn("Шины")]
    public class Tires
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [LocalizedColumn("Id")]
        public int Id { get; set; }
        [LocalizedColumn("Модель")]
        public string Model { get; set; } = string.Empty;
        [LocalizedColumn("Размер")]
        public string Size { get; set; } = string.Empty;
        [LocalizedColumn("Тип")]
        public string Type { get; set; } = string.Empty;

        [LocalizedColumn("Производитель")]
        [NotNull]
        public int? ManufacturerId { get; set; }

        [LocalizedColumn("Производитель")]
        [ForeignKey(nameof(ManufacturerId))]
        public Company? Manufacturer { get; set; }

        public override string ToString()
        {
            return $"{Model}";
        }
    }
}

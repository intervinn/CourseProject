using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CourseProject.Models
{
    [LocalizedColumn("Фары")]
    public class Headlights
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [LocalizedColumn("Id")]
        public int Id { get; set; }

        [NotNull]
        [LocalizedColumn("Тип")]
        public HeadlightType Type { get; set; }

        [NotNull]
        [LocalizedColumn("Автоматические")]
        public bool IsAutomatic { get; set; }

        [NotNull]
        [LocalizedColumn("Имеет адаптивный угол")]
        public bool HasAdaptiveCornering { get; set; }

        [NotNull]
        [LocalizedColumn("Светоотдача")]
        public int LumenOutput { get; set; }

        [NotNull]
        [LocalizedColumn("Производитель")]
        public int? ManufacturerId { get; set; }

        [LocalizedColumn("Производитель")]
        [ForeignKey(nameof(ManufacturerId))]
        public Company? Manufacturer { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }
    }

}

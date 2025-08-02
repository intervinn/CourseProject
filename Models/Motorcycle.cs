
/*

Мотоцикл
производитель
модель
страна
тип (спорт, коляска)
цвет
год выпуска
объем двигателя
шины, фары 
опции комфорт
макс. скорость
рег номер
дата прохождения техосмотра
данные о владельце


*/

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace CourseProject.Models
{
    [LocalizedColumn("Мотоцикл")]
    public class Motorcycle
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [LocalizedColumn("Id")]
        public int Id { get; set; }
        [NotNull]
        [LocalizedColumn("Модель")]
        public string Model { get; set; } = string.Empty;
        [NotNull]
        [LocalizedColumn("Страна")]
        public string Country { get; set; } = string.Empty;
        [NotNull]
        [LocalizedColumn("Тип")]
        public MotorcycleType Type { get; set; }
        [NotNull]
        [LocalizedColumn("Цвет")]
        public string Color { get; set; } = string.Empty;

        [NotNull]
        [LocalizedColumn("Дата производства")]
        public int ProductionYear { get; set; }
        [NotNull]
        [LocalizedColumn("Удобства")]
        public List<ComfortFeature> ComfortFeatures { get; set; } = [];
        [NotNull]
        [LocalizedColumn("Макс.Скорость")]
        public int MaxSpeed { get; set; }
        [LocalizedColumn("Рег.Номер")]
        public string RegistrationNumber { get; set; } = string.Empty;
        [LocalizedColumn("Дата Техосмотра")]
        public DateTime LastInspectionDate { get; set; }

        
        [NotNull]
        public int? OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        [LocalizedColumn("Владелец")]
        public Person? Owner { get; set; }

        [NotNull]
        public int? ManufacturerId { get; set; }

        [ForeignKey(nameof(ManufacturerId))]
        [LocalizedColumn("Производитель")]
        public Company? Manufacturer { get; set; }

        [NotNull]
        public int? TiresId { get; set; }

        [ForeignKey(nameof(TiresId))]
        [LocalizedColumn("Шины")]
        public Tires? Tires { get; set; }

        [NotNull]
        [LocalizedColumn("Фары")]
        public int? HeadlightsId { get; set; }

        [ForeignKey(nameof(HeadlightsId))]
        public Headlights? Headlights { get; set; }
    }
}

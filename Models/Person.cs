using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace CourseProject.Models
{
    [LocalizedColumn("Человек")]
    public class Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [LocalizedColumn("Id")]
        public int Id { get; set; }
        [NotNull]
        [LocalizedColumn("Имя")]
        public string FirstName { get; set; } = string.Empty;
        [NotNull]
        [LocalizedColumn("Фамилия")]
        public string LastName { get; set; } = string.Empty;
        [NotNull]
        [LocalizedColumn("Дата рождения")]
        public DateTime BirthDate { get; set; }
        [LocalizedColumn("Лицензия")]
        public string LicenseNumber { get; set; } = string.Empty;
        [LocalizedColumn("Телефон")]
        public string ContactPhone { get; set; } = string.Empty;
        [LocalizedColumn("Адрес")]
        public string Address { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }
}

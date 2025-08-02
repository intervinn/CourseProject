
namespace CourseProject.Models
{
    public enum MotorcycleType
    {
        [LocalizedColumn("Спортивный")]
        Sport,
        [LocalizedColumn("Коляска")]
        Sidecar,
        [LocalizedColumn("Турер")]
        Touring,
        [LocalizedColumn("Круизер")]
        Cruiser,
        [LocalizedColumn("Бездорожный")]
        OffRoad,
        [LocalizedColumn("Нэйкед")]
        Naked,
        [LocalizedColumn("Скутер")]
        Scooter
    }

}

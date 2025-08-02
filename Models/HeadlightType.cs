
namespace CourseProject.Models
{
    public enum HeadlightType
    {
        [LocalizedColumn("Галогенные")]
        Halogen,
        [LocalizedColumn("LED")]
        LED,
        [LocalizedColumn("HID")]
        HID,
        [LocalizedColumn("Лазерные")]
        Laser
    }
}

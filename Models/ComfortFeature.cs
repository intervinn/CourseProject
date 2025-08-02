
namespace CourseProject.Models
{
    public enum ComfortFeature
    {
        [LocalizedColumn("Ручки с подогревом")]
        HeatedGrips,
        [LocalizedColumn("Круиз-контроль")]
        CruiseControl,
        [LocalizedColumn("ABS")]
        ABS,
        [LocalizedColumn("Управление тягой")]
        TractionControl,
        [LocalizedColumn("Быстрая передача")]
        QuickShifter,
        [LocalizedColumn("Регулируемая подвеска")]
        AdjustableSuspension,
        [LocalizedColumn("Подогрев сидения")]
        HeatedSeat,
        [LocalizedColumn("Система навигации")]
        NavigationSystem
    }
}

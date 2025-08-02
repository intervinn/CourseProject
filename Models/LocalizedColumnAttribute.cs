
namespace CourseProject.Models
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
    class LocalizedColumnAttribute : Attribute
    {
        public string Name;

        public LocalizedColumnAttribute(string name )
        {
            Name = name;
        }
    }
}


namespace CourseProject.Helpers
{
    public interface IModelForm
    {
        public void Load(object archetype);
        public void Reset();
        public void SetMode(string mode);
    }
}

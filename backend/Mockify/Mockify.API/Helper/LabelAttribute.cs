namespace Mockify.API.Helper
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class LabelAttribute : System.Attribute
    {
        public readonly string Text;

        public LabelAttribute(string Text)
        {
            this.Text = Text;
        }
    }
}

namespace _3dPrintHelper.ViewsExt
{
    public class NamedControlAttribute : System.Attribute
    {
        public string? Name { get; set; }

        public NamedControlAttribute()
        {
        }

        public NamedControlAttribute(string name) => Name = name;
    }
}
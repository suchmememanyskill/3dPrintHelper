using System;

namespace _3dPrintHelper.ViewsExt
{
    public class NamedControlAttribute : Attribute
    {
        public string? Name { get; set; }

        public NamedControlAttribute()
        {
        }

        public NamedControlAttribute(string name) => Name = name;
    }
}
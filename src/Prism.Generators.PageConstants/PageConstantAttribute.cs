using System;

namespace Prism.Generators.PageConstants
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class PageConstantAttribute : Attribute
    {
        public string Name { get; set; }

        public PageConstantAttribute(string name = default)
            => Name = name;
    }
}

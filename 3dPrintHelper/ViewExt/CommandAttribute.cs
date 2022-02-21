using System;
using _3dPrintHelper.Service;
using Avalonia.Controls;

namespace _3dPrintHelper.ViewsExt
{
    public class CommandAttribute : Attribute
    {
        public string ButtonName { get; set; }

        public CommandAttribute(string buttonName) => ButtonName = buttonName;
    }
}
using ApiLinker.Generic;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dPrintHelper.Service
{
    public static class Extensions
    {
        public static SolidColorBrush ToBrush(this Colour colour) => new(new Color(255, colour.R, colour.G, colour.B));
    }
}

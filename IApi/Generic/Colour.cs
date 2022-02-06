using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Generic
{
    public class Colour
    {
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }

        public Colour(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public Colour() : this(0, 0, 0)
        { }
    }
}

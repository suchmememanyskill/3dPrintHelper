using ApiLinker.Interfaces;
using ApiLinker.Local;
using ApiLinker.Thingiverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker
{
    public class API
    {
        public List<IApi> APIs { get; private set; }
        public API()
        {
            APIs = new()
            {
                new ThingiverseApi(),
                LocalApi.GetInstance(),
            };
        }
    }
}

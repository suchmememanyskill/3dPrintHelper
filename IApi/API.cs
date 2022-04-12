using ApiLinker.Interfaces;
using ApiLinker.Local;
using ApiLinker.MyMiniFactory;
using ApiLinker.Thingiverse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiLinker.PrusaPrintables;

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
                new MMFApi(),
                new PrintablesApi(),
                LocalApi.GetInstance(),
            };
        }
    }
}

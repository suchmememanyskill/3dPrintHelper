using ApiLinker.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Interfaces
{
    public interface ICreator
    {
        string Name();
        Uri Uri();
        ISavable Thumbnail();
    }
}

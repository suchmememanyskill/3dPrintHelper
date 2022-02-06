using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiLinker.Local.Models
{
    public class LocalRoot
    {
        public List<LocalPost> Posts { get; private set; } = new();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimChainLib
{
    abstract class Transformer
    {
        public abstract (double x, double y) Transform((double x, double y) point, double fac);
    }
}

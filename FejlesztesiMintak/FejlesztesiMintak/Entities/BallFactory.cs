using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FejlesztesiMintak.Entities
{
    public class BallFactory
    {
        public Ball CreateNew()
        {
            Ball b = new Ball();
            return b;
        }
    }
}

using FejlesztesiMintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FejlesztesiMintak.Entities
{
    public class CarFactory : IToyFactory
    {
        //jobbklikk class-névre -> Quick actions... -> Implement..
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FejlesztesiMintak.Abstractions
{
    public interface IToyFactory
    {
        //interface-ben mindig public-ak --> elhagyható
        //abstract függvényhez hasonlóan nem kell {}
        //kötelező lesz használni
        Toy CreateNew();
    }
}

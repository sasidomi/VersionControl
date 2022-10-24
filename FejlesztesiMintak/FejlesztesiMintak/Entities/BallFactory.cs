﻿using FejlesztesiMintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FejlesztesiMintak.Entities
{
    public class BallFactory : IToyFactory
    {
        public Toy CreateNew()
        {
            Ball b = new Ball();
            return b;
        }
    }
}

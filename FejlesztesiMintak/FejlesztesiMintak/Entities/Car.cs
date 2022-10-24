using FejlesztesiMintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FejlesztesiMintak.Entities
{
    public class Car : Toy
    {
        //gyorsba: osztálynévre jobbklikk -> Quick actions -> Implement Abstract Class
        protected override void DrawImage(Graphics g)
        {
            Image imgFile = Image.FromFile("Images/car.png");
            //Width és Height: a korábban Toy-ban is meghatározott 50
            g.DrawImage(imgFile, new Rectangle(0, 0, Width, Height));
        }
    }
}

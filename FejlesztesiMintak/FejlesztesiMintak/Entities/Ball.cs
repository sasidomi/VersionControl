using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using FejlesztesiMintak.Abstractions;

namespace FejlesztesiMintak.Entities
{
    public class Ball : Toy
    {
        //private set: osztályon kívül csak lekérdezni lehet, módosítani nem
        public SolidBrush BallColor { get; private set; }

        //Ball osztálynak bemenő paraméter = Ball osztály ctor-jában bemenő paraméter
        public Ball(Color color)
        {
            BallColor = new SolidBrush(color);
        }

        //override: abstract és virtual függvények felülírása esetén kell
        //láthatóság-nak egyeznie kell (private nem lehet nyilván)
        protected override void DrawImage(Graphics g)
        {
            //g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
            g.FillEllipse(BallColor, 0, 0, Width, Height);
        }
    }
}

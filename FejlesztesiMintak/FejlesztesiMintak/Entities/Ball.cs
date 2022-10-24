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
        //override: abstract és virtual függvények felülírása esetén kell
        //láthatóság-nak egyeznie kell (private nem lehet nyilván)
        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(new SolidBrush(Color.Blue), 0, 0, Width, Height);
        }
    }
}

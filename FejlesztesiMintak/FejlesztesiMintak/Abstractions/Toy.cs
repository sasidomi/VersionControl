using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace FejlesztesiMintak.Abstractions
{
    public abstract class Toy : Label
    {
        public Toy()
        {
            AutoSize = false;
            Width = 50;
            Height = 50;
            Paint += Toy_Paint;
        }

        private void Toy_Paint(object sender, PaintEventArgs e)
        {
            //throw new NotImplementedException();
            DrawImage(e.Graphics);
        }

        //így ez a függvény minden osztályban kötelező lesz
        protected abstract void DrawImage(Graphics g);

        //virtual: felül lehet írni a leszármaztatott osztályokban, de önmagában is használható
        public virtual void MoveToy()
        {
            Left += 1;
        }
    }
}

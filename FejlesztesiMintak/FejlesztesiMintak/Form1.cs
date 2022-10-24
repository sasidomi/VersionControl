using FejlesztesiMintak.Abstractions;
using FejlesztesiMintak.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FejlesztesiMintak
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private List<Toy> _toys = new List<Toy>();

        private IToyFactory _factory;
        //Inconsistent accessibility: property type 'type' is less accessible than property 'property'
        //megoldás: a Ball és BallFactory class-okat is public-ra rakni az Entities-ben
        public IToyFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            //automatikusan aktív: Design -> Properties -> Enabled: true
            Toy t = Factory.CreateNew();
            _toys.Add(t);
            t.Left = -t.Width;
            mainPanel.Controls.Add(t);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            //automatikusan aktív: Design -> Properties -> Enabled: true
            var mostRightToy = 0;
            foreach (Toy t in _toys)
            {
                t.MoveToy();

                if (t.Left > mostRightToy)
                {
                    mostRightToy = t.Left;
                }
            }
            if (mostRightToy > 1000)
            {
                var firstToy = _toys[0];
                _toys.Remove(firstToy);
                mainPanel.Controls.Remove(firstToy);
            }
        }
    }
}

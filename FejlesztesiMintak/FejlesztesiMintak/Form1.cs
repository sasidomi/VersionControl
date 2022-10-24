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
            Factory = new BallFactory();
        }

        private List<Ball> _balls = new List<Ball>();

        private BallFactory _factory;
        //Inconsistent accessibility: property type 'type' is less accessible than property 'property'
        //megoldás: a Ball és BallFactory class-okat is public-ra rakni az Entities-ben
        public BallFactory Factory
        {
            get { return _factory; }
            set { _factory = value; }
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            //automatikusan aktív: Design -> Properties -> Enabled: true
            Ball b = Factory.CreateNew();
            _balls.Add(b);
            b.Left = -b.Width;
            mainPanel.Controls.Add(b);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            //automatikusan aktív: Design -> Properties -> Enabled: true
            var mostRightBall = 0;
            foreach (Ball b in _balls)
            {
                b.MoveBall();

                if (b.Left > mostRightBall)
                {
                    mostRightBall = b.Left;
                }
            }
            if (mostRightBall > 1000)
            {
                var firstBall = _balls[0];
                _balls.Remove(firstBall);
                mainPanel.Controls.Remove(firstBall);
            }
        }
    }
}

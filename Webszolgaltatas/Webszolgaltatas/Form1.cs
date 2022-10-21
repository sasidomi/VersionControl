using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Webszolgaltatas.Entities;
using Webszolgaltatas.MnbServiceReference;

namespace Webszolgaltatas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            GetEuroExchangeRate();
            dataGridView1.DataSource = Rates;
        }

        void GetEuroExchangeRate()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = "2020-01-01",
                endDate = "2020-06-30"
            };

            var response = mnbService.GetExchangeRates(request);

            var result = response.GetExchangeRatesResult;

            //Console.Write(result);
        }

        BindingList<RateData> Rates = new BindingList<RateData>();

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using Webszolgaltatas.Entities;
using Webszolgaltatas.MnbServiceReference;

namespace Webszolgaltatas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var result = GetEuroExchangeRate();
            XML(result);
            dataGridView1.DataSource = Rates;
        }

        string GetEuroExchangeRate()
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
            return result;
        }

        void XML(string result)
        {
            var xml = new XmlDocument();

            xml.LoadXml(result);

            foreach (XmlElement e in xml.DocumentElement)
            {
                var rate = new RateData();

                rate.Date = DateTime.Parse(e.GetAttribute("date"));

                var childElement = (XmlElement)e.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }

        }

        BindingList<RateData> Rates = new BindingList<RateData>();
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;
using Webszolgaltatas.Entities;
using Webszolgaltatas.MnbServiceReference;

namespace Webszolgaltatas
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();

        BindingList<string> Currencies = new BindingList<string>();

        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = Currencies;

            string result = GetCurrencies();
            LoadCurrencies(result);

            RefreshData();

        }

        void RefreshData()
        {
            Rates.Clear();
            string result = GetEuroExchangeRate();
            XML(result);
            dataGridView1.DataSource = Rates;
            ShowDiagram();
        }

        string GetCurrencies()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetCurrenciesRequestBody();

            var response = mnbService.GetCurrencies(request);

            var result = response.GetCurrenciesResult;

            return result;
        }

        void LoadCurrencies(string result)
        {
            var xml = new XmlDocument();

            xml.LoadXml(result);

            var childElement = xml.DocumentElement.ChildNodes[0];

            foreach (XmlElement e in childElement)
            {
                Currencies.Add(e.InnerText.ToString());
            }
        }

        string GetEuroExchangeRate()
        {
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = (string)comboBox1.SelectedItem,
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
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
                Rates.Add(rate);

                rate.Date = DateTime.Parse(e.GetAttribute("date"));

                var childElement = (XmlElement)e.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");

                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
            }
        }

        void ShowDiagram()
        {
            chart1.DataSource = Rates;

            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;

            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();

        }
    }
}

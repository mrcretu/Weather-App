using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using System.IO;
using System.Web;

namespace WeatherApp
{
    public partial class Form1 : Form
    {
        string Temperatură;
        string Condiții;
        string Umiditate;
        string VitezaVântului;
        string Oraș;
        string TFCond;
        string TFHigh;
        string TFLow;
        string OraData;
        string Rasarit;
        string Apus;
        int temperatura;
        int temperaturaa;
        int temperaturaaa;

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            GetWeather();

            temperatura = Convert.ToInt32(Temperatură);
            temperatura = (temperatura - 30) / 2;
            Temperatură = Convert.ToString(temperatura);

            this.textBox1.Text = Temperatură + " C°";
            this.textBox2.Text = Condiții;
            this.textBox3.Text = Umiditate + " %";
            this.textBox4.Text = VitezaVântului + " km/h";
            this.textBox5.Text = Oraș;
            temperaturaa = Convert.ToInt32(TFLow);
            temperaturaa = (temperaturaa - 30) / 2;
            TFLow = Convert.ToString(temperaturaa);

            this.textBox6.Text = TFLow + " C°";

            temperaturaaa = Convert.ToInt32(TFHigh);
            temperaturaaa = (temperaturaaa - 30) / 2;
            TFHigh = Convert.ToString(temperaturaaa);

            this.textBox7.Text = TFHigh + " C°";
            this.textBox8.Text = TFCond;
            this.DataOra.Text = OraData;
            this.textBox9.Text = Rasarit;
            this.textBox10.Text = Apus;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void GetWeather()
        {
            string query = String.Format("http://weather.yahooapis.com/forecastrss?w=881258");
            XmlDocument wData = new XmlDocument();
            wData.Load(query);

            XmlNamespaceManager manager = new XmlNamespaceManager(wData.NameTable);
            manager.AddNamespace("yweather", "http://xml.weather.yahoo.com/ns/rss/1.0");

            XmlNode channel = wData.SelectSingleNode("rss").SelectSingleNode("channel");
            XmlNodeList nodes = wData.SelectNodes("/rss/channel/item/yweather:forecast", manager);

            Temperatură = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["temp"].Value;
            Condiții = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["text"].Value;
            Umiditate = channel.SelectSingleNode("yweather:atmosphere", manager).Attributes["humidity"].Value;
            VitezaVântului = channel.SelectSingleNode("yweather:wind", manager).Attributes["speed"].Value;
            Oraș = channel.SelectSingleNode("yweather:location", manager).Attributes["city"].Value;
            TFCond = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["text"].Value;
            TFHigh = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["high"].Value;
            TFLow = channel.SelectSingleNode("item").SelectSingleNode("yweather:forecast", manager).Attributes["low"].Value;
            OraData = channel.SelectSingleNode("item").SelectSingleNode("yweather:condition", manager).Attributes["date"].Value;
            Rasarit = channel.SelectSingleNode("yweather:astronomy", manager).Attributes["sunrise"].Value;
            Apus = channel.SelectSingleNode("yweather:astronomy", manager).Attributes["sunset"].Value;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace GeoPoligons
{
    public partial class Form1 : Form
    {
        private async Task GeoDataAsync()
        {
            var client = new HttpClient();

            // .NET 4.5 async await pattern
            var task = await client.GetAsync("ВашUrl");
            var jsonString = await task.Content.ReadAsStringAsync();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
                var client = new WebClient { Encoding = Encoding.UTF8 };
            string StartUrl = "https://nominatim.openstreetmap.org/search?q=";
            client.Headers["User-Agent"] = "Mozilla/5.0";
            // string json;
            string json = Encoding.UTF8.GetString(client.DownloadData(StartUrl+textBox3.Text+"&format=json&polygon_geojson=1"));
            List<Models.Class1> geo = JsonConvert.DeserializeObject<List<Models.Class1>>(json);
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(54.532747, 36.318718));
            points.Add(new PointLatLng(54.492495, 36.326470));
            points.Add(new PointLatLng(54.476423, 36.242333));
            points.Add(new PointLatLng(54.504639, 36.169835));
            foreach (var item in geo)
            {
                foreach (var item2 in item.geojson.coordinates)
                {
                    foreach (var myarr in item2.)
                    {

                    }
                }
            }
            gMap.DragButton = MouseButtons.Left;
            gMap.MapProvider = GMapProviders.YandexMap;

            double lat = Convert.ToDouble(textBox1.Text);
            double lon = Convert.ToDouble(textBox2.Text);
            gMap.Position = new PointLatLng(lat, lon);

            gMap.MinZoom = 5;
            gMap.MaxZoom = 100;
            gMap.Zoom = 15;

            //Вид маркера
            PointLatLng point = new PointLatLng(lat, lon);
            GMapMarker marker = new GMarkerGoogle(point, GMarkerGoogleType.blue_small);
            //Создание маркера
            GMapOverlay markers = new GMapOverlay("markers");

            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            polyOverlay.Polygons.Clear();
            gMap.Overlays.Clear();
            polyOverlay.Polygons.Add(polygon);
            gMap.Overlays.Add(polyOverlay);
            markers.Markers.Clear();
            markers.Markers.Add(marker);
            gMap.Overlays.Add(markers);
            gMap.Zoom += 1;
            gMap.Zoom -= 1;
            
        } 
    }
}

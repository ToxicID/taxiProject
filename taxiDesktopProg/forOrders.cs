using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;
using GMap.NET.WindowsForms;
using GMap.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap.NET.WindowsPresentation;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Drawing;
using Newtonsoft.Json.Linq;

namespace taxiDesktopProg
{
    internal class forOrders
    {
        public void findAddressCity(TextBox text)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(auth.connectionString))
            {
                var city = db.addresses;

                foreach (address City in city)
                {
                    textComplete.Add(City.city);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
        }
        public void findAddressStreet(TextBox text, string cityText)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(auth.connectionString))
            {
                var city = db.addresses.Where(p => p.city == cityText);

                foreach (address City in city)
                {
                    textComplete.Add(City.street);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }
        public void findClientMobile(TextBox text)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(auth.connectionString))
            {
                var Client = db.clients.Where(p => p.blacklist != true);

                foreach (client cl in Client)
                {
                    textComplete.Add(cl.mobile_phone);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
        }
        //Проверка клиента, если нет такого в таблице, то добавляется, если есть, просто передаётся id
        public long checkClient(string mc)
        {
            long idClient;
            using (Context db = new Context(auth.connectionString))
            {
                var client = db.clients;

                if (client.Where(p => p.mobile_phone == mc).Count() != 1)
                {
                    client cl = new client
                    {
                        mobile_phone = mc,
                        blacklist = false
                    };
                    db.clients.Add(cl);
                    db.SaveChanges();
                    idClient = cl.id_client;
                    return idClient;
                }
                else
                {
                    var findClient = client.Where(p => p.mobile_phone == mc).FirstOrDefault();
                    idClient = findClient.id_client;
                    return idClient;

                }
            }
        }
        //Проверка адреса, если нет такого адреса в списках, добавляется, если есть то просто возращает его id
        public long checkAddress(string cityAd, string streetAd, string houseAd, string enranceAd)
        {
            using (Context db = new Context(auth.connectionString))
            {
                var address = db.addresses;
                long idAddress;
                string newEnranced = "";
                if (string.IsNullOrWhiteSpace(enranceAd))
                    newEnranced = "";
                else
                    newEnranced = $"П{enranceAd}";

                if (address.Where(p => p.city == cityAd &&
                                 p.street == streetAd &&
                                 p.house == houseAd &&
                                 p.enrance == newEnranced).Count() != 1)
                {

                    address ad = new address
                    {
                        enrance = newEnranced,
                        city = cityAd,
                        street = streetAd,
                        house = houseAd
                    };
                    db.addresses.Add(ad);

                    db.SaveChanges();


                    idAddress = ad.id_address;
                    return idAddress;
                }
                else
                {
                    var findAddress = address.Where(p => p.city == cityAd &&
                                 p.street == streetAd &&
                                 p.house == houseAd &&
                                 p.enrance == newEnranced).FirstOrDefault();
                    idAddress = findAddress.id_address;
                    return idAddress;

                }
            }
        }
        public void findAddressHouse(TextBox text, string cityText, string streetText)
        {
            AutoCompleteStringCollection textComplete = new AutoCompleteStringCollection();

            using (Context db = new Context(auth.connectionString))
            {
                var city = db.addresses.Where(p => p.city == cityText && p.street == streetText);

                foreach (address City in city)
                {
                    textComplete.Add(City.house);
                }

                text.AutoCompleteCustomSource = textComplete;
                text.AutoCompleteMode = AutoCompleteMode.Suggest;
                text.AutoCompleteSource = AutoCompleteSource.CustomSource;

            }
        }
        public string BingMapsApiKey = "vSu0ER5x3HHYD5rGxVaB~YuU-4x1i_kyBbcC_YW2ipA~AvEK6xCobjGDdWvUXvsi8MDhiuaduWxeSveNhLTBOVTP9B7lgi1vT_DYA4CGvIuD";
        addOrEditOrders form;
       public forOrders(addOrEditOrders s)
        {
            this.form = s;
        }
        public GMapOverlay markersOverlay;
        public GMapOverlay routesOverlay;
        public void LoadMap()
        {
            if (form.gmap.Overlays.Count > 0)
            {
                for (int i = 0; i < form.gmap.Overlays.Count; i++)
                {
                    form.gmap.Overlays.RemoveAt(0);

                    form.gmap.Refresh();
                }
                routesOverlay.Clear();
            }

            BingMapProvider.Instance.ClientKey = BingMapsApiKey;
            //gmap.MapProvider = BingMapProvider.Instance;
            form.gmap.MapProvider = GMapProviders.GoogleMap;
            GMapProvider.Language = LanguageType.Russian;
            form.gmap.PolygonsEnabled = true;
            form.gmap.RoutesEnabled = true;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            form.gmap.Position = new PointLatLng(array1[0], array1[1]); // Начальные координаты 
            form.gmap.MinZoom = 1;
            form.gmap.MaxZoom = 18;
            form.gmap.Zoom = 10;
            form.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionWithoutCenter;
            form.gmap.ShowCenter = false;
            // Создание оверлеев для маркеров и маршрута
            markersOverlay = new GMapOverlay("markers");
            routesOverlay = new GMapOverlay("routes");
            form.gmap.Overlays.Add(markersOverlay);
            form.gmap.Overlays.Add(routesOverlay);
        }
        public double[] array1 = new double[2];
        public double[] array2 = new double[2];
        public void AddMarker(PointLatLng start, PointLatLng end, string label1, string label2)
        {
            GMarkerGoogle markerStart = new GMarkerGoogle(start, GMarkerGoogleType.green);
            markerStart.ToolTip = new GMapRoundedToolTip(markerStart);
            markerStart.ToolTipText = label1;
            markersOverlay.Markers.Add(markerStart);

            GMarkerGoogle markerEnd = new GMarkerGoogle(end, GMarkerGoogleType.red);
            markerEnd.ToolTip = new GMapRoundedToolTip(markerEnd);
            markerEnd.ToolTipText = label2;
            markersOverlay.Markers.Add(markerEnd);
        }

        public void DrawRoute(PointLatLng start, PointLatLng end)
        {
            MapRoute route = GMap.NET.MapProviders.GMapProviders.BingMap.GetRoute(start, end, false, false, 15);
            GMap.NET.WindowsForms.GMapRoute routeOverlay = new GMap.NET.WindowsForms.GMapRoute(route.Points, "Route");
            routeOverlay.Stroke = new Pen(System.Drawing.Color.Red, 3);
            routesOverlay.Routes.Add(routeOverlay);
        }
        public double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            string originCoordinates = $"{lat1},{lon1}";
            string destinationCoordinates = $"{lat2},{lon2}";

            string encodedOriginCoordinates = Uri.EscapeDataString(originCoordinates);
            string encodedDestinationCoordinates = Uri.EscapeDataString(destinationCoordinates);


            string requestUrl = string.Format("http://dev.virtualearth.net/REST/V1/Routes/Driving?wp.0={0}&wp.1={1}&key={2}", encodedOriginCoordinates, encodedDestinationCoordinates, BingMapsApiKey);


            WebRequest request = WebRequest.Create(requestUrl);
            WebResponse response = request.GetResponse();


            StreamReader reader = new StreamReader(response.GetResponseStream());
            string responseString = reader.ReadToEnd();

            dynamic result = JsonConvert.DeserializeObject(responseString);


            double distanceInMiles = result.resourceSets[0].resources[0].travelDistance;
            response.Close();
            reader.Close();

            //вывод расстояния
            //MessageBox.Show(distanceInMiles.ToString());
            return distanceInMiles;
        }
   
        //Вычисление координат адреса
        
    }
}

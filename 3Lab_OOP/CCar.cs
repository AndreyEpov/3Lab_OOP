using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Numerics;


namespace _3Lab_OOP
{
    public class CCar : CMapObject
    {
       
        private PointLatLng point;
        private CRoude route;
        //private List<CHuman> pass = new List<CHuman>();
        private CHuman pass;
        GMapMarker carMarker;
        GMapMarker humanMarker;
        GMapControl gMap = null;
        Thread newThread;
        List<PointLatLng> ePoints = new List<PointLatLng>();
        // событие прибытия
        public event EventHandler Arrived;
        public event EventHandler ArrivedtoDestination;
        public event EventHandler FollowtheCar;
        public CCar(string title, PointLatLng point, GMapControl map) : base(title)
        {
            this.point = point;
            pass = null;
            gMap = map; 
        }
        public override double getDistance(PointLatLng point)
        {
            GeoCoordinate p1 = new GeoCoordinate(point.Lat, point.Lng);
            GeoCoordinate p2 = new GeoCoordinate(this.point.Lat, this.point.Lng);
            return p1.GetDistanceTo(p2);
        }



        public override PointLatLng getFocus()
        {
            return point;
        }

        public override GMapMarker getMarker()
        {
            GMapMarker marker = new GMapMarker(point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                    VerticalAlignment = System.Windows.VerticalAlignment.Center,
                    ToolTip = this.getTitle(), // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/pics/car.png")) // картинка
                }

            };
            marker.ZIndex = 100;
            carMarker = marker;
            return marker;
        }
        public GMapMarker moveTo(PointLatLng dest)
        {
            // провайдер навигации
            RoutingProvider routingProvider = GMapProviders.OpenStreetMap;
            // определение маршрута
            MapRoute route = routingProvider.GetRoute(
             point, // начальная точка маршрута
             dest, // конечная точка маршрута
             false, // поиск по шоссе (false - включен)
             false, // режим пешехода (false - выключен)
             (int)15);
            // получение точек маршрута
            List<PointLatLng> routePoints = route.Points;

            this.route = new CRoude("", routePoints);

            double dist = 0;
            double minDist = 0.000001;

            List<PointLatLng> points = this.route.getPoints();
            ePoints.Clear();

            for (int i = 0; i < points.Count -1; i++)
            {
                dist = Vector2.Distance(new Vector2((float)points[i].Lat, (float)points[i].Lng), new Vector2((float)points[i + 1].Lat, (float)points[i + 1].Lng));
                    if(dist> minDist)
                    {
                    double aPoints = dist / minDist;
                    for (int j = 0; j < aPoints; j++)
                        {
                        Vector2 t = Vector2.Lerp(new Vector2((float)points[i].Lat, (float)points[i].Lng), new Vector2((float)points[i + 1].Lat, (float)points[i + 1].Lng), (float)(j / aPoints));
                        ePoints.Add(new PointLatLng(t.X, t.Y));
                        }  
                    }
            }
           


            newThread = new Thread(new ThreadStart(MoveByRoute));
            newThread.Start();


            return this.route.getMarker();
        }
        private void MoveByRoute()
        {
            double cAngle = 0;
            // последовательный перебор точек маршрута
            for (int i = 0; i < ePoints.Count; i++)
            {
                var point = ePoints[i];
                // делегат, возвращающий управление в главный поток
                Application.Current.Dispatcher.Invoke(delegate {

                    if (i < ePoints.Count - 10)
                    {
                        var nextPoint = ePoints[i + 10];

                        double latDiff = nextPoint.Lat - point.Lat;
                        double lngDiff = nextPoint.Lng - point.Lng;
                        // вычисление угла направления движения
                        // latDiff и lngDiff - катеты прямоугольного треугольника
                        double angle = Math.Atan2(lngDiff, latDiff) * 180.0 / Math.PI;

                        // установка угла поворота маркера

                        if (Math.Abs(angle - cAngle) > 11) //|| (a - angle < 0))
                        {
                            cAngle = angle;
                            carMarker.Shape.RenderTransform = new RotateTransform(angle, 20, 20);
                        }
                    }
                    // изменение позиции маркера
                    carMarker.Position = point;
                    this.point = point;

                    if (pass != null)
                    {
                        pass.setPosition(point);
                        pass.humanMarker.Position = point;
                        pass.humanMarker.Shape.RenderTransform = new RotateTransform(cAngle, 20, 20);

                    }
                });
                // задержка 5 мс
                Thread.Sleep(5);
            }
            // отправка события о прибытии после достижения последней точки маршрута
            if (pass == null)
                Arrived?.Invoke(this, null);
            else
            {
                MessageBox.Show("Высаживаемся!");
                pass = null;
                //ArrivedtoDestination?.Invoke(this, null);
                newThread.Abort();
            }
                
        }


        public void passSeated(object sender,EventArgs e)
        {
            MessageBox.Show("Taxi arrived");
            pass = (CHuman)sender;
             Application.Current.Dispatcher.Invoke(delegate{ 
                 gMap.Markers.Add(moveTo(pass.getDestination()));
             });
            pass.point=point;
        }
        public void passRise(object sender, EventArgs e)
        {
            //pass = (CHuman)sender;
            MessageBox.Show("Car arrived");
        }


    }
}

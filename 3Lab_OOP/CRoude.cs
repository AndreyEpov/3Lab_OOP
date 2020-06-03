using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3Lab_OOP
{
    public class CRoude : CMapObject
    {
        private List<PointLatLng> points;
        public CRoude(string title,List<PointLatLng> points) : base(title) 
        {
            this.points = new List<PointLatLng>();
         
            foreach(PointLatLng p in points)
            {
                this.points.Add(p);
            }
        }

        public List<PointLatLng> getPoints()
        {
            return points;
        }
        public override double getDistance(PointLatLng point)
        {
            GeoCoordinate p1 = new GeoCoordinate(point.Lat, point.Lng);
            GeoCoordinate p2 = new GeoCoordinate(points[0].Lat, points[0].Lng);
            return p1.GetDistanceTo(p2);
        }

        public override PointLatLng getFocus()
        {
            return points[0];
        }

        public override GMapMarker getMarker()
        {
            GMapMarker marker = new GMapRoute(points)
            {
                Shape = new Path()
                {
                    Stroke = Brushes.DarkBlue, // цвет обводки
                    Fill = Brushes.DarkBlue, // цвет заливки
                    StrokeThickness = 4 // толщина обводки
                }
            };
            return marker;
        }

    }
}

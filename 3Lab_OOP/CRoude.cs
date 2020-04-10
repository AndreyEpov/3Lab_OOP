using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
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
        public override double getDistance(PointLatLng point)
        {
            throw new NotImplementedException();
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

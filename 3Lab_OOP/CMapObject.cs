using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Device.Location;
namespace _3Lab_OOP
{
    public abstract class CMapObject
    {
        private string title;
        private DateTime date;
        public CMapObject(string title)
        {
            this.title = title;
            date = DateTime.Now;
        }

        public string getTitle()
        {
            return title;
        }

        public DateTime getCreationDate()
        {
            return date;
        }
        
        public abstract double getDistance(PointLatLng point);
        public abstract PointLatLng getFocus();
        public abstract GMapMarker getMarker();
    }
}

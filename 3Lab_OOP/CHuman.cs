﻿using GMap.NET;
using GMap.NET.WindowsPresentation;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace _3Lab_OOP
{
    class CHuman : CMapObject
    {
        public PointLatLng point;
        private PointLatLng destination;
        public GMapMarker humanMarker;
        public event EventHandler passSeated;
      //  public event EventHandler passRise;
        public CHuman(string title, PointLatLng point) : base(title)
        {
            this.point = point;
        }

        public PointLatLng getDestination()
        {
            return destination;
        }

        public void setPosition(PointLatLng point)
        {
            this.point = point;
        }

        public void moveTo(PointLatLng dest)
        {
            destination = dest;
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
                    ToolTip = this.getTitle(), // всплывающая подсказка
                    Margin = new System.Windows.Thickness(-16, -16, 0, 0),
                    Source = new BitmapImage(new Uri("pack://application:,,,/pics/human1.png")) // картинка
                }

            };
            humanMarker = marker;
            return marker; 
        }
        public void CarArrived(object sender, EventArgs e)
        {

            passSeated?.Invoke(this, EventArgs.Empty);

            //MessageBox.Show("Car arrived");

        }

        public void CarArrivedToDestination(object sender, EventArgs e)
        {
          //  passRise?.Invoke(this, EventArgs.Empty);
        }

    }
}

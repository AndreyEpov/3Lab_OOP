using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using System.Device.Location;


namespace _3Lab_OOP
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CMapObject> objs = new List<CMapObject>();

        List<PointLatLng> pts = new List<PointLatLng>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void MapLoaded(object sender, RoutedEventArgs e)
        {
            // настройка доступа к данным
            GMaps.Instance.Mode = AccessMode.ServerAndCache;

            // установка провайдера карт
            Map.MapProvider = OpenStreetMapProvider.Instance;

            // установка зума карты
            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            Map.Zoom = 15;
            // установка фокуса карты
            Map.Position = new PointLatLng(55.012823, 82.950359);

            // настройка взаимодействия с картой
           
            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;

            PointLatLng point = new PointLatLng(55.016511, 82.946152);

            GMapMarker marker = new GMapMarker(point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    ToolTip = "Honda CR-V", // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/pics/car.png")) // картинка
                }

            };
            Map.Markers.Add(marker);

            // координаты точек замкнутой области (полигона)
            List<PointLatLng> points = new PointLatLng[] {
                 new PointLatLng(55.016351, 82.950650),
                 new PointLatLng(55.017021, 82.951484),
                 new PointLatLng(55.015795, 82.954526),
                 new PointLatLng(55.015129, 82.953586) }.ToList();
            GMapMarker marker2 = new GMapPolygon(points)
            {
                Shape = new Path
                {
                    Stroke = Brushes.Black, // стиль обводки
                    Fill = Brushes.Violet, // стиль заливки
                    Opacity = 0.7 // прозрачность
                }
            };
            Map.Markers.Add(marker2);
            // координаты точек маршрута
            List<PointLatLng> points2 = new PointLatLng[] {
             new PointLatLng(55.010637, 82.938550),
             new PointLatLng(55.012421, 82.940781),
             new PointLatLng(55.014613, 82.943497),
             new PointLatLng(55.016214, 82.945469) }.ToList();
            GMapMarker marker3 = new GMapRoute(points)
            {
                Shape = new Path()
                {
                    Stroke = Brushes.DarkBlue, // цвет обводки
                    Fill = Brushes.DarkBlue, // цвет заливки
                    StrokeThickness = 4 // толщина обводки
                }
            };
            Map.Markers.Add(marker3);
        }

        private void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
            pts.Add(point);
        }

        private void ShowObject_Click(object sender, RoutedEventArgs e)
        {
            Map.Markers.Clear();
            objectList.Items.Clear();
            foreach (CMapObject cm in objs)
            {
                Map.Markers.Add(cm.getMarker());
                objectList.Items.Add(cm.getTitle());
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (objectList.SelectedIndex >-1)
            {
                PointLatLng p = objs[objectList.SelectedIndex].getFocus();
                Map.Position = p; 

            }
                
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (objType.SelectedIndex > -1)
            {
                if (objType.SelectedIndex == 0)
                {
                    CCar car = new CCar(objTitle.Text, pts[0] );
                    objs.Add(car);
                }
                if (objType.SelectedIndex == 4)
                {
                    CRoude roude = new CRoude(objTitle.Text, pts);
                    objs.Add(roude);
                }
            }
            pts.Clear();
        }
    }
}

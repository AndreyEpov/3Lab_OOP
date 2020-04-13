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
        bool createFlag = true;
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
        }

        private void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            if (objType.SelectedIndex == 0 || objType.SelectedIndex == 1 || objType.SelectedIndex == 2)
            {
                Map.Markers.Clear();
                foreach (CMapObject cm in objs)
                {
                    Map.Markers.Add(cm.getMarker());
                }
            }
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
            pts.Add(point);
            GMapMarker marker = new GMapMarker(point)
            {
                Shape = new Image
                {
                    Width = 32, // ширина маркера
                    Height = 32, // высота маркера
                    Source = new BitmapImage(new Uri("pack://application:,,,/pics/point.png")), // картинка
                 
                }
                
            };
            
            Map.Markers.Add(marker);

        }

        private void ShowObject_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            if (find.IsChecked == true)
            {
                foreach (CMapObject cm in objs)
                {
                    if (cm.getTitle() == objTitle.Text)
                        Map.Position = cm.getFocus();

                }

            }


        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (objType.SelectedIndex > -1 && createFlag)
            {
                if (objType.SelectedIndex == 0)
                {
                    CCar car = new CCar(objTitle.Text, pts[0] );
                    objs.Add(car);
                }
                if (objType.SelectedIndex == 1)
                {
                    CHuman human = new CHuman(objTitle.Text, pts[0]);
                    objs.Add(human);
                }
                if (objType.SelectedIndex == 2)
                {
                    CLocation location = new CLocation(objTitle.Text, pts[0]);
                    objs.Add(location);
                }
                if (objType.SelectedIndex == 3)
                {
                    CArea area = new CArea(objTitle.Text, pts);
                    objs.Add(area);
                }
                if (objType.SelectedIndex == 4)
                {
                    CRoude roude = new CRoude(objTitle.Text, pts);
                    objs.Add(roude);
                }
            }
            else if (objType.SelectedIndex > -1 && !createFlag)
            {

            }
            pts.Clear();
            Map.Markers.Clear();
            objTitle.Clear();
            objectList.Items.Clear();
            foreach (CMapObject cm in objs)
            {
                Map.Markers.Add(cm.getMarker());
                objectList.Items.Add(cm.getTitle());
            }
        }

        private void createObjs_Checked(object sender, RoutedEventArgs e)
        {
             createFlag = true;
        }

        private void find_Checked(object sender, RoutedEventArgs e)
        {
            createFlag = false;
        }

        private void Clear_but_Click(object sender, RoutedEventArgs e)
        {
            Map.Markers.Clear();
            pts.Clear();
            foreach (CMapObject cm in objs)
            {
                Map.Markers.Add(cm.getMarker());
            }
        }

        private void objTitle_TextChanged(object sender, TextChangedEventArgs e)
        {
            


        }
    }
}

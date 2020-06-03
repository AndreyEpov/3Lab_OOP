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

        List<CCar> cars = new List<CCar>();

        List<CCar> carsSort = new List<CCar>();

        List<PointLatLng> pts = new List<PointLatLng>();

        List<CMapObject> nearestObjects = new List<CMapObject>();

        List<GMapMarker> markerTaxi = new List<GMapMarker>();

        bool createFlag = true;
        bool buttonDoubleClick = false;
        CHuman human = null;
        CCar car = null;

       

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
            CMapObject mapObject = null;
           
            if (createFlag)
            {
        
                if (objType.SelectedIndex == 0 || objType.SelectedIndex == 1 || objType.SelectedIndex == 2)
                {
                   
                    pts.Clear();
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
                if (!buttonDoubleClick)
                {
                    Map.Markers.Add(marker);
                    buttonDoubleClick = false;
                }
            }
            else if (!createFlag)
            {
                IEnumerable<CMapObject> query;
                pts.Clear();
                Map.Markers.Clear();
                nearestObjects.Clear();
                foreach (CMapObject cm in objs)
                {
                    Map.Markers.Add(cm.getMarker());
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
                foreach (CMapObject cm in objs)
                {
                    nearestObjects.Add(cm);
                }
                foreach (CMapObject cm in objs)
                {
                    mapObject = cm;
                  
                    break;
                }
                objectList.Items.Clear();
                query = nearestObjects.OrderBy(cmapObj => cmapObj.getDistance(pts[0]));
                foreach (CMapObject obj in query)
                {
                    try
                    {
               
                        objectList.Items.Add(obj.getTitle());
                    }
                    catch
                    {
                        break;
                    }
                }
            }
        }

        private void ShowObject_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void button_Click(object sender, RoutedEventArgs e) //focus
        {

            if (find.IsChecked == true)
            {
                foreach (CMapObject cm in objs)
                {
                    if (cm.getTitle() == objTitle.Text)
                    {
                        Map.Position = cm.getFocus();
                       
                    }
                }

            }


        }

        private void button1_Click(object sender, RoutedEventArgs e)//add obj
        {
            try
            { 
                if (objType.SelectedIndex > -1 && createFlag == true)
                {
                    if (objType.SelectedIndex == 0  && objTitle.Text.Length > 0)
                    {
                        CCar car = new CCar(objTitle.Text, pts[0],Map);
                        objs.Add(car);
                        cars.Add(car);
                    }
                    else if (objType.SelectedIndex == 0 && objTitle.Text.Length == 0)
                    {
                        MessageBox.Show("Введите имя машины");
                    }
                    if (objType.SelectedIndex == 1 && objTitle.Text.Length > 0)
                    {
                         human = new CHuman(objTitle.Text, pts[0]);
                        objs.Add(human);
                    }
                    else if (objType.SelectedIndex == 1 && objTitle.Text.Length == 0)
                    {
                        MessageBox.Show("Введите имя человека");
                    }
                    if (objType.SelectedIndex == 2 && objTitle.Text.Length > 0)
                    {
                        
                        CLocation location = new CLocation(objTitle.Text, pts[0]);
                        objs.Add(location);
                    }
                    else if (objType.SelectedIndex == 2 && objTitle.Text.Length == 0)
                    {
                        MessageBox.Show("Введите название место прибытия");
                    }
                    if (objType.SelectedIndex == 3 && pts.Count>2 && objTitle.Text.Length > 0)
                    {
                        CArea area = new CArea(objTitle.Text, pts);
                        objs.Add(area);
                    }
                    else if(objType.SelectedIndex == 3 && pts.Count <= 2 && objTitle.Text.Length > 0)
                    {
                        MessageBox.Show("Обозначьте площадь не менее из 3 точек");
                    }
                    if (objType.SelectedIndex == 4 && pts.Count > 1 && objTitle.Text.Length > 0)
                    {
                        CRoude roude = new CRoude(objTitle.Text, pts);
                        objs.Add(roude);
                    }
                    else if(objType.SelectedIndex == 4 && pts.Count <= 1 && objTitle.Text.Length > 0)
                    {
                        MessageBox.Show("Введите маршрут не менее из 2 точек");
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
                foreach (GMapMarker cm in markerTaxi)
                    Map.Markers.Add(cm);
            }
            catch
            {
                MessageBox.Show("Выберите место");
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

        private void I(object sender, DependencyPropertyChangedEventArgs e)
        {
            foreach (CMapObject cm in objs)
            {
                
                    
            }
        }

        private void Listbox_Mouse_d_click(object sender, MouseButtonEventArgs e)
        {
            foreach (CMapObject cm in objs)
            {
                if (cm.getTitle() == objectList.SelectedItem.ToString())
                {
                    Map.Position = cm.getFocus();
                    
                }
            }
 
        }

        private void objectList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void But_ToDest_Click(object sender, RoutedEventArgs e)
        {
            objType.SelectedIndex = 2;
                if(human != null)
                    if (human.getTitle() != null)
                        objTitle.Text = "Place of destination - " + human.getTitle();
        }

        private void But_callCar_Click(object sender, RoutedEventArgs e)
        {
            objType.SelectedIndex = 0;
            if (human.getTitle() != null)
                objTitle.Text = "Car of pass - " + human.getTitle();
        }

        private void Map_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            buttonDoubleClick = true;
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
            if (objType.SelectedIndex > -1 && createFlag == true)
            {
                if (objType.SelectedIndex == 0 && objTitle.Text.Length > 0)
                {
                    CCar car = new CCar(objTitle.Text, pts[0],Map);
                    objs.Add(car);
                    cars.Add(car);
                    if(human!=null)
                    { 
                        //car.Arrived += human.CarArrived;
                      //  human.passSeated += car.passSeated;
                       // car.ArrivedtoDestination += human.CarArrivedToDestination;
                       // human.passRise += car.passRise;
                    }

                }
                else if (objType.SelectedIndex == 0 && objTitle.Text.Length == 0)
                {
                    MessageBox.Show("Введите имя машины");
                }
                if (objType.SelectedIndex == 1 && objTitle.Text.Length > 0)
                {
                     human = new CHuman(objTitle.Text, pts[0]);
                    objs.Add(human);
                }
                else if (objType.SelectedIndex == 1 && objTitle.Text.Length == 0)
                {
                    MessageBox.Show("Введите имя человека");
                }
                if (objType.SelectedIndex == 2 && objTitle.Text.Length > 0)
                {
                    if (human != null)
                    {
                        human.moveTo(point);
                        
                        CLocation location = new CLocation(objTitle.Text, pts[0]);
                        objs.Add(location);
                    }
                }
                else if (objType.SelectedIndex == 2 && objTitle.Text.Length == 0)
                {
                    MessageBox.Show("Введите название место прибытия");
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
                foreach (GMapMarker cm in markerTaxi)
                    Map.Markers.Add(cm);
            }
        }

       /* private void But_Ok_Click(object sender, RoutedEventArgs e)
        {
              IEnumerable<CCar> query;
              query = cars.OrderBy(car => car.getDistance(human.getFocus()));
              foreach (CCar car in query)
              {
                  try
                  {
                      carsSort.Add(car);
                  }
                  catch
                  {
                      break;
                  }
              }
              
            Map.Markers.Add(carsSort[0].moveTo(human.getFocus()) );
            markerTaxi.Add(carsSort[0].moveTo(human.getFocus()));
        }*/
        private void But_Ok_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<CCar> query;
            query = cars.OrderBy(car => car.getDistance(human.getFocus()));
            foreach (CCar car in query)
            {
                try
                {
                    carsSort.Add(car);
                    break;
                }
                catch
                {
                    break;
                }
            }
            carsSort[0].Arrived += human.CarArrived;
            human.passSeated += carsSort[0].passSeated;
            carsSort[0].FollowtheCar += Follow_to;
            Map.Markers.Add(carsSort[0].moveTo(human.getFocus()));
           // markerTaxi.Add(carsSort[0].moveTo(human.getFocus()));
         
        }
        private void Follow_to(object sender, EventArgs args)
        {
            carsSort[0].Arrived -= human.CarArrived;           
            human.passSeated -= carsSort[0].passSeated;
            carsSort[0].FollowtheCar -= Follow_to;
            MessageBox.Show("Your stop!");
        }

    }
}

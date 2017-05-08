using System;
using System.IO;
using System.Reflection;
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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MetroMapNavigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
   
        public partial class MainWindow : Window
        {

            private void Enter_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                Group2.Clear();
                GlobalVaries.NameAnimation = GlobalVaries.CollectionPoints[GlobalVaries.StartPointIndex].Name + GlobalVaries.CollectionPoints[GlobalVaries.EndPointIndex].Name;
                GlobalVaries.CollectionPoints[GlobalVaries.StartPointIndex].ValueEdge = 0;
                GlobalVaries.da.AlgoritmRun(GlobalVaries.CollectionPoints[GlobalVaries.StartPointIndex]);
                GlobalVaries.r = PrintGrath.PrintMinPath(GlobalVaries.da);
                this.CreateAnimation(GlobalVaries.NameAnimation, GlobalVaries.r);
            }

            private void EnterButton_Click(object sender, RoutedEventArgs e)
            {
                Group2.Clear();
                GlobalVaries.NameAnimation = GlobalVaries.CollectionPoints[GlobalVaries.StartPointIndex].Name + GlobalVaries.CollectionPoints[GlobalVaries.EndPointIndex].Name;
                GlobalVaries.r = PrintGrath.PrintMinPath(GlobalVaries.da);
                this.CreateAnimation(GlobalVaries.NameAnimation, GlobalVaries.r);
            }

            private void ComboBox_Selected(object sender, RoutedEventArgs e)
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox == ComboBox1)
                {
                    GlobalVaries.EndPointIndex = comboBox.SelectedIndex;
                }
                else
                {
                    foreach (Pointer p in GlobalVaries.CollectionPoints)
                    {
                        p.ValueEdge = 9999;
                        p.IsChecked = false;
                    }
                    if ((GlobalVaries.StartPointIndex != null) && (GlobalVaries.EndPointIndex != null))
                    {
                        EnterButton.IsEnabled = true;
                    }
                    GlobalVaries.StartPointIndex = comboBox.SelectedIndex;
                    GlobalVaries.CollectionPoints[GlobalVaries.StartPointIndex].ValueEdge = 0;
                    GlobalVaries.da.AlgoritmRun(GlobalVaries.CollectionPoints[GlobalVaries.StartPointIndex]);
                }
                string selectedItem = (string)comboBox.SelectedItem;
                MessageBox.Show(selectedItem);
            }


            //https://geektimes.ru/post/63347/

            public class DekstraAlgorim
            {

                public Pointer[] points { get; private set; }
                public Edge[] rebra { get; private set; }
                public Pointer BeginPoint { get; private set; }

                public DekstraAlgorim(Pointer[] pointsOfgrath, Edge[] rebraOfgrath)
                {
                    points = pointsOfgrath;
                    rebra = rebraOfgrath;
                }

                /// <summary>
                /// Запуск алгоритма расчета
                /// </summary>

                public void AlgoritmRun(Pointer beginp)
                {
                    if (this.points.Count() == 0 || this.rebra.Count() == 0)
                    {
                        throw new DekstraException("Массив вершин или ребер не задан!");
                    }
                    else
                    {
                        BeginPoint = beginp;
                        OneStep(beginp);
                        foreach (Pointer point in points)
                        {
                            Pointer anotherP = GetAnotherUncheckedPoint();
                            if (anotherP != null)
                            {
                                OneStep(anotherP);
                            }
                            else
                            {
                                break;
                            }

                        }
                    }

                }
                /// <summary>
                /// Метод, делающий один шаг алгоритма. Принимает на вход вершину
                /// </summary>

                public void OneStep(Pointer beginpoint)
                {
                    foreach (Pointer nextp in Pred(beginpoint))
                    {
                        if (nextp.IsChecked == false)//не отмечена
                        {
                            float newmetka = beginpoint.ValueEdge + GetMyRebro(nextp, beginpoint).Weight;
                            if (nextp.ValueEdge > newmetka)
                            {
                                nextp.ValueEdge = newmetka;
                                nextp.predPoint = beginpoint;
                            }
                            else
                            {

                            }
                        }
                    }
                    beginpoint.IsChecked = true;//вычеркиваем
                }

                /// <summary>
                /// Поиск соседей для вершины. Для неориентированного графа ищутся все соседи.
                /// </summary>

                private IEnumerable<Pointer> Pred(Pointer currpoint)
                {
                    IEnumerable<Pointer> firstpoints = from ff in rebra where ff.FirstPoint == currpoint select ff.SecondPoint;
                    IEnumerable<Pointer> secondpoints = from sp in rebra where sp.SecondPoint == currpoint select sp.FirstPoint;
                    IEnumerable<Pointer> totalpoints = firstpoints.Concat<Pointer>(secondpoints);
                    return totalpoints;
                }

                /// <summary>
                /// Получаем ребро, соединяющее 2 входные точки
                /// </summary>

                private Edge GetMyRebro(Pointer a, Pointer b)
                {//ищем ребро по 2 точкам
                    IEnumerable<Edge> myr = from reb in rebra where (reb.FirstPoint.Name == a.Name && reb.SecondPoint.Name == b.Name) || (reb.SecondPoint.Name == a.Name && reb.FirstPoint.Name == b.Name) select reb;
                    if (myr.Count() > 1 || myr.Count() == 0)
                    {
                        throw new DekstraException("Не найдено ребро между соседями!");
                    }
                    else
                    {
                        return myr.First();
                    }
                }
                /// <summary>
                /// Получаем очередную неотмеченную вершину, "ближайшую" к заданной.
                /// </summary>
                /// <returns></returns>
                private Pointer GetAnotherUncheckedPoint()
                {
                    IEnumerable<Pointer> pointsuncheck = from p in points where p.IsChecked == false select p;
                    if (pointsuncheck.Count() != 0)
                    {
                        float minVal = pointsuncheck.First().ValueEdge;
                        Pointer minPoint = pointsuncheck.First();
                        foreach (Pointer p in pointsuncheck)
                        {
                            if (p.ValueEdge < minVal)
                            {
                                minVal = p.ValueEdge;
                                minPoint = p;
                            }
                        }
                        return minPoint;
                    }
                    else
                    {
                        return null;
                    }
                }

                public List<Pointer> MinPath1(Pointer end)
                {
                    List<Pointer> listOfpoints = new List<Pointer>();
                    Pointer tempp = new Pointer();
                    tempp = end;
                    while (tempp != this.BeginPoint)
                    {
                        listOfpoints.Add(tempp);
                        tempp = tempp.predPoint;
                    }
                    listOfpoints.Add(tempp);
                    return listOfpoints;
                }
            }

            /// <summary>
            /// Класс, реализующий ребро
            /// </summary>
            public class Edge
            {
                public Pointer FirstPoint { get; private set; }
                public Pointer SecondPoint { get; private set; }
                public float Weight { get; private set; }

                public Edge(Pointer first, Pointer second, float valueOfWeight)
                {
                    FirstPoint = first;
                    SecondPoint = second;
                    Weight = valueOfWeight;
                }
                public void RebroDraw()
                {
                    LineGeometry a = new LineGeometry();

                }
            }



            /// <summary>
            /// Класс, реализующий вершину графа
            /// </summary>
            public class Pointer
            {
                public float ValueEdge { get; set; }
                public string Name { get; private set; }
                public bool IsChecked { get; set; }
                public double latitude { get; set; }
                public double longtitude { get; set; }
                public Pointer predPoint { get; set; }
                public string LineColor { get; set; }
                public object SomeObj { get; set; }
                public Pointer(int value, bool ischecked)
                {
                    ValueEdge = value;
                    IsChecked = ischecked;
                    predPoint = new Pointer();
                }
                public Pointer(int value, bool ischecked, string name, double latitude, double longtitude, string LineColor)
                {
                    this.ValueEdge = value;
                    this.IsChecked = ischecked;
                    this.Name = name;
                    this.latitude = latitude;
                    this.longtitude = longtitude;
                    this.LineColor = LineColor;
                    predPoint = new Pointer();
                }
                public Pointer()
                {
                }


            }


            // <summary>
            /// для печати графа
            /// </summary>
            static class PrintGrath
            {
                public static List<LineGeometry> PrintAllPoints(DekstraAlgorim da)
                {
                    List<LineGeometry> retListOfPoints = new List<LineGeometry>();
                    int i = 0;
                    foreach (Pointer p in da.points)
                    {
                        if (p.predPoint.latitude != 0 && p.predPoint.longtitude != 0)
                        {
                            retListOfPoints.Add(new LineGeometry());
                            retListOfPoints[i].StartPoint = new Point(p.latitude * GlobalVaries.ScreenWidth, p.longtitude * GlobalVaries.ScreenHeight);
                            retListOfPoints[i].EndPoint = new Point(p.predPoint.latitude * GlobalVaries.ScreenWidth, p.predPoint.longtitude * GlobalVaries.ScreenHeight);
                            i++;
                        }

                    }
                    return retListOfPoints;
                }

                public static List<List<Point>> PrintMinPath(DekstraAlgorim da)
                {
                    int t = 0;

                    List<List<Point>> retListOfPointsAndPaths = new List<List<Point>>();
                    retListOfPointsAndPaths.Add(new List<Point>());

                    foreach (Pointer p in da.points)
                    {
                        foreach (Pointer p1 in da.MinPath1(p))
                        {
                            Point s = new Point();
                            s.X = (p1.latitude * GlobalVaries.ScreenWidth);
                            s.Y = (p1.longtitude * GlobalVaries.ScreenHeight);
                            retListOfPointsAndPaths[t].Add(s);

                        }


                        retListOfPointsAndPaths.Add(new List<Point>());
                        t++;
                    }
                    return retListOfPointsAndPaths;

                }
            }

            class DekstraException : ApplicationException
            {
                public DekstraException(string message) : base(message)
                {

                }

            }
            public static class GlobalVaries
            {
                public static double ScreenWidth = (SystemParameters.PrimaryScreenWidth - 250)*(1350/(SystemParameters.PrimaryScreenWidth - 250));
                public static double ScreenHeight = SystemParameters.PrimaryScreenHeight*(900 / SystemParameters.PrimaryScreenHeight);
                public static int StartPointIndex, EndPointIndex;
                public static List<List<Point>> r;
                public static string NameAnimation;
                public static Edge[] CollectionEdge = new Edge[52];
                public static Pointer[] CollectionPoints = new Pointer[52];
                public static DekstraAlgorim da = new DekstraAlgorim(GlobalVaries.CollectionPoints, GlobalVaries.CollectionEdge);
             }


            public void CreateAnimation(string s, List<List<Point>> GlobalVaries)
            {
                EllipseGeometry animatedEllipseGeometry =
                               new EllipseGeometry(new Point(0, 0), 10, 10);
                this.RegisterName(s, animatedEllipseGeometry);

                PathGeometry animationPath = new PathGeometry();
                PathFigure pFigure = new PathFigure();

                PolyLineSegment pBezierSegment = new PolyLineSegment();
                pFigure.StartPoint = GlobalVaries[MainWindow.GlobalVaries.EndPointIndex][0];
                Path3.Data = animatedEllipseGeometry;

                for (int i = MainWindow.GlobalVaries.EndPointIndex; i < MainWindow.GlobalVaries.EndPointIndex + 1; i++)
                {
                    for (int j = 1; j < (GlobalVaries[i].Count); j++)
                    {
                        pBezierSegment.Points.Add(GlobalVaries[i][j]);

                        LineGeometry q = new LineGeometry();
                        q.StartPoint = GlobalVaries[i][j - 1];
                        q.EndPoint = GlobalVaries[i][j];
                        Group2.AddGeometry(q);

                    }
                }



                pFigure.Segments.Add(pBezierSegment);
                animationPath.Figures.Add(pFigure);
                animationPath.Freeze();

                PointAnimationUsingPath pointAnimation =
                    new PointAnimationUsingPath();
                pointAnimation.PathGeometry = animationPath;
                pointAnimation.Duration = TimeSpan.FromSeconds(GlobalVaries[MainWindow.GlobalVaries.EndPointIndex].Count);
                pointAnimation.RepeatBehavior = RepeatBehavior.Forever;

                Storyboard.SetTargetName(pointAnimation, s);
                Storyboard.SetTargetProperty(pointAnimation,
                    new PropertyPath(EllipseGeometry.CenterProperty));

                Storyboard WayPointAnimationStoryboard = new Storyboard();
                WayPointAnimationStoryboard.AutoReverse = false;
                WayPointAnimationStoryboard.Children.Add(pointAnimation);
                WayPointAnimationStoryboard.Begin(this);

                this.UnregisterName(s);
            }


            public void LoadFile(string path,Assembly assembly)
            {
                List<EllipseGeometry> PointsList = new List<EllipseGeometry>();
                string[] NamesCollection = new string[52];

                int counter = 0;
            using (Stream stream = assembly.GetManifestResourceStream(path))
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] SplittedString = line.Split(' ');
                        double CoordX = Convert.ToDouble(SplittedString[0]);
                        double CoordY = Convert.ToDouble(SplittedString[1]);


                        NamesCollection[counter] = SplittedString[2];
                        GlobalVaries.CollectionPoints[counter] = new Pointer(9999, false, SplittedString[2], CoordX, CoordY, SplittedString[3]);

                        PointsList.Add(new EllipseGeometry());
                        PointsList[counter].RadiusX = 10;
                        PointsList[counter].RadiusY = 10;
                        PointsList[counter].Center = new Point(((GlobalVaries.ScreenWidth) * CoordX /*/ fl*/), (GlobalVaries.ScreenHeight * CoordY));
                        Group1.Children.Add(PointsList[counter]);
                        counter++;

                    }
                    ComboBox1.ItemsSource = NamesCollection;
                    ComboBox2.ItemsSource = NamesCollection;

                    for (int i = 0; i < counter - 1; i++)
                    {

                        if (GlobalVaries.CollectionPoints[i].LineColor == GlobalVaries.CollectionPoints[i + 1].LineColor)
                        {
                            GlobalVaries.CollectionEdge[i] = new Edge(GlobalVaries.CollectionPoints[i], GlobalVaries.CollectionPoints[i + 1], 1);
                        }
                    }
                    GlobalVaries.CollectionEdge[17] = new Edge(GlobalVaries.CollectionPoints[9], GlobalVaries.CollectionPoints[21], 2);
                    GlobalVaries.CollectionEdge[33] = new Edge(GlobalVaries.CollectionPoints[10], GlobalVaries.CollectionPoints[41], 2);
                    GlobalVaries.CollectionEdge[51] = new Edge(GlobalVaries.CollectionPoints[22], GlobalVaries.CollectionPoints[42], 2);
                    sr.Close();
                };
            }




            public MainWindow()
            {
                InitializeComponent();

                   
            var assembly = Assembly.GetExecutingAssembly();
            string path = "ProjectPoints.txt";

                ImageMap.Margin = new Thickness((GlobalVaries.ScreenWidth)/21.5, 0, 0, 0);
                LoadFile(path, assembly);
            
        }
    }
}

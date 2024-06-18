using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketMapTeam6.Views;

using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;
using static MarketMapTeam6.Views.ShoppingListPage;

namespace MarketMapTeam6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private Dictionary<string, List<IShoppingItem>> CategorizedItems { get; set; }
        public ObservableCollection<IShoppingItem> SelectedItems { get; set; }
        public MapPage(ObservableCollection<IShoppingItem> selectedItems)
        {
            InitializeComponent();
            SelectedItems = selectedItems;
            CreateCheckBoxes();
            CategorizeItems();
            CalculateShortestPath(); // Call CalculateShortestPath here
        }
        private void CalculateShortestPath()
        {
            GFG gfg = new GFG();
            List<int> points = gfg.GetPointsFromCategories(CategorizedItems);
            int[,] graph = new int[GFG.V, GFG.V]; // Define the graph
            gfg.dijkstra(graph, points);

            // Add points to the map image
            DrawToMap(points);

        }

        private bool pointsAdded = false;

        private void DrawToMap(List<int> points)
        {
            // Get the map image and its layout
            if (outerStack.Children.Count > 0 && outerStack.Children[0] is AbsoluteLayout mapLayout)
            {
                Image mapImage = mapLayout.Children[0] as Image;
                AbsoluteLayout pointsLayout = mapLayout.Children[1] as AbsoluteLayout;

                // Clear any existing points and lines
                pointsLayout.Children.Clear();

                // Define the marker color and size
                Color markerColor = Color.Red;
                double markerSize = 20;

                // Define the line color and thickness
                Color lineColor = Color.Blue;
                double lineThickness = 2;

                // Get the actual size of the map image
                mapImage.SizeChanged += (sender, e) =>
                {
                    if (!pointsAdded)
                    {
                        pointsAdded = true;

                        double mapWidth = mapImage.Width;
                        double mapHeight = mapImage.Height;

                        // Calculate the number of rows and columns in the map
                        int numRows = (int)Math.Ceiling(Math.Sqrt(GFG.V));
                        int numCols = (int)Math.Ceiling((double)GFG.V / numRows);

                        // Add points to the map image
                        foreach (int point in points)
                        {
                            // Create a circular marker using the Ellipse shape
                            Ellipse marker = new Ellipse
                            {
                                WidthRequest = markerSize,
                                HeightRequest = markerSize,
                                Stroke = markerColor,
                                StrokeThickness = 2,
                                Fill = markerColor,
                                IsVisible = true // Ensure the marker is visible
                            };

                            // Calculate the position of the marker on the map image
                            double x = (point % numCols) * (mapWidth / numCols) + (markerSize / 2);
                            double y = (point / numCols) * (mapHeight / numRows) + (markerSize / 2);

                            // Create a Rectangle with the desired bounds
                            Xamarin.Forms.Rectangle bounds = new Xamarin.Forms.Rectangle(x, y, markerSize, markerSize);

                            // Add the marker to the points layout
                            AbsoluteLayout.SetLayoutBounds(marker, bounds);
                            pointsLayout.Children.Add(marker);
                        }

                        // Add lines between the points on the map image
                        for (int i = 0; i < points.Count - 1; i++)
                        {
                            int point1 = points[i];
                            int point2 = points[i + 1];

                            // Calculate the position of the points on the map image
                            double x1 = (point1 % numCols) * (mapWidth / numCols) + (lineThickness / 2);
                            double y1 = (point1 / numCols) * (mapHeight / numRows) + (lineThickness / 2);
                            double x2 = (point2 % numCols) * (mapWidth / numCols) + (lineThickness / 2);
                            double y2 = (point2 / numCols) * (mapHeight / numRows) + (lineThickness / 2);

                            // Create a Line with the desired bounds
                            Line line = new Line
                            {
                                Stroke = lineColor,
                                StrokeThickness = lineThickness,
                                X1 = x1,
                                Y1 = y1,
                                X2 = x2,
                                Y2 = y2
                            };

                            // Add the line to the points layout
                            pointsLayout.Children.Add(line);
                        }
                    }
                };
            }
            else
            {
                Console.WriteLine("The first child of outerStack is not an AbsoluteLayout or does not contain an Image and an AbsoluteLayout.");
            }
        }

        private void CategorizeItems()
        {
            CategorizedItems = new Dictionary<string, List<IShoppingItem>>
            {
                { "Dairy", new List<IShoppingItem>() },
                { "Produce", new List<IShoppingItem>() },
                { "Frozen", new List<IShoppingItem>() },
                { "Baked", new List<IShoppingItem>() },
                { "Pantry", new List<IShoppingItem>() },
                { "Nonfood", new List<IShoppingItem>() },
                { "Meat", new List<IShoppingItem>() }
            };

            foreach (var item in SelectedItems)
            {
                if (!string.IsNullOrEmpty(item.Category))
                {
                    if (CategorizedItems.TryGetValue(item.Category, out var categoryList))
                    {
                        categoryList.Add(item);
                    }
                    else
                    {
                        CategorizedItems[item.Category] = new List<IShoppingItem> { item };
                    }
                }
                else
                {
                    // Handle unknown categories
                    Console.WriteLine($"Unknown category: {item.Name}");
                }
            }
        }
        private void CreateCheckBoxes()
        {
            checkBoxStackLayout.Children.Clear();
            foreach (var item in SelectedItems)
            {
                var checkBox = new CheckBox();
                checkBox.SetBinding(CheckBox.IsCheckedProperty, new Binding("IsSelected", source: item));
                var label = new Label { Text = item.Name };
                checkBoxStackLayout.Children.Add(new StackLayout { Orientation = StackOrientation.Horizontal, Children = { checkBox, label } });
            }
        }

        public double width { get; private set; }
        public double height { get; private set; }
        //Code for orentation change format

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.width || height != this.height)
            {
                this.width = width;
                this.height = height;
                if (width > height)
                {
                    outerStack.Orientation = StackOrientation.Horizontal;
                }
                else
                {
                    outerStack.Orientation = StackOrientation.Vertical;
                }
            }
        }


    }


    //pathing algorithm
    public class GFG
    {
        public static int V = 100;

        int minDistance(int[] dist, bool[] sptSet)
        {
            int min = int.MaxValue, min_index = -1;

            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min)
                {
                    min = dist[v];
                    min_index = v;
                }

            return min_index;
        }

        void printSolution(int[] dist, int n)
        {
            Console.Write("Vertex     Distance from Source\n");
            for (int i = 0; i < V; i++)
                Console.Write(i + " \t\t " + dist[i] + "\n");
        }

        public void dijkstra(int[,] graph, List<int> points)
        {
            if (points == null || points.Count == 0)
            {
                Console.WriteLine("Invalid points");
                return;
            }

            int[] dist = new int[V];
            bool[] sptSet = new bool[V];

            for (int i = 0; i < V; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }

            foreach (int point in points)
            {
                if (point >= 0 && point < V) // Check bounds
                {
                    dist[point] = 0;

                    for (int count = 0; count < V - 1; count++)
                    {
                        int u = minDistance(dist, sptSet);

                        if (u >= 0 && u < V) // Check bounds
                        {
                            sptSet[u] = true;

                            for (int v = 0; v < V; v++)
                            {
                                if (!sptSet[v] && graph[u, v] != 0 &&
                                     dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                                {
                                    dist[v] = dist[u] + graph[u, v];
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Invalid point: {point}");
                }
            }

            printSolution(dist, V);
        }

        public List<int> GetPointsFromCategories(Dictionary<string, List<IShoppingItem>> categorizedItems)
        {
            var points = new List<int>();

            foreach (var category in categorizedItems.Keys)
            {
                switch (category)
                {
                    case "Dairy":
                        points.Add(21);
                        break;
                    case "Produce":
                        points.Add(41);
                        break;
                    case "Frozen":
                        points.Add(59);
                        break;
                    case "Baked":
                        points.Add(76);
                        break;
                    case "Pantry":
                        points.Add(26);
                        break;
                    case "Nonfood":
                        points.Add(66);
                        break;
                    case "Meat":
                        points.Add(49);
                        break;
                    default:
                        // Handle unknown categories
                        break;
                }
            }

            return points;
        }
    }

}



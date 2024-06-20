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
        private Dictionary<string, List<Items>> CategorizedItems { get; set; }
        public ObservableCollection<Items> SelectedItems { get; set; }
        public MapPage(ObservableCollection<Items> selectedItems)
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
            List<Tuple<int, int>> points = gfg.GetCategoryPoints(CategorizedItems);
            List<int> obstaclePoints = gfg.GetObstaclePoints();
            int[,] graph = new int[GFG.V, GFG.V]; // Define the graph
            gfg.dijkstra(graph, points, obstaclePoints);

            // Add points to the map image
            DrawToMap(points);
        }

        private bool pointsAdded = false;

        private void DrawToMap(List<Tuple<int, int>> points)
{
    // Get the map image and its layout
    if (outerStack.Children.Count > 0 && outerStack.Children[0] is AbsoluteLayout mapLayout)
    {
        Image mapImage = mapLayout.Children[0] as Image;
        AbsoluteLayout pointsLayout = mapLayout.Children[1] as AbsoluteLayout;

        // Clear any existing points and lines
        pointsLayout.Children.Clear();

        Color categoryMarkerColor = Color.Red;
        double categoryMarkerSize = 20;
        Color nonCategoryMarkerColor = Color.Green;
        double nonCategoryMarkerSize = 10;

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
                foreach (var point in points)
                {
                    int x = point.Item1;
                    int y = point.Item2;

                    // Check if the point is a category point
                    bool isCategoryPoint = false;
                    foreach (var category in CategorizedItems.Keys)
                    {
                        switch (category)
                        {
                            case "Dairy":
                                if (x == 26)
                                {
                                    isCategoryPoint = true;
                                    break;
                                }
                                break;
                            case "Produce":
                                if (x == 51)
                                {
                                    isCategoryPoint = true;
                                    break;
                                }
                                break;
                            case "Frozen":
                                if (x == 29)
                                {
                                    isCategoryPoint = true;
                                    break;
                                }
                                break;
                            case "Baked":
                                if (x == 59)
                                {
                                    isCategoryPoint = true;
                                    break;
                                }
                                break;
                            case "Pantry":
                                if (x == 43)
                                {
                                    isCategoryPoint = true;
                                    break;
                                }
                                break;
                            case "Nonfood":
                                if (x == 46)
                                {
                                    isCategoryPoint = true;
                                    break;
                                }
                                break;
                            case "Meat":
                                if (x == 24)
                                {
                                    isCategoryPoint = true;
                                    break;
                                }
                                break;
                            default:
                                // Handle unknown categories
                                break;
                        }
                    }

                    // Create a circular marker using the Ellipse shape
                    Ellipse marker;
                    if (isCategoryPoint)
                    {
                        marker = new Ellipse
                        {
                            WidthRequest = categoryMarkerSize,
                            HeightRequest = categoryMarkerSize,
                            Stroke = categoryMarkerColor,
                            StrokeThickness = 2,
                            Fill = categoryMarkerColor,
                            IsVisible = true // Ensure the marker is visible
                        };
                    }
                    else
                    {
                        marker = new Ellipse
                        {
                            WidthRequest = nonCategoryMarkerSize,
                            HeightRequest = nonCategoryMarkerSize,
                            Stroke = nonCategoryMarkerColor,
                            StrokeThickness = 2,
                            Fill = nonCategoryMarkerColor,
                            IsVisible = false // Ensure the marker is visible
                        };
                    }

                    // Calculate the position of the marker on the map image
                    double markerX = (x % numCols) * (mapWidth / numCols) + (marker.WidthRequest / 2);
                    double markerY = (x / numCols) * (mapHeight / numRows) + (marker.HeightRequest / 2);

                    // Create a Rectangle with the desired bounds
                    Xamarin.Forms.Rectangle bounds = new Xamarin.Forms.Rectangle(markerX, markerY, marker.WidthRequest, marker.HeightRequest);

                    // Add the marker to the points layout
                    AbsoluteLayout.SetLayoutBounds(marker, bounds);
                    pointsLayout.Children.Add(marker);
                }

                // Add lines between the points on the map image
                for (int i = 0; i < points.Count - 1; i++)
                {
                    var point1 = points[i];
                    var point2 = points[i + 1];

                    // Calculate the position of the points on the map image
                    double x1 = (point1.Item1 % numCols) * (mapWidth / numCols) + (lineThickness / 2);
                    double y1 = (point1.Item1 / numCols) * (mapHeight / numRows) + (lineThickness / 2);
                    double x2 = (point2.Item1 % numCols) * (mapWidth / numCols) + (lineThickness / 2);
                    double y2 = (point2.Item1 / numCols) * (mapHeight / numRows) + (lineThickness / 2);

                    // Create a Line shape to draw the line between the points
                    Line line = new Line
                    {
                        X1 = x1,
                        Y1 = y1,
                        X2 = x2,
                        Y2 = y2,
                        Stroke = lineColor,
                        StrokeThickness = lineThickness
                    };

                    // Add the line to the points layout
                    pointsLayout.Children.Add(line);
                }
            }
        };
    }
}

        private void CategorizeItems()
        {
            CategorizedItems = new Dictionary<string, List<Items>>
            {
                { "Dairy", new List<Items>() },
                { "Produce", new List<Items>() },
                { "Frozen", new List<Items>() },
                { "Baked", new List<Items>() },
                { "Pantry", new List<Items>() },
                { "Nonfood", new List<Items>() },
                { "Meat", new List<Items>() }
            };

            foreach (var item in SelectedItems)
            {
                if (!string.IsNullOrEmpty(item.Item_Category))
                {
                    if (CategorizedItems.TryGetValue(item.Item_Category, out var categoryList))
                    {
                        categoryList.Add(item);
                    }
                    else
                    {
                        CategorizedItems[item.Item_Category] = new List<Items> { item };
                    }
                }
                else
                {
                    // Handle unknown categories
                    Console.WriteLine($"Unknown category: {item.Item_Description}");
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
                var label = new Label { Text = item.Item_Description };
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

        public static int[,] graph;

        static GFG()
        {
            graph = new int[V, V];
            for (int i = 0; i < V; i++)
            {
                for (int j = 0; j < V; j++)
                {
                    graph[i, j] = i == j ? 0 : int.MaxValue; // default weight of 0 for self-loops, and infinity for other edges
                }
            }
            // Now specify weights for specific edges
            graph[0, 1] = 10;
            graph[0, 6] = 5;
            graph[1, 2] = 5;
            
            // Add more edges to the graph as needed
        }

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

        public void dijkstra(int[,] graph, List<Tuple<int, int>> points, List<int> obstaclePoints)
        {
            int[] dist = new int[points.Count];
            bool[] sptSet = new bool[points.Count];
            int[] parent = new int[points.Count]; // Array to store the parent of each node

            for (int i = 0; i < points.Count; i++)
            {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
                parent[i] = -1; // Initialize parent of each node as -1
            }

            foreach (var point in points)
            {
                if (!obstaclePoints.Contains(point.Item1) && !obstaclePoints.Contains(point.Item2)) // Check bounds and obstacle points
                {
                    dist[points.IndexOf(point)] = 0;

                    for (int count = 0; count < points.Count - 1; count++)
                    {
                        int u = minDistance(dist, sptSet);

                        if (u >= 0 && u < points.Count && !obstaclePoints.Contains(points[u].Item1) && !obstaclePoints.Contains(points[u].Item2)) // Check bounds and obstacle points
                        {
                            sptSet[u] = true;

                            for (int v = 0; v < points.Count; v++)
                            {
                                if (!sptSet[v] && graph[points[u].Item1, points[v].Item1] != 0 && graph[points[u].Item1, points[v].Item1] != int.MaxValue && dist[u] != int.MaxValue && dist[u] + graph[points[u].Item1, points[v].Item1] < dist[v])
                                {
                                    // Check if the edge connects to an obstacle node
                                    if (graph[points[u].Item1, points[v].Item1] != int.MaxValue && !obstaclePoints.Contains(points[v].Item1) && !obstaclePoints.Contains(points[v].Item2))
                                    {
                                        dist[v] = dist[u] + graph[points[u].Item1, points[v].Item1];
                                        parent[v] = u; // Update parent of node v as u
                                    }
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

            // Print the shortest path from the source node to all other nodes
            for (int i = 0; i < points.Count; i++)
            {
                if (i != points.IndexOf(points[0]) && !obstaclePoints.Contains(points[i].Item1) && !obstaclePoints.Contains(points[i].Item2)) // Skip the source node and obstacle points
                {
                    Console.Write("Shortest path from " + points[0].Item1 + " to " + points[i].Item1 + ": ");
                    printPath(parent, i);
                    Console.WriteLine();
                }
            }
        }

        private void printPath(int[] parent, int j)
        {
            if (parent[j] == -1)
            {
                return;
            }

            printPath(parent, parent[j]);

            Console.Write(j + " ");
        }


        public List<int> GetObstaclePoints()
        {
            var obstaclePoints = new List<int>
            {
                0, 1, 2, 3, 4, 5, 6, 7, 8, 9,
                10, 11, 12, 13, 14, 15, 16, 17, 18, 19,
                20, 33, 34, 35, 36, 37, 38,
                64, 65, 66, 67, 68, 69, 79,
                80, 81, 82, 83, 84, 85, 86, 87, 88, 89,
                90, 91, 92, 93, 94, 95, 96, 97, 98, 99
            };

            return obstaclePoints;
        }
        public List<Tuple<int, int>> GetCategoryPoints(Dictionary<string, List<Items>> categorizedItems)
        {
            var categoryPoints = new List<int>();
            var nonObstaclePoints = new List<int>();

            foreach (var category in categorizedItems.Keys)
            {
                switch (category)
                {
                    case "Dairy":
                        categoryPoints.Add(26);
                        break;
                    case "Produce":
                        categoryPoints.Add(51);
                        break;
                    case "Frozen":
                        categoryPoints.Add(29);
                        break;
                    case "Baked":
                        categoryPoints.Add(59);
                        break;
                    case "Pantry":
                        categoryPoints.Add(43);
                        break;
                    case "Nonfood":
                        categoryPoints.Add(46);
                        break;
                    case "Meat":
                        categoryPoints.Add(24);
                        break;
                    default:
                        // Handle unknown categories
                        break;
                }
            }

            // Generate all non-obstacle points
            for (int i = 0; i < GFG.V; i++)
            {
                if (!GetObstaclePoints().Contains(i) && !categoryPoints.Contains(i))
                {
                    nonObstaclePoints.Add(i);
                }
            }

            // Combine category points and non-obstacle points into tuples
            var categoryPointTuples = new List<Tuple<int, int>>();
            foreach (var categoryPoint in categoryPoints)
            {
                foreach (var nonObstaclePoint in nonObstaclePoints)
                {
                    categoryPointTuples.Add(new Tuple<int, int>(categoryPoint, nonObstaclePoint));
                }
            }

            return categoryPointTuples;
        }
    }

}



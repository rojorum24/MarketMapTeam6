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
        }
        private void CalculateShortestPath()
        {
            GFG gfg = new GFG();
            List<int> points = gfg.GetPointsFromCategories(CategorizedItems);
            int[,] graph = new int[GFG.V, GFG.V]; // Define the graph
            gfg.dijkstra(graph, points);
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
        public static int V = 7;

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
                dist[point] = 0;

                for (int count = 0; count < V - 1; count++)
                {
                    int u = minDistance(dist, sptSet);

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
                        points.Add(0);
                        break;
                    case "Produce":
                        points.Add(1);
                        break;
                    case "Frozen":
                        points.Add(2);
                        break;
                    case "Baked":
                        points.Add(3);
                        break;
                    case "Pantry":
                        points.Add(4);
                        break;
                    case "Nonfood":
                        points.Add(5);
                        break;
                    case "Meat":
                        points.Add(6);
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



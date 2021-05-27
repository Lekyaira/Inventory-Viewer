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

namespace InventoryViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int CountLowThreshold { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Program.Start(); // Initialize the program and start reading data
            lsbInventoryItems.ItemsSource = Program.inventory.itemsList; // Bind data to the listbox
            CountLowThreshold = Program.CountLowThreshold;
        }
    }
}

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
        TextBlock copyBox;

        public int CountLowThreshold { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            LoginDialog login = new LoginDialog();
            login.ShowDialog();
            if ((bool)login.DialogResult)
            {
                Program.Start(login.username, login.password); // Initialize the program and start reading data
                lsbInventoryItems.ItemsSource = Program.inventory.itemsList; // Bind data to the listbox
            } else
            {
                Close();
            }
        }

        private void CopyToClipboard_Click(object sender, RoutedEventArgs e)
        {
            TextBox box = new TextBox();
            box.Text = copyBox.Text;
            box.SelectAll();
            box.Copy();
        }

        private void ItemID_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            copyBox = (TextBlock)e.OriginalSource;
        }
    }
}

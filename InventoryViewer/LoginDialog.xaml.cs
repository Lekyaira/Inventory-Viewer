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
using System.Windows.Shapes;
using System.Security;

namespace InventoryViewer
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        public string username { get; private set; }

        public SecureString password { get; private set; }

        public LoginDialog()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Set our username and password properties
            username = txtUsername.Text;
            password = pwbPassword.SecurePassword;
            password.MakeReadOnly();  // We don't want password to be written to anymore.
            DialogResult = true; // User clicked "Login" button
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false; // User cancelled dialog
        }
    }
}

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

namespace InventoryViewer
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
    public partial class LoginDialog : Window
    {
        private static readonly DependencyProperty usernameProperty = DependencyProperty.Register("username", typeof(string), typeof(LoginDialog));
        public string username
        {
            get { return (string)GetValue(usernameProperty); }
            set { SetValue(usernameProperty, value);  }
        }

        private static readonly DependencyProperty passwordProperty = DependencyProperty.Register("password", typeof(string), typeof(LoginDialog));
        public string password
        {
            get { return (string)GetValue(passwordProperty); }
            set { SetValue(passwordProperty, value); }
        }

        private Program.Credentials credentials;

        public LoginDialog(Program.Credentials credentials)
        {
            InitializeComponent();

            this.credentials = credentials;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            credentials.username = username;
            credentials.password = password;
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            //Close();
        }
    }
}

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WorkersManagement
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            string hashed = SecurityHelper.HashPassword(password);

            using var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.Username == username && u.PasswordHash == hashed);

            if (user != null)
            {
                MessageBoxResult result = MessageBox.Show(
                    "Login successful!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                );

                if(result == MessageBoxResult.OK)
                {
                    var main = new Window1();
                    main.Show();


                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
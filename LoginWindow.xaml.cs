using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InventoryManagmentApplication
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginNameTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Имя пользователя")
            {
                textBox.Text = "";
            }
        }

        private void LoginNameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Имя пользователя";
            }
        }

        private void LoginPasswordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "Пароль")
            {
                textBox.Text = "";
            }
        }

        private void LoginPasswordTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Пароль";
            }
        }

        private void ShowError()
        {
            // Рассчитываем необходимую высоту
            ErrorTextBlock.Measure(new Size(ErrorContainer.ActualWidth, double.PositiveInfinity));
            double neededHeight = ErrorTextBlock.DesiredSize.Height;

            // Анимация появления
            var heightAnim = new DoubleAnimation(0, neededHeight, TimeSpan.FromSeconds(0.5));
            var opacityAnim = new DoubleAnimation(0, 10, TimeSpan.FromSeconds(0.5));

            ErrorContainer.BeginAnimation(HeightProperty, heightAnim);
            ErrorTextBlock.BeginAnimation(OpacityProperty, opacityAnim);
        }

        private void HideError()
        {
            var heightAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));
            var opacityAnim = new DoubleAnimation(0, TimeSpan.FromSeconds(0.3));

            ErrorContainer.BeginAnimation(HeightProperty, heightAnim);
            ErrorTextBlock.BeginAnimation(OpacityProperty, opacityAnim);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // Пример использования
            if ((LoginName.Text != "123") && (LoginPassword.Text != "123"))
            {
                ShowError();
            }
            else
            {
                HideError();
            }
        }
    }
}
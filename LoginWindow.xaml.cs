using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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
            string[] logistician = { "Logistician", "Logic" };

            string[] manager = { "Manager", "Manage" };

            string[] storekeeper = { "Storekeeper", "Store" };

            // Пример использования
            if (LoginName.Text == logistician[0] && LoginPassword.Text == logistician[1])
            {
                MessageBox.Show("Вход выполнен успешно (Логист)", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                LogisticianMainWindow mainWindow = new LogisticianMainWindow();

                mainWindow.Show();

                this.Close();

                HideError();
            }
            else if (LoginName.Text == manager[0] && LoginPassword.Text == manager[1])
            {
                MessageBox.Show("Вход выполнен успешно (Менеджер)", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                HideError();
            }
            else if (LoginName.Text == storekeeper[0] && LoginPassword.Text == storekeeper[1])
            {
                MessageBox.Show("Вход выполнен успешно (Кладовщик)", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);

                HideError();
            }
            else
            {
                ShowError();
            }
        }
    }
}
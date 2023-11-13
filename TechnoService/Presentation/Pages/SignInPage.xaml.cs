using System.Windows;
using System.Windows.Controls;
using TechnoService.Data;

namespace TechnoService.Presentation.Pages
{
    public partial class SignInPage : Page
    {
        private TechnoServiceWindow window;
        private TechnoServiceRepository technoServiceRepository;

        public SignInPage()
        {
            InitializeComponent();
            technoServiceRepository = ((App)Application.Current).GetTechnoServiceRepository();
            Loaded += SignInPageLoaded;
        }

        private void SignInPageLoaded(object sender, RoutedEventArgs e)
        {
            window = (TechnoServiceWindow)Window.GetWindow(this);
            window.changeWindowTitle("Окно авторизации");
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            string login = textBoxLogin.Text;
            string password = passwordBoxPassword.Password;

            if (login.Length == 0 && password.Length == 0)
            {
                MessageBox.Show("Заполните поля логина и пароля данными!", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxLogin.Focus();
                return;
            }

            if (login.Length == 0)
            {
                MessageBox.Show("Заполните поле логина данными!", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxLogin.Focus();
                return;
            }

            if (password.Length == 0)
            {
                MessageBox.Show("Заполните поле пароля данными!", "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
                passwordBoxPassword.Focus();
                return;
            }

            try
            {
                // Получение id пользователя.
                int userId = technoServiceRepository.CheckUserExists(login, password);
                NavigationService.Navigate(new RequestListPage(userId));
                textBoxLogin.Clear();
                passwordBoxPassword.Clear();
            }
            catch (System.Exception exception)
            {
                MessageBox.Show(exception.Message, "Ошибка ввода данных", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

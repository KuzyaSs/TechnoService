using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace TechnoService.Presentation.Pages
{
    public partial class SplashPage : Page
    {
        private TechnoServiceWindow window;

        public SplashPage()
        {
            InitializeComponent();
            Loaded += SplashPageLoaded;
        }

        private void SplashPageLoaded(object sender, RoutedEventArgs e)
        {
            window = (TechnoServiceWindow)Window.GetWindow(this);
            window.changeWindowTitle("Окно загрузки");
            Thread thread = new Thread(NavigateToSignInPage);
            thread.Start();
        }

        private void NavigateToSignInPage()
        {
            // Имитация загрузки.
            Thread.Sleep(2000);
             Application.Current.Dispatcher.Invoke(() =>
            {
                SignInPage signInPage = new SignInPage();
                NavigationService.Navigate(signInPage);
                window.SetUpDefaultWindow();
            });
        }
    }
}

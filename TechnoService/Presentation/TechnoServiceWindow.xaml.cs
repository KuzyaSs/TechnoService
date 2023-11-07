using System.ComponentModel;
using System.Windows;
using TechnoService.Presentation.Pages;

namespace TechnoService.Presentation
{
    public partial class TechnoServiceWindow : Window
    {
        public TechnoServiceWindow()
        {
            InitializeComponent();
            frame.Content = new SplashPage();
            Closing += TechnoServiceWindowClosed;
        }

        private void TechnoServiceWindowClosed(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти?", "Подтверждение закрытия", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        public void changeWindowTitle(string title)
        {
            Title = title;
        }

        public void SetUpWindowSize(int height, int width)
        {
            Height = height;
            MinHeight = height;
            Width = width;
            MinWidth = width;
        }

        public void SetUpDefaultWindow()
        {
            WindowStyle = WindowStyle.ThreeDBorderWindow;
            ResizeMode = ResizeMode.CanResize;
        }
    }
}

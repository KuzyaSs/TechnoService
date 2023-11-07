using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TechnoService.Data;

namespace TechnoService.Presentation.Pages
{
    public partial class RequestListPage : Page
    {
        private TechnoServiceWindow window;
        private TechnoServiceRepository technoServiceRepository;
        private User currentUser;
        private ObservableCollection<Request> requestList;

        public RequestListPage(int userId)
        {
            InitializeComponent();
            technoServiceRepository = ((App)Application.Current).GetTechnoServiceRepository();
            currentUser = technoServiceRepository.GetUserById(userId);
            Loaded += RequestListPageLoaded;
        }

        private void RequestListPageLoaded(object sender, RoutedEventArgs e)
        {
            window = (TechnoServiceWindow)Window.GetWindow(this);
            window.changeWindowTitle("Окно списка заявок");
            window.SetUpWindowSize(600, 800);
            SetUpUI();
        }

        private void SetUpUI()
        {
            if (currentUser.Role.Name == "Менеджер" || currentUser.Role.Name == "Исполнитель")
            {
                textBlockTitle.Text = "Заявки";
                buttonCreateRequest.Visibility = Visibility.Collapsed;
            }
            textBlockLogin.Text = currentUser.Login;

            requestList = new ObservableCollection<Request>();
            dataGridRequestList.ItemsSource = requestList;
            GetRequestsBySearchQuery();
        }

        private void NavigateToRequestDetailPage(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is Request request)
            {
                NavigationService.Navigate(new RequestDetailPage(currentUser, request.Id)); // currentUser, request.
            }
        }

        private void SignOut(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из аккаунта?", "Выход из аккаунта", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void NavigateToCreateRequestPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateRequestPage(currentUser.Id));
        }

        private void SetUpNotFoundText()
        {
            if (requestList.Count > 0)
            {
                textBlockNotFound.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (textBoxSearch.Text.Length == 0)
                {
                    textBlockNotFound.Text = "Заявки отсутствуют";
                }
                else
                {
                    textBlockNotFound.Text = "Ничего не найдено";
                }
                textBlockNotFound.Visibility = Visibility.Visible;
            }
        }

        private void GetRequestsBySearchQuery()
        {
            List<Request> newRequestList = new List<Request>();
            requestList.Clear();
            switch (currentUser.Role.Name)
            {
                case "Клиент":
                    newRequestList = technoServiceRepository.GetRequestsByClientSearchQuery(textBoxSearch.Text, currentUser.Id);
                    break;

                case "Менеджер":
                    newRequestList = technoServiceRepository.GetRequestsByManagerSearchQuery(textBoxSearch.Text);
                    break;

                case "Исполнитель":
                    newRequestList = technoServiceRepository.GetRequestsByContractorSearchQuery(textBoxSearch.Text, currentUser.Id);
                    break;
            }
            foreach (Request request in newRequestList)
            {
                requestList.Add(request);
            }
            SetUpNotFoundText();
        }

        private void SearchRequests(object sender, TextChangedEventArgs e)
        {
            GetRequestsBySearchQuery();
        }
    }
}
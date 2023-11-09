using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TechnoService.Data;
using TechnoService.Model;

namespace TechnoService.Presentation.Pages
{
    public partial class StatisticsPage : Page
    {
        private TechnoServiceWindow window;
        private TechnoServiceRepository technoServiceRepository;
        private ObservableCollection<FaultTypeStatistics> faultTypeStatisticsList;

        public StatisticsPage()
        {
            InitializeComponent();
            technoServiceRepository = ((App)Application.Current).GetTechnoServiceRepository();
            Loaded += StatisticsPageLoaded;
        }

        public void StatisticsPageLoaded(object sender, RoutedEventArgs e)
        {
            window = (TechnoServiceWindow)Window.GetWindow(this);
            window.changeWindowTitle("Окно статистики");
            SetUpUI();
        }

        private void SetUpUI()
        {
            faultTypeStatisticsList = new ObservableCollection<FaultTypeStatistics>();
            dataGridFaultTypeStatistics.ItemsSource = faultTypeStatisticsList;
            var newFaultTypeStatisticsList = technoServiceRepository.GetFaultTypeStatistics();
            foreach ( FaultTypeStatistics faultTypeStatistics in newFaultTypeStatisticsList )
            {
                faultTypeStatisticsList.Add(faultTypeStatistics);
            }
            SetUpNotFoundText();

            int numOfReadyRequests = technoServiceRepository.GetNumOfReadyRequests();
            textBlockNumOfReadyRequests.Text = $"Количество выполненных заявок: {numOfReadyRequests}";
            double avgCompletionTime = technoServiceRepository.GetAvgCompletionTime();
            if (avgCompletionTime >= 0)
            {
                textBlockAvgCompletionTime.Text = $"Среднее время выполнения заявок (часы): {avgCompletionTime}";
            }
            else
            {
                textBlockAvgCompletionTime.Visibility = Visibility.Collapsed;
            }
        }

        private void SetUpNotFoundText()
        {
            if (faultTypeStatisticsList.Count > 0)
            {
                textBlockNotFound.Visibility = Visibility.Collapsed;
            }
            else
            {
                textBlockNotFound.Visibility = Visibility.Visible;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

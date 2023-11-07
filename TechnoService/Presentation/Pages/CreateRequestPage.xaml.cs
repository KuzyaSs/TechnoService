using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TechnoService.Data;

namespace TechnoService.Presentation.Pages
{
    public partial class CreateRequestPage : Page
    {
        private TechnoServiceWindow window;
        private TechnoServiceRepository technoServiceRepository;
        private readonly int userId;
        private List<Equipment> equipmentList;
        private List<FaultType> faultTypeList;

        public CreateRequestPage(int userId)
        {
            InitializeComponent();
            technoServiceRepository = ((App)Application.Current).GetTechnoServiceRepository();
            this.userId = userId;
            Loaded += CreateRequestPageLoaded;
        }

        private void CreateRequestPageLoaded(object sender, RoutedEventArgs e)
        {
            window = (TechnoServiceWindow)Window.GetWindow(this);
            window.changeWindowTitle("Окно создания заявки");
            SetUpUI();
        }

        private void SetUpUI()
        {
            equipmentList = technoServiceRepository.GetAllEquipment();
            faultTypeList = technoServiceRepository.GetAllFaultTypes();
            comboBoxEquipment.ItemsSource = equipmentList;
            comboBoxFaultType.ItemsSource = faultTypeList;
            comboBoxEquipment.SelectedIndex = 0;
            comboBoxFaultType.SelectedIndex = 0;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите вернуться назад? Все несохранённые изменения будут потеряны!", "Подтверждение перехода", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void Send(object sender, RoutedEventArgs e)
        {
            int equipmentId = ((Equipment)comboBoxEquipment.SelectedItem).Id;
            int faultTypeId = ((FaultType)comboBoxFaultType.SelectedItem).Id;
            string description = textBoxDescription.Text;

            if (description.Length == 0)
            {
                MessageBox.Show("Опишите свою проблему!", "Ошибка создания заявки", MessageBoxButton.OK, MessageBoxImage.Error);
                textBoxDescription.Focus();
                return;
            }

            technoServiceRepository.AddRequest(userId, equipmentId, faultTypeId, description);
            MessageBox.Show("Заявка успешно создана!", "Создание заявки", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TechnoService.Data;

namespace TechnoService.Presentation.Pages
{
    public partial class RequestDetailPage : Page
    {
        private TechnoServiceWindow window;
        private TechnoServiceRepository technoServiceRepository;
        private User currentUser;
        private Request currentRequest;
        private List<Equipment> equipmentList;
        private List<FaultType> faultTypeList;
        private List<Status> statusList;
        private List<User> contractorList;

        public RequestDetailPage(User currentUser, int requestId)
        {
            InitializeComponent();
            technoServiceRepository = ((App)Application.Current).GetTechnoServiceRepository();
            this.currentUser = currentUser;
            currentRequest = technoServiceRepository.GetRequestById(requestId);
            Loaded += RequestDetailPageLoaded;
        }

        private void RequestDetailPageLoaded(object sender, RoutedEventArgs e)
        {
            window = (TechnoServiceWindow)Window.GetWindow(this);
            window.changeWindowTitle("Окно заявки");
            SetUpUI();
        }

        private void SetUpUI()
        {
            // Заполнение формы информацией о текущей заявке.
            equipmentList = technoServiceRepository.GetAllEquipment();
            faultTypeList = technoServiceRepository.GetAllFaultTypes();
            statusList = technoServiceRepository.GetAllStatuses();
            contractorList = technoServiceRepository.GetAllContractors();
            User absentContractor = new User { Id = -1, Login = "Отсутствует" };
            contractorList.Add(absentContractor);

            comboBoxEquipment.ItemsSource = equipmentList;
            comboBoxFaultType.ItemsSource = faultTypeList;
            comboBoxStatus.ItemsSource = statusList;
            comboBoxContractor.ItemsSource = contractorList;

            comboBoxEquipment.SelectedItem = currentRequest.Equipment;
            comboBoxFaultType.SelectedItem = currentRequest.FaultType;
            comboBoxStatus.SelectedItem = currentRequest.Status;
            if (currentRequest.ContractorId != null)
            {
                comboBoxContractor.SelectedItem = currentRequest.User1;
            }
            else
            {
                comboBoxContractor.SelectedIndex = contractorList.Count - 1;
            }

            textBlockTitle.Text = $"Заявка #{currentRequest.Id}";
            textBlockClient.Text = $"Клиент: {currentRequest.User.Login} (+{currentRequest.User.PhoneNumber})";
            textBlockPublicationDate.Text = $"Дата добавления: {currentRequest.PublicationDate}";
            datePickerEndDate.SelectedDate = currentRequest.EndDate;
            textBoxDescription.Text = currentRequest.Description;
            textBoxComment.Text = currentRequest.Comment;
            
            // Настройка интерфейса в зависимости от роли пользователя.
            switch (currentUser.Role.Name)
            {
                case "Клиент":
                    comboBoxEquipment.IsEnabled = false;
                    comboBoxFaultType.IsEnabled = false;
                    comboBoxStatus.IsEnabled = false;
                    datePickerEndDate.IsEnabled = false;
                    textBoxDescription.IsEnabled = false;
                    textBlockContractor.Visibility = Visibility.Collapsed;
                    comboBoxContractor.Visibility = Visibility.Collapsed;
                    textBlockComment.Visibility = Visibility.Collapsed;
                    textBoxComment.Visibility = Visibility.Collapsed;
                    textBlockClient.Visibility = Visibility.Collapsed;
                    buttonSave.Visibility = Visibility.Collapsed;

                    if (currentRequest.Status.Name != "Выполнено")
                    {
                        textBlockQrCode.Visibility = Visibility.Collapsed;
                        imageQrCode.Visibility = Visibility.Collapsed;
                    }
                    break;

                case "Менеджер":
                    textBoxComment.IsEnabled = false;
                    textBlockQrCode.Visibility = Visibility.Collapsed;
                    imageQrCode.Visibility = Visibility.Collapsed;
                    break;

                case "Исполнитель":
                    comboBoxEquipment.IsEnabled = false;
                    comboBoxFaultType.IsEnabled = false;
                    textBoxDescription.IsEnabled = false;
                    datePickerEndDate.IsEnabled = false;
                    textBlockContractor.Visibility = Visibility.Collapsed;
                    comboBoxContractor.Visibility = Visibility.Collapsed;
                    textBlockQrCode.Visibility = Visibility.Collapsed;
                    imageQrCode.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (currentUser.Role.Name == "Клиент")
            {
                NavigationService.GoBack();
                return;
            }

            MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите вернуться назад? Все несохранённые изменения будут потеряны!", "Подтверждение перехода", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            // Получение информации о редактированной заявке из формы.
            int equipmentId = ((Equipment)comboBoxEquipment.SelectedItem).Id;
            int faultTypeId = ((FaultType)comboBoxFaultType.SelectedItem).Id;
            int statusId = ((Status)comboBoxStatus.SelectedItem).Id;
            int contractorId = ((User)comboBoxContractor.SelectedItem).Id;
            string description = textBoxDescription.Text;
            string comment = textBoxComment.Text;
            DateTime? endDate = datePickerEndDate.SelectedDate;
            if (endDate < DateTime.Now)
            {
                MessageBoxResult result = MessageBox.Show($"Срок сдачи не может быть до {DateTime.Now}", "Некорректный срок сдачи", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Внесение изменений в заявку.
            technoServiceRepository.EditRequest(currentRequest.Id, equipmentId, faultTypeId, statusId, contractorId, description, comment, endDate);
            MessageBox.Show("Изменения успешно сохранены!", "Изменение заявки", MessageBoxButton.OK, MessageBoxImage.Information);
            NavigationService.GoBack();
        }
    }
}

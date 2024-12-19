using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Izosimova_Autoservice
{
    /// <summary>
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Service _currentServise = new Service();
        public AddEditPage(Service SelectedService)
        {
            InitializeComponent();
            if (SelectedService != null)
            {
                _currentServise = SelectedService;
            }
            DataContext = _currentServise;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentServise.Title))
            {
                errors.AppendLine("Укажите название услуги");
            }
            if (_currentServise.Cost <= 0)
            {
                errors.AppendLine("Укажите корректно стоимость услуги");
            }
            if (_currentServise.DiscountInt < 0)
            {
                errors.AppendLine("Укажите корректную скидку");
            }
            if (_currentServise.DiscountInt > 100)
            {
                errors.AppendLine("Укажите корректную скидку");
            }
            if (_currentServise.Duration == 0 )
            {
                errors.AppendLine("Укажите длительность услуг");
            }
            if (_currentServise.Duration > 240)
            {
                errors.AppendLine("Длительность не может быть больше 240 минут");
            }
            if (_currentServise.Duration < 0)
            {
                errors.AppendLine("Длительность не может быть меньше 0 минут");
            }
            if (errors.Length > 0) {
                MessageBox.Show(errors.ToString());
                return;
            }
            var allServise = Izosimova_autoserviceEntities1.GetContecxt().Service.ToList(); 
            allServise = allServise.Where(p => p.Title == _currentServise.Title).ToList();
            if (allServise.Count == 0)
            {
                if (_currentServise.ID == 0)
                {
                    Izosimova_autoserviceEntities1.GetContecxt().Service.Add(_currentServise);
                }
                try
                {
                    Izosimova_autoserviceEntities1.GetContecxt().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    Manager.MainFrame.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Уже существует такая услуга");
            }
            if(_currentServise.ID == 0)
            {
                Izosimova_autoserviceEntities1.GetContecxt().Service.Add(_currentServise);
            }
            try
            {
                Izosimova_autoserviceEntities1.GetContecxt().SaveChanges();
                MessageBox.Show("информация сонхранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
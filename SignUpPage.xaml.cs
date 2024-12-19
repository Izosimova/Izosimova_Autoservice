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
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        private Service _currentService=new Service();
        public SignUpPage(Service SelectedService)
        {
            InitializeComponent();
            if(SelectedService != null)
            {
                this._currentService = SelectedService;
            }
            DataContext = _currentService;
             
            var _currentClient = Izosimova_autoserviceEntities1.GetContecxt().Client.ToList();
            ComboClient.ItemsSource = _currentClient;
        }
        private ClientService _currentClientService = new ClientService();
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if(ComboClient.SelectedItem == null)
            {
                errors.AppendLine("Укажите ФИО клиента");
            }
            if(StartDate.Text == "")
            {
                errors.AppendLine("Укажите дату услуги");
            }
            if (TBStart.Text == "")
            {
                errors.AppendLine("Укажите время начала услуги");
            }
            string[] start = TBStart.Text.Split(new char[] { ':' }); int startHour = -1;
            int startMin = -1; if (TBStart.Text.Contains(":") && start[0].ToString() != "" && start[1].ToString() != "")
            {
                startHour = Convert.ToInt32(start[0].ToString());
                startMin = Convert.ToInt32(start[1].ToString());
            }
            if (startHour > 23 || startHour < 0 || startMin > 59 || startMin < 0)
                errors.AppendLine("Неправильно указано время");
            if (errors.Length > 0) {
                MessageBox.Show(errors.ToString());
                return;
            }
            _currentClientService.ClientID=ComboClient.SelectedIndex + 1;
            _currentClientService.ServiceID = _currentService.ID;
            _currentClientService.StartTime = Convert.ToDateTime(StartDate.Text + " " + TBStart.Text);
            if (_currentClientService.ID == 0) { 
            Izosimova_autoserviceEntities1.GetContecxt().ClientService.Add(_currentClientService);
            }
            try
            {
                Izosimova_autoserviceEntities1.GetContecxt().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void TBStart_TextChanged(object sender, TextChangedEventArgs e)
        {
            /* string s = TBStart.Text; 
             if (s.Length < 3 || !s.Contains(":"))
                 TBEnd.Text = "";
             else
             {
                 string[] start = s.Split(new char[] { ':' });
                 int startHour = 0; 
                 if (!s.StartsWith(":"))
                     startHour = Convert.ToInt32(start[0].ToString()) * 60; 
                 int startMin = 0;
                 if (!s.EndsWith(":"))
                 {
                     startMin = Convert.ToInt32(start[1].ToString());
                 }
                 int sum = startHour + startMin + _currentService.Duration;
                 int EndHour = sum / 60 % 24;
                 int EndMin = sum % 60;
                 if (EndMin < 10)
                 {
                     s = EndHour.ToString() + ":0" + EndMin.ToString();
                 }
                 else
                 {
                     s = EndHour.ToString() + ":" + EndMin.ToString();
                     TBEnd.Text = s;
                 }*/
            string s = TBStart.Text; if (s.Length < 3 || !s.Contains(":"))
                TBEnd.Text = "";
            else
            {
                string[] start = s.Split(new char[] { ':' });
                int startHour = 0; 
                if (!s.StartsWith(":"))
                    startHour = Convert.ToInt32(start[0].ToString()) * 60; 
                int startMin = 0;
                
                if (!s.EndsWith(":")) 
                    startMin = Convert.ToInt32(start[1].ToString());
               
                int sum = startHour + startMin + _currentService.Duration;
                int EndHour = sum / 60 % 24;
                int EndMin = sum % 60; 
                if (EndMin < 10)
                    s = EndHour.ToString() + ":0" + EndMin.ToString();
                else
                    s = EndHour.ToString() + ":" + EndMin.ToString(); 
                TBEnd.Text = s;
          
             
            }
        }

        }
    }


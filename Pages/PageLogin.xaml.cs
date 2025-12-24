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

namespace Exam5.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageLogin.xaml
    /// </summary>
    public partial class PageLogin : Page
    {
        public PageLogin()
        {
            InitializeComponent();
        }

        private void Log_in_Click(object sender, RoutedEventArgs e)
        {
            string loginText = login.Text;
            string passText = password.Password;

            using (var db = new BuldCompEntities())
            {
                var user = db.Set<Users>().FirstOrDefault(u => u.UserLogin == loginText && u.UserPassword == passText);
                if (user != null)
                {
                    if (user.RoleID == 1)
                    {
                        NavigationService.Navigate(new UserPage());
                        MessageBox.Show("Вошли как пользователь");
                    }
                    else if (user.RoleID == 2)
                    {
                        NavigationService.Navigate(new AdminPage());
                        MessageBox.Show("Вошли как админ");
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль");
                    }
                }
                else
                {
                    NavigationService.Navigate(new GuestPage());
                    MessageBox.Show("Вы вошли как гость! Войдите, чтобы оформить заказ");
                }
            }
        }
    }

}

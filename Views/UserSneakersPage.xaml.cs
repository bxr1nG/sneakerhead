using Sneakerhead.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace Sneakerhead.Views
{
    /// <summary>
    /// Логика взаимодействия для UserSneakersPage.xaml
    /// </summary>
    public partial class UserSneakersPage : Page
    {
        public UserSneakersPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                db.Sneakers.Load();
                sneakersGrid.ItemsSource = db.Sneakers.Local.ToBindingList();
            }
        }

        private void sneakersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sneaker sneaker = (Sneaker)sneakersGrid.SelectedItem;
            MainWindow window = Window.GetWindow(this) as MainWindow;
            window.setCurrentSneaker(sneaker);
            UserNewOrderPage userNewOrderPage = new UserNewOrderPage();
            NavigationService.Navigate(userNewOrderPage);
        }

        private void LikeButton_Click(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                Button cmd = (Button)sender;
                if (cmd.DataContext is Sneaker)
                {
                    MainWindow window = Window.GetWindow(this) as MainWindow;
                    User usr = window.getCurrentUser();
                    Sneaker snk = (Sneaker)cmd.DataContext;

                    db.Users.Load();
                    User user = db.Users.Include(u => u.Sneaker).First(u => u.ID == usr.ID);

                    db.Sneakers.Load();
                    Sneaker sneaker = db.Sneakers.Include(u => u.User).First(u => u.ID == snk.ID);

                    if (user.Sneaker.Contains(sneaker))
                    {
                        MessageBox.Show("Sneaker already in your wish list", "Sneakerhead", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        user.Sneaker.Add(sneaker);
                        db.SaveChanges();
                        MessageBox.Show("Sneaker added to your wish list", "Sneakerhead", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }
    }
}

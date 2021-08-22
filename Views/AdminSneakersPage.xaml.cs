using Microsoft.Win32;
using Sneakerhead.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sneakerhead.Views
{
    /// <summary>
    /// Логика взаимодействия для AdminSneakersPage.xaml
    /// </summary>
    public partial class AdminSneakersPage : Page
    {
        public AdminSneakersPage()
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

        private void ImageSetter_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (op.ShowDialog() == true && sneakersGrid.SelectedItem != null)
            {
                Sneaker selectedSnk = (Sneaker)sneakerGrid.DataContext;
                selectedSnk.Image = File.ReadAllBytes(op.FileName);
                sneakerGrid.DataContext = null;
                sneakerGrid.DataContext = selectedSnk;
            }
        }

        private void sneakersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sneakersGrid.SelectedItem != null)
            {
                Sneaker selectedSnk = (Sneaker)sneakersGrid.SelectedItem;
                sneakerGrid.DataContext = selectedSnk;
            }
        }

        private void NewSneaker_Click(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                OpenFileDialog op = new OpenFileDialog();
                op.Title = "Select a picture";
                op.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                if (op.ShowDialog() == true)
                {
                    Sneaker sneaker = new Sneaker();
                    sneaker.Image = File.ReadAllBytes(op.FileName);

                    sneaker.Model = op.FileName.Substring(op.FileName.Length - 10);
                    sneaker.Brand = op.FileName.Substring(op.FileName.Length - 10);
                    sneaker.Price = 0;

                    db.Sneakers.Add(sneaker);
                    db.SaveChanges();
                    sneakersGrid.ItemsSource = null;
                    db.Sneakers.Load();
                    sneakersGrid.ItemsSource = db.Sneakers.Local.ToBindingList().OrderBy(u => u.ID);
                    sneakerGrid.DataContext = null;
                }
            }
        }

        private void SaveSneaker_Click(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                if (sneakersGrid.SelectedItem != null && checkTextBoxes())
                {
                    Sneaker sneaker = (Sneaker)sneakerGrid.DataContext;
                    Sneaker mySneaker = db.Sneakers.Find(sneaker.ID);

                    mySneaker.Brand = sneaker.Brand;
                    mySneaker.Model = sneaker.Model;
                    mySneaker.Price = sneaker.Price;
                    mySneaker.Image = sneaker.Image;

                    db.SaveChanges();
                    sneakersGrid.ItemsSource = null;
                    db.Sneakers.Load();
                    sneakersGrid.ItemsSource = db.Sneakers.Local.ToBindingList().OrderBy(u => u.ID);
                    sneakerGrid.DataContext = null;
                }
            }
        }

        private void DelSneaker_Click(object sender, RoutedEventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                if (sneakersGrid.SelectedItem != null)
                {
                    Sneaker sneaker = (Sneaker)sneakerGrid.DataContext;
                    Sneaker mySneaker = db.Sneakers.Find(sneaker.ID);

                    db.Sneakers.Remove(mySneaker);
                    db.SaveChanges();

                    sneakersGrid.ItemsSource = null;
                    db.Sneakers.Load();
                    sneakersGrid.ItemsSource = db.Sneakers.Local.ToBindingList().OrderBy(u => u.ID);

                    sneakerGrid.DataContext = null;
                }
            }
        }

        public void SetError(TextBox textBox, string errorName)
        {
            ToolTip tooltip = new ToolTip { Content = errorName };
            textBox.ToolTip = tooltip;
            textBox.Background = Brushes.Yellow;
        }

        public void SetValid(TextBox textBox)
        {
            textBox.ToolTip = null;
            textBox.Background = null;
        }

        public bool checkTextBoxes()
        {
            bool check = true;
            clearTextBoxes();
            Regex nameCheck = new Regex(@"[A-Za-z][A-Za-z0-9._]{3,20}");

            if (Brand.Text == "")
            {
                SetError(Brand, "Required field");
                check = false;
            }
            else if (!nameCheck.IsMatch(Brand.Text))
            {
                SetError(Brand, "Invalid input format");
                check = false;
            }

            if (Model.Text == "")
            {
                SetError(Model, "Required field");
                check = false;
            }
            else if (!nameCheck.IsMatch(Model.Text))
            {
                SetError(Model, "Invalid input format");
                check = false;
            }

            if (Price.Text == "")
            {
                SetError(Price, "Required field");
                check = false;
            }

            return check;
        }

        public void clearTextBoxes()
        {
            SetValid(Brand);
            SetValid(Model);
            SetValid(Price);
        }
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DesktopUI_4227
{
    public partial class AddUserViewModel:ObservableObject
    {
        [ObservableProperty]
        public string firstname;
        [ObservableProperty]
        public string lastname;
        [ObservableProperty]
        public int age;
        [ObservableProperty]
        public string dateofbirth;

        [ObservableProperty]
        public double gpa;

        [ObservableProperty]
        public BitmapImage image;
        [ObservableProperty]
        public string title;

        public User Student { get; set; }
        public Action CloseAction { get;  set; }


        public AddUserViewModel()
        {
            Title = "Add User";
        }


        public AddUserViewModel(User s)
        {
            Title = "Edit User";
            Student = s;
            firstname =Student.FirstName;
            lastname = Student.LastName;
            age =Student.Age;
            dateofbirth = Student.DateofBirth;
            gpa = Student.GPA;
            image = Student.Image;
        }

        [RelayCommand]
        public void UploadPhoto()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.bmp; *.png; *.jpg";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                image = new BitmapImage(new Uri(dialog.FileName));

                MessageBox.Show("Image successfully uploded!", "successfull");
            }
        }



        [RelayCommand]
        public void Save()
        {



            if (gpa < 0.00 || gpa > 4.00)
            {
                MessageBox.Show("GPA value must be between 0-4", "Error!!!");
                return;
            }
            if (Student == null)
            {

                Student = new User()
                {
                    FirstName = firstname,
                    LastName = lastname,
                    Age = age,
                    DateofBirth = dateofbirth,
                    Image = image,

                    GPA = gpa

                };
            }
            else
            {

                Student.FirstName = Firstname;
                Student.LastName = Lastname;
                Student.Age = Age;
                Student.GPA = Gpa;
                Student.Image = Image;
                Student.DateofBirth = Dateofbirth;
            }

            if (Student.FirstName == null  || Student.LastName==null  || Student.Image==null || Student.Age==null  || Student.DateofBirth==null )
            {
                MessageBox.Show("Please enter your full details..", "Error");

            }
            Application.Current.MainWindow.Show();

            if (Student.DateofBirth != null)
            {
                DateTime parsedDate;
                if (!DateTime.TryParseExact(Dateofbirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    MessageBox.Show("Invalid date format. Please use the format dd/MM/yyyy", "Error");
                    return;
                }
            }

            

            if (Student.FirstName != null)
            {
                CloseAction();

            }
            Application.Current.MainWindow.Show();




        }

       
    }
}

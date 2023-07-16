using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DesktopUI_4227
{
    public partial class MainWindowViewModel : ObservableObject
    {
        private ObservableCollection<User> users;
        private User selectedStudent;

        public ObservableCollection<User> Users
        {
            get { return users; }
            set { SetProperty(ref users, value); }
        }

        public User SelectedStudent
        {
            get { return selectedStudent; }
            set { SetProperty(ref selectedStudent, value); }
        }

        private ObservableCollection<User> filteredUsers;
        public ObservableCollection<User> FilteredUsers
        {
            get { return filteredUsers; }
            set { SetProperty(ref filteredUsers, value); }
        }

        private string searchQuery;
        public string SearchQuery
        {
            get { return searchQuery; }
            set
            {
                SetProperty(ref searchQuery, value);
                FilterUsers();
            }
        }

        public MainWindowViewModel()
        {
            Users = new ObservableCollection<User>(GetUser());
            FilteredUsers = Users;
        }

        private void FilterUsers()
        {
            if (string.IsNullOrEmpty(SearchQuery))
            {
                FilteredUsers = Users;
            }
            else
            {
                FilteredUsers = new ObservableCollection<User>(Users.Where(u =>
                    u.FirstName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    u.LastName.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    IsValidDateFormat(u.DateofBirth, SearchQuery) ||
                    u.Age.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    u.GPA.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)));
            }
        }

        private bool IsValidDateFormat(string dateOfBirth, string searchQuery)
        {
            if (DateTime.TryParseExact(dateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                var formattedSearchQuery = parsedDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                return formattedSearchQuery.Contains(searchQuery, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        [RelayCommand]
        public void AddStudent()
        {
            var st = new AddUserViewModel();
            AddUserWindow window = new AddUserWindow(st);
            window.ShowDialog();

            if (!string.IsNullOrEmpty(st.Student?.FirstName))
            {
                Users.Add(st.Student);
            }
        }

        [RelayCommand]
        public void Delete()
        {
            if (SelectedStudent != null)
            {
                string name = SelectedStudent.FirstName;
                MessageBoxResult messageBoxResult = MessageBox.Show($"Are you sure you want to delete {name}?", "Confirmation", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Users.Remove(SelectedStudent);
                    MessageBox.Show($"{name} has been deleted successfully.", "DELETE");
                }
            }
            else
            {
                MessageBox.Show("Please select a student before deleting.", "Error");
            }
        }

        [RelayCommand]
        public void EditStudent()
        {
            if (SelectedStudent != null)
            {
                var st = new AddUserViewModel(SelectedStudent);
                var window = new AddUserWindow(st);

                window.ShowDialog();

                int index = Users.IndexOf(SelectedStudent);
                if (index != -1)
                {
                    Users[index] = st.Student;
                }
                users.RemoveAt(index);
                users.Insert(index, st.Student);

            }
            else
            {
                MessageBox.Show("Please select a student before editing.", "Warning");
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        private void SetProperty<T>(ref T field, T value, [System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, value))
            {
                field = value;
                RaisePropertyChanged(propertyName);
            }
        }

       
        private ObservableCollection<User> GetUser()
        {
            ObservableCollection<User> users = new ObservableCollection<User>();

            //  user data here
            BitmapImage image1 = new BitmapImage(new Uri("/Images/1.png", UriKind.Relative));
            users.Add(new User("Kumar", "Sangakkara", 50, "12/7/1973", 2.5, image1));

            BitmapImage image2 = new BitmapImage(new Uri("/Images/2.png", UriKind.Relative));
            users.Add(new User("Lasith", "Malinga", 48, "21/1/1975", 3.75, image2));

            BitmapImage image3 = new BitmapImage(new Uri("/Images/3.png", UriKind.Relative));
            users.Add(new User("Mahela", "Jeyawardana", 49, "14/10/1974", 0.0, image3));

            BitmapImage image4 = new BitmapImage(new Uri("/Images/4.png", UriKind.Relative));
            users.Add(new User("Dasun", "Shanaka", 29, "19/11/1994", 0.0, image4));

            BitmapImage image5 = new BitmapImage(new Uri("/Images/4.png", UriKind.Relative));
            users.Add(new User("Angelo", "Mathews", 34, "29/1/1989", 0.0, image5));

            return users;
        }
    }
}

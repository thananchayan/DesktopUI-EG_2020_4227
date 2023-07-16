using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DesktopUI_4227
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string DateofBirth { get; set; }
        public double GPA { get; set; }
        public BitmapImage Image { get; set; }

        public User()
        {

        }

        public User(string firstName, string lastName, int age, string dateofBirth, double gPA, BitmapImage image)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            DateofBirth = dateofBirth;
            GPA = gPA;
            Image = image;
        }

        public String ImagePath
        {
            get { return $"/Images/{Image}"; }
        }


    }
}


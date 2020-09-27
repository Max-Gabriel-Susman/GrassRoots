using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GrassRoots
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<User> Users { get; private set; }

        public MainPage()
        {
            InitializeComponent();

            Users = new ObservableCollection<User>();
            Users.Add(new User
            {
                Username = "one",
            });

            Users.Add(new User
            {
                Username = "two",
            });

            Users.Add(new User
            {
                Username = "three",
            });

            foreach (User user in Users)
                System.Diagnostics.Debug.WriteLine(user.Username);

            BindingContext = this;
        }
    }
}
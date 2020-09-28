using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GrassRoots.Models;
using GrassRoots.Services;
using Xamarin.Forms;


namespace GrassRoots.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        // FIELDS
        List<DropsUser> users;

        // CONSTRUCTORS
        public MainPageViewModel()
        {

            //Users = new ObservableCollection<User>();

            //Users.Add(new User() { Username = "a" });

            //Users.Add(new User() { Username = "b" });

            //Users.Add(new User() { Username = "c" });

            System.Diagnostics.Debug.WriteLine("MVVM active");

            Users = new List<DropsUser>();

            //async () => await ExecuteRefreshCommand();

            System.Diagnostics.Debug.WriteLine("Users collections instantiated, Refreshcommand pending");

            // the statement block of the lambda expression isn't excecuting, wtf
            // is executing the right word in this context?
            System.Diagnostics.Debug.WriteLine(RefreshCommand == null);
            // RefreshCommand is succsesfully instantiated, but the work isn't being done

            // I think this is an object initializer and it's scope itsn't being touched
            RefreshCommand = new Command(async () =>
            {
                System.Diagnostics.Debug.WriteLine("in scope that's dope");
                await ExecuteRefreshCommand();
            });

            // use breakpoints to step through the code?


            System.Diagnostics.Debug.WriteLine(RefreshCommand == null);

            
            System.Diagnostics.Debug.WriteLine("RefreshCommand Instantiation attempt complete");
            //CompleteCommand = new Command<DropsUser>(async (item) => await ExecuteCompleteCommand(item));

        }

        // PROPERTIES
        //public ObservableCollection<User> Users { get; set; }

        //  we need to find where set prp
        public List<DropsUser> Users { get => users; set => SetProperty(ref users, value); }

        public ICommand RefreshCommand { get; }
        //public ICommand CompleteCommand { get; }


        // EVENT HANDLERS
        async Task ExecuteRefreshCommand()
            {
            if (IsBusy)
            {
                System.Diagnostics.Debug.WriteLine("was busy");
                return;
            }

            //{
            //    System.Diagnostics.Debug.WriteLine("was busy");
            //    return;
            //}

            System.Diagnostics.Debug.WriteLine("wasn't busy");
            IsBusy = true;

            // does the block containing these guys need to be asynchronous?
            try
            {
                // I think this is what i need to keep my eye on
                System.Diagnostics.Debug.WriteLine("try");
                Users = await CosmosDBService.GetToDoItems();
            }
            finally
            {
                System.Diagnostics.Debug.WriteLine("finally");
                IsBusy = false;
            }
        }

        // synchronous version
        //void ExecuteRefreshCommand()
        //{
        //    if (IsBusy)
        //    {
        //        System.Diagnostics.Debug.WriteLine("was busy");
        //        return;
        //    }

        //    //{
        //    //    System.Diagnostics.Debug.WriteLine("was busy");
        //    //    return;
        //    //}

        //    System.Diagnostics.Debug.WriteLine("wasn't busy");
        //    IsBusy = true;

        //    // does the block containing these guys need to be asynchronous?
        //    try
        //    {
        //        // I think this is what i need to keep my eye on
        //        System.Diagnostics.Debug.WriteLine("try");
        //        Users = CosmosDBService.GetToDoItems();
        //    }
        //    finally
        //    {
        //        System.Diagnostics.Debug.WriteLine("finally");
        //        IsBusy = false;
        //    }
        //}

        // i dont' think we'll end up needing this one
        //async Task ExecuteCompleteCommand(ToDoItem item)
        //{
        //    if (IsBusy)
        //        return;

        //    IsBusy = true;

        //    try
        //    {
        //        await CosmosDBService.CompleteToDoItem(item);
        //        ToDoItems = await CosmosDBService.GetToDoItems();
        //    }
        //    finally
        //    {
        //        IsBusy = false;
        //    }
        //}


    }
}

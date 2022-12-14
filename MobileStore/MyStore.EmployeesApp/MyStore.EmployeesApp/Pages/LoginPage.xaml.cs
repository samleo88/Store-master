using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MyStore.Services;
using MyStore.Models;
using MyStore.EmployeesApp.Services;

namespace MyStore.EmployeesApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void SignInButton_Clicked(object sender, EventArgs e)
        {
            var username = UserNameEntry.Text;
            var password = PasswordEntry.Text;

            var employee = await StoreWebApiClient.Instance.Login(username, password);

            if (employee != default(EmployeeDTO))
            {
                App.CurrentEmployee = employee;

                App.SignalRService = new SignalRService();
                await App.SignalRService.InitEmployee(App.CurrentEmployee);

                await DisplayAlert("Welcome!", $"Welcome {employee.FullName}", "OK");
                await Navigation.PushAsync(new CustomerListPage());
            }
            else
            {
                await DisplayAlert("Error!", "Wrong credentials!", "OK");
            }
        }
    }
}
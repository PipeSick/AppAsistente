using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AppAsistente.Views.AccessView;
using AppAsistente.Views;
using AppAsistente.Views.UsersView;
using AppAsistente.Views.VehiculosView;
using AppAsistente.Views.ConductorView;
using AppAsistente.Views.Establecimiento;
using AppAsistente.Views.AlumnosView;

namespace AppAsistente
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

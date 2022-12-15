using AppAsistente.Views.AlumnosView;
using AppAsistente.Views.ConductorView;
using AppAsistente.Views.Establecimiento;
using AppAsistente.Views.UsersView;
using AppAsistente.Views.VehiculosView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.IndexTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfigView : ContentPage
    {
        public ConfigView()
        {
            InitializeComponent();
            
        }
        void AbrirCrudUsers(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new UsersIndex());
        }

        void AbrirIndexVehiculos(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VehiculoIndex());
        }
        void AbrirIndexConductores(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ConductorIndex());
        }
        void AbrirIndexEstablecimientos(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EstablecimientoIndex());
        }
        void AbrirIndexAlumnos(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AlumnosIndex());
        }
    }    
}
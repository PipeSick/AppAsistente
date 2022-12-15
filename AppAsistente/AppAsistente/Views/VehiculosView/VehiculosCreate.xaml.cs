using AppAsistente.Models;
using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.VehiculosView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiculosCreate : ContentPage
    {
        VehiculoViewModel repository = new VehiculoViewModel();
        ConductorViewModel repositoryConductor = new ConductorViewModel();
        public VehiculosCreate()
        {
            InitializeComponent();
            BindingContext = this;
            mostrarconductores();         


        }

        public async void mostrarconductores()
        {
            var conductores = await repositoryConductor.GetAll();
            pickerCountry.ItemsSource = conductores;

        }
        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            string patente = TxtPatente.Text;
            string año = TxtAñoFabricacion.Text;
            ConductorModel conmodel = pickerCountry.SelectedItem as ConductorModel;

            string rutC = conmodel.Rut;

            if (string.IsNullOrEmpty(patente))
            {
                await DisplayAlert("Warning", "Por Favor ingresa la patente", "Cancel");
            }
            if (string.IsNullOrEmpty(año))
            {
                await DisplayAlert("Warning", "Por Favor ingresa el año de fabricación", "Cancel");
            }
            if (string.IsNullOrEmpty(rutC))
            {
                await DisplayAlert("Warning", "Por favor ingrese el rut del conductor", "Cancel");
            }
            VehiculoModel vehiculo = new VehiculoModel();
            vehiculo.Patente = patente;
            vehiculo.AñoFabricacion = int.Parse(año);
            vehiculo.ConductorId = rutC;


            var isSaved = await repository.SaveVehiculo(vehiculo);
            if (isSaved)
            {
                await DisplayAlert("Information", "El vehiculo se ha agregado correctamente", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Error en el guardado del vehiculo", "Ok");
            }

        }
    }
}
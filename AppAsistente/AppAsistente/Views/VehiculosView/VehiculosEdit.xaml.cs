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
    public partial class VehiculosEdit : ContentPage
    {
        ConductorViewModel repositoryConductor = new ConductorViewModel();
        VehiculoViewModel repository = new VehiculoViewModel();
        public VehiculosEdit(VehiculoModel v)
        {
            InitializeComponent();
            BindingContext = this;
            mostrarconductores();
            TxtPatente.Text = v.Patente;
            TxtAñoFabricacion.Text = v.AñoFabricacion.ToString();
            pickerCountry.SelectedItem = v.ConductorId;
            TxtId.Text = v.Id;
        }
        public async void mostrarconductores()
        {
            var conductores = await repositoryConductor.GetAll();
            pickerCountry.ItemsSource = conductores;

        }
        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string patente = TxtPatente.Text;
            string añoF = TxtAñoFabricacion.Text;
            ConductorModel conmodel = pickerCountry.SelectedItem as ConductorModel;
            string Conductor = conmodel.Rut;
            string id = TxtId.Text;

            if (string.IsNullOrEmpty(patente))
            {
                await DisplayAlert("Warning", "Por favor ingresa patente", "Cancel");
            }
            if (string.IsNullOrEmpty(añoF))
            {
                await DisplayAlert("Warning", "Por favor ingresa el año de fabricación", "Cancel");
            }
            if (string.IsNullOrEmpty(Conductor))
            {
                await DisplayAlert("Warning", "Por favor ingresa el rut del conductor", "Cancel");
            }


            VehiculoModel v = new VehiculoModel();
            v.Id = TxtId.Text;
            v.Patente = patente;
            v.AñoFabricacion = int.Parse(añoF);
            v.ConductorId = Conductor;


            bool isUpdated = await repository.Update(v);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Error modificando el vehiculo.", "Cancel");
            }
        }
    }
}
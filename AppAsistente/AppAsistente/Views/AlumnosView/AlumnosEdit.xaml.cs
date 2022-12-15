using AppAsistente.Models;
using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.AlumnosView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlumnosEdit : ContentPage
    {
        AlumnoViewModel repository = new AlumnoViewModel();
        EstablecimientoViewModel repositoryEstablecimiento = new EstablecimientoViewModel();
        public AlumnosEdit(AlumnoModel A)
        {
            InitializeComponent();
            BindingContext = this;
            mostrarestablecimientos();
            TxtNombres.Text = A.Nombre;
            TxtApellidos.Text = A.Apellidos;
            TxtRut.Text = A.RutAlu;
            TxtId.Text = A.Id;
        }
        public async void mostrarestablecimientos()
        {
            var establecimientos = await repositoryEstablecimiento.GetAll();
            pickerEstablecimiento.ItemsSource = establecimientos;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string nombre = TxtNombres.Text;
            string apellidos = TxtApellidos.Text;
            string rut = TxtRut.Text;
            EstablecimientoModel estamodel = pickerEstablecimiento.SelectedItem as EstablecimientoModel;
            string establecimientoId = estamodel.Id;
            string id = TxtId.Text;

            if (string.IsNullOrEmpty(nombre))
            {
                await DisplayAlert("Warning", "Por Favor ingresa los nombres", "Cancel");
            }
            if (string.IsNullOrEmpty(apellidos))
            {
                await DisplayAlert("Warning", "Por Favor ingresa los apellidos", "Cancel");
            }
            if (string.IsNullOrEmpty(rut))
            {
                await DisplayAlert("Warning", "Por favor ingrese el rut.", "Cancel");
            }
            if (string.IsNullOrEmpty(establecimientoId))
            {
                await DisplayAlert("Warning", "Por favor seleccione el establecimiento.", "Cancel");
            }
            AlumnoModel a = new AlumnoModel();
            a.Nombre = nombre;
            a.RutAlu = rut;
            a.Apellidos = apellidos;
            a.EstableciminetoId = establecimientoId;
            a.Id = id;
            bool isUpdated = await repository.Update(a);
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
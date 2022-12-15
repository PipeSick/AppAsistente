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
    public partial class AlumnosCreate : ContentPage
    {
        AlumnoViewModel repository = new AlumnoViewModel();
        EstablecimientoViewModel repositoryEstablecimiento = new EstablecimientoViewModel();
        public AlumnosCreate()
        {
            InitializeComponent();
            BindingContext = this;
            mostrarestablecimientos();
        }

        public async void mostrarestablecimientos()
        {
            var establecimientos = await repositoryEstablecimiento.GetAll();
            pickerEstablecimiento.ItemsSource = establecimientos;
        }

        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            string nombre = TxtNombres.Text;
            string apellidos = TxtApellidos.Text;
            string rut = TxtRut.Text;
            EstablecimientoModel estamodel = pickerEstablecimiento.SelectedItem as EstablecimientoModel;
            string establecimientoId = estamodel.Id;

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

            var isSaved = await repository.Save(a);
            if (isSaved)
            {
                await DisplayAlert("Information", "El Alumno se ha agregado correctamente", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Error en el guardado del vehiculo", "Ok");
            }

        }
    }
}
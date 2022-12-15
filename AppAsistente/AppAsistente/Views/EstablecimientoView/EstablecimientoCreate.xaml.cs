using AppAsistente.Models;
using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.Establecimiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablecimientoCreate : ContentPage
    {
        EstablecimientoViewModel repository = new EstablecimientoViewModel();
        public EstablecimientoCreate()
        {
            InitializeComponent();
        }
        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            string nombre = TxtNombre.Text;
            string direccion = TxtDireccion.Text;
            string director = TxtDirector.Text;
            string telefono = TxtTelefono.Text;

            if (string.IsNullOrEmpty(nombre))
            {
                await DisplayAlert("Warning", "Por Favor ingresa el nombre.", "Cancel");
            }
            if (string.IsNullOrEmpty(direccion))
            {
                await DisplayAlert("Warning", "Por Favor ingresa la dirección.", "Cancel");
            }
            if (string.IsNullOrEmpty(director))
            {
                await DisplayAlert("Warning", "Por favor ingrese el nombre del director.", "Cancel");
            }
            if (string.IsNullOrEmpty(telefono))
            {
                await DisplayAlert("Warning", "Por favor ingrese el numero de contacto.", "Cancel");
            }
            EstablecimientoModel estable = new EstablecimientoModel();
            estable.Nombre = nombre;
            estable.Direccion = direccion;
            estable.Director = director;
            estable.Contacto = telefono;


            var isSaved = await repository.Save(estable);
            if (isSaved)
            {
                await DisplayAlert("Information", "El Establecimiento se ha agregado correctamente", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Error en el guardado del Establecimiento", "Ok");
            }

        }
    }
}
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
    public partial class EstablecimientoEdit : ContentPage
    {
        EstablecimientoViewModel repository = new EstablecimientoViewModel();
        public EstablecimientoEdit(EstablecimientoModel e)
        {
            InitializeComponent();
            TxtNombre.Text = e.Nombre;
            TxtDireccion.Text = e.Direccion;
            TxtDirector.Text = e.Director;
            TxtTelefono.Text = e.Contacto;
            txtid.Text = e.Id;
        }
        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string nombre = TxtNombre.Text;
            string direccion = TxtDireccion.Text;
            string director = TxtDirector.Text;
            string contacto = TxtTelefono.Text;
            string id = txtid.Text;

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
            if (string.IsNullOrEmpty(contacto))
            {
                await DisplayAlert("Warning", "Por favor ingrese el numero de contacto.", "Cancel");
            }
            EstablecimientoModel estable = new EstablecimientoModel();
            estable.Nombre = nombre;
            estable.Direccion = direccion;
            estable.Director = director;
            estable.Contacto = contacto;
            estable.Id = id;

            bool isUpdated = await repository.Update(estable);
            if (isUpdated)
            {
                
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Error modificando el establecimiento.", "Cancel");
            }
        }
    }
}
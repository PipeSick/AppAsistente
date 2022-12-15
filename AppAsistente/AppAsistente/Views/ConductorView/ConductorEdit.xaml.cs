using AppAsistente.Models;
using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.ConductorView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConductorEdit : ContentPage
    {
        ConductorViewModel repository = new ConductorViewModel();
        public ConductorEdit(ConductorModel c)
        {
            InitializeComponent();
            TxtRut.Text = c.Rut;
            TxtNombres.Text = c.Nombres;
            TxtApellidos.Text = c.apellidos;
            TxtContacto.Text = c.NumeroContacto;
            TxtId.Text = c.Id;
        }

        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string rut = TxtRut.Text;
            string nombres = TxtNombres.Text;
            string apellidos = TxtApellidos.Text;
            string contacto = TxtContacto.Text;
            string id = TxtId.Text;

            if (string.IsNullOrEmpty(rut))
            {
                await DisplayAlert("Warning", "Por favor ingresa el rut", "Cancel");
            }
            if (string.IsNullOrEmpty(nombres))
            {
                await DisplayAlert("Warning", "Por favor ingresa el nombre del conductor", "Cancel");
            }
            if (string.IsNullOrEmpty(apellidos))
            {
                await DisplayAlert("Warning", "Por favor ingresa los apellidos", "Cancel");
            }
            if (string.IsNullOrEmpty(contacto))
            {
                await DisplayAlert("Warning", "Por favor ingresa el número de contacto", "Cancel");
            }

            ConductorModel c = new ConductorModel();
            c.Rut = rut;
            c.Nombres = nombres;
            c.apellidos = apellidos;
            c.NumeroContacto = contacto;
            c.Id = id;


            bool isUpdated = await repository.Update(c);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Error modificando el conductor.", "Cancel");
            }
        }
    }
}
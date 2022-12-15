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
    public partial class ConductorCreate : ContentPage
    {
        ConductorViewModel repository = new ConductorViewModel();
        public ConductorCreate()
        {
            InitializeComponent();
        }

        public async void SaveButtonClicked(object sender, EventArgs e)
        {
            string rut = TxtRut.Text;
            string nombres = TxtNombres.Text;
            string apellidos = TxtApellidos.Text;
            string contacto = TxtContacto.Text;

            if (string.IsNullOrEmpty(rut))
            {
                await DisplayAlert("Warning", "Por Favor ingrese el rut ", "Cancel");
            }
            if (string.IsNullOrEmpty(nombres))
            {
                await DisplayAlert("Warning", "Por Favor ingrese el nombre del conductor", "Cancel");
            }
            if (string.IsNullOrEmpty(apellidos))
            {
                await DisplayAlert("Warning", "Por favor ingrese los apellidos del  conductor", "Cancel");
            }
            if (string.IsNullOrEmpty(contacto))
            {
                await DisplayAlert("Warning", "Por favor ingrese el numero de contacto del  conductor", "Cancel");
            }
            ConductorModel conduc = new ConductorModel();
            conduc.Rut = rut;
            conduc.Nombres = nombres;
            conduc.apellidos = apellidos;
            conduc.NumeroContacto = contacto;

            var isSaved = await repository.Save(conduc);
            if (isSaved)
            {
                await DisplayAlert("Information", "El conductor se ha agregado correctamente", "Ok");
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Error en el guardado del conductor", "Ok");
            }
        }
    }
}
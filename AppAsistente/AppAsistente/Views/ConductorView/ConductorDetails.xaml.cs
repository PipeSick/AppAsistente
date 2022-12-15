using AppAsistente.Models;
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
    public partial class ConductorDetails : ContentPage
    {
        public ConductorDetails(ConductorModel c)
        {
            InitializeComponent();

            if (c.Rut == null)
            {
                LabelRut.Text = "No Asignado.";

            }
            else
            {
                LabelRut.Text = c.Rut;
            }
            if (c.Nombres == null)
            {
                LabelNombre.Text = "No Asignado.";

            }
            else
            {
                LabelNombre.Text = c.Nombres;
            }
            if (c.apellidos == null)
            {
                LabelApellidos.Text = "No Asignado.";

            }
            else
            {
                LabelApellidos.Text = c.apellidos;
            }
            if (c.NumeroContacto == null)
            {
                LabelContacto.Text = "No Asignado.";

            }
            else
            {
                LabelContacto.Text = c.NumeroContacto;
            }
        }

        private async void VolverBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    
    }
}
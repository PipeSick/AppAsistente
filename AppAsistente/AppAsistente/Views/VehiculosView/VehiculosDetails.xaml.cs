using AppAsistente.Models;
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
    public partial class VehiculosDetails : ContentPage
    {
        public VehiculosDetails(VehiculoModel v)
        {
            InitializeComponent();
            if (v.Patente == null)
            {
                LabelPatente.Text = "No Asignado.";

            }
            else
            {
                LabelPatente.Text = v.Patente;
            }


            
            if (v.ConductorId == null)
            {
                LabelConductor.Text = "No Asignado.";

            }
            else
            {
                LabelConductor.Text = v.ConductorId;
            }
            if (v.AñoFabricacion.ToString() == null)
            {
                LabelFecha.Text = "No Asignado.";

            }
            else
            {
                LabelFecha.Text = v.AñoFabricacion.ToString();
            }            
        }

        private async void VolverBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
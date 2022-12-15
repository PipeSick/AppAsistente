using AppAsistente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.UsersView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetails : ContentPage
    {
        public UserDetails(UserModel User)
        {
            InitializeComponent();
            if (User.Nombre == null)
            {
                LabelNombre.Text = "No Asignado.";

            }
            else
            {
                LabelNombre.Text = User.Nombre;
            }
            if (User.Apellido == null)
            {
                LabelApellido.Text = "No Asignado.";

            }
            else
            {
                LabelApellido.Text = User.Apellido;
            }
            if (User.Correo == null)
            {
                LabelCorreo.Text = "No Asignado.";

            }
            else
            {
                LabelCorreo.Text = User.Correo;
            }
            if (User.VehiculoId == null)
            {
                LabelVehiculoAsignado.Text = "No Asignado.";

            }
            else
            {
                LabelVehiculoAsignado.Text = User.VehiculoId;
            }
            if (User.AlumnoRut == null)
            {
                LabelEstudianteRut.Text = "No Asignado.";

            }
            else
            {
                LabelEstudianteRut.Text = User.AlumnoRut;
            }
            Id.Text = User.Id;   
        }

        private async void VolverBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
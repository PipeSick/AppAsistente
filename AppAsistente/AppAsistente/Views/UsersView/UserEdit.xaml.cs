using AppAsistente.Models;
using AppAsistente.ViewModel;
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
    public partial class UserEdit : ContentPage
    {
        UserViewModel repository = new UserViewModel();
        public UserEdit(UserModel user)
        {
            InitializeComponent();
            TxtNombre.Text = user.Nombre;
            TxtApellido.Text = user.Apellido;
            TxtCorreo.Text = user.Correo;
            TxtContraseña.Text = user.Password;
            TxtVehiculoId.Text = user.VehiculoId;
            TxtId.Text = user.Id;
            TxtAlumnoRut.Text = user.AlumnoRut;
        }
        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            string nombre = TxtNombre.Text;
            string apellidos = TxtApellido.Text;
            string correo = TxtCorreo.Text;
            string contraseña = TxtContraseña.Text;
            string vehiculoid = TxtVehiculoId.Text;
            string alumnorut = TxtAlumnoRut.Text;

            if (string.IsNullOrEmpty(nombre))
            {
                await DisplayAlert("Warning", "Por favor ingresa nombre", "Cancel");
            }
            if (string.IsNullOrEmpty(correo))
            {
                await DisplayAlert("Warning", "Por favor ingresa tu correo", "Cancel");
            }
            if (string.IsNullOrEmpty(contraseña))
            {
                await DisplayAlert("Warning", "Por favor ingresa tu contraseña", "Cancel");
            }
            if (string.IsNullOrEmpty(vehiculoid))
            {
                await DisplayAlert("Warning", "Por favor ingresa la patente del vehiculo", "Cancel");
            }


            UserModel user = new UserModel();
            user.Id = TxtId.Text;
            user.Nombre = nombre;
            user.Apellido = apellidos;
            user.Correo = correo;
            user.Password = contraseña;
            user.VehiculoId = vehiculoid;
            user.AlumnoRut = alumnorut;
                       
            bool isUpdated = await repository.Update(user);
            if (isUpdated)
            {
                await Navigation.PopModalAsync();
            }
            else
            {
                await DisplayAlert("Error", "Update failed.", "Cancel");
            }

        }


    }
}
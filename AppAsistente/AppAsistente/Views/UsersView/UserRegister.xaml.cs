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
    public partial class UserRegister : ContentPage
    {
        VehiculoViewModel repositoryVehiculos = new VehiculoViewModel();
        UserViewModel repository = new UserViewModel();
        AlumnoViewModel repositoryAlumnos = new AlumnoViewModel();
        public UserRegister()
        {
            InitializeComponent();
            BindingContext = this;
            mostrarinfo();
        }
        public async void mostrarinfo()
        {
            var Patentes = await repositoryVehiculos.GetAll();
            pickerPatente.ItemsSource = Patentes;
            var alumnos = await repositoryAlumnos.GetAll();
            pickerAlumno.ItemsSource = alumnos;

        }

        private async void SaveButtonClicked(object sender, EventArgs e)
        {
 
            string Nombre = TxtNombre.Text;
            string Apellido = TxtApellidos.Text;
            string Correo = TxtCorreo.Text;
            string Password = TxtContraseña.Text;
            VehiculoModel vehimodel = pickerPatente.SelectedItem as VehiculoModel;
            string VehiculoId = vehimodel.Patente;
            AlumnoModel alumodel = pickerAlumno.SelectedItem as AlumnoModel;
            string AlumnoRut = alumodel.RutAlu;
            if (string.IsNullOrEmpty(Nombre))
            {
                await DisplayAlert("Warning", "Por Favor ingresa tu nombre", "Cancelar");
            }
            if (string.IsNullOrEmpty(Apellido))
            {
                await DisplayAlert("Warning", "Por Favor ingresa tu apellido", "Cancelar");
            }
            if (string.IsNullOrEmpty(Correo))
            {
                await DisplayAlert("Warning", "Por Favor Ingresa tu correo", "Cancelar");
            }
            if (string.IsNullOrEmpty(Password))
            {
                await DisplayAlert("Warning", "Por Favor Ingresa tu contraseña", "Cancelar");
            }
            if (string.IsNullOrEmpty(VehiculoId))
            {
                await DisplayAlert("Warning", "Por Favor Ingresa la patente del vehiculo asignado", "Cancelar");
            }

            UserModel user = new UserModel();
            user.Nombre = Nombre;
            user.Apellido = Apellido;
            user.Correo = Correo;
            user.Password = Password;
            user.VehiculoId = VehiculoId;
            user.AlumnoRut = AlumnoRut;
            var isSaved = await repository.SaveUser(user);
            if (isSaved)
            {
                await DisplayAlert("Information", "El usuario se ha guardado.", "Ok");

            }
            else
            {
                await DisplayAlert("Error", "Error al crear el usuario.", "Ok");
            }

        }
    }
}
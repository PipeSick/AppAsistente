using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using CommunityToolkit.Mvvm.Input;
using AppAsistente.Views.AccessView;
using Firebase.Database;
using AppAsistente.Models;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppAsistente.Views;
using Firebase.Auth;
using System.Collections.ObjectModel;
using Newtonsoft.Json;


namespace AppAsistente.ViewModel
{

    public class UserViewModel : BaseViewModel
    {
        private ObservableCollection<UserModel> _userModels = new ObservableCollection<UserModel>();
        public ObservableCollection<UserModel> UserModels
        {
            get { return _userModels; }
            set
            {
                _userModels = value;
                OnPropertyChanged();
            }
        }
        public string email;
        public string password;
        public string Nombre;
        public string Apellido;
        public string Correo;
        public string VehiculoId;
        public string AlumnoRut;

        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");


        public string EmailTxt
        {
            get { return this.email; }
            set { SetValue(ref this.email, value); }
        }

        public string PasswordTxt
        {
            get { return this.password; }
            set { SetValue(ref this.password, value); }
        }

        public string TxtNombre
        {
            get { return this.Nombre; }
            set { SetValue(ref this.Nombre, value); }
        }

        public string TxtApellido
        {
            get { return this.Apellido; }
            set { SetValue(ref this.Apellido, value); }
        }
        public string TxtVehiculoId
        {
            get { return this.VehiculoId; }
            set { SetValue(ref this.VehiculoId, value); }
        }
        public string TxtRutAlumno
        {
            get { return this.AlumnoRut; }
            set { SetValue(ref this.AlumnoRut, value); }
        }

        public ICommand LoginCommand
        {
            get
            {
                UserModels = getData();
                return new RelayCommand(LoginMethod);
            }
        }



        public async Task<bool> SaveUser(UserModel user)
        {
            var data = await firebaseClient.Child(nameof(UserModel)).PostAsync(JsonConvert.SerializeObject(user));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;


        }
        public async void LoginMethod()
        {
            
            if (string.IsNullOrEmpty(this.email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar el correo.",
                    "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "Debes ingresar una contraseña.",
                    "Aceptar");
                return;
            }

            try
            {
                var usuarios = await GetAll();

                foreach (var item in usuarios)
                {
                    if (item.Correo.Contains(email))
                    {
                        if (item.Password.Contains(password))
                        {
                            await Application.Current.MainPage.Navigation.PushAsync(new ContainerTabbedPage(item));
                            break;
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert(
                                   "Error",
                                   "Usuario o contraseña incorrectos",
                                   "Aceptar");

                        }
                    }

                }
      
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert(
                       "Error",
                       ex.Message,
                       "Aceptar");
                return;                
            }            
        }

        public ObservableCollection<UserModel> getData()
        {
            var UserData = firebaseClient
                .Child(nameof(UserModel))
                .AsObservable<UserModel>()
                .AsObservableCollection();
            return UserData;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(UserModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<UserModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(UserModel) + "/" + id).OnceSingleAsync<UserModel>());
        }


        public async Task<List<UserModel>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(UserModel)).OnceAsync<UserModel>()).Select(item => new UserModel
            {
                Nombre = item.Object.Nombre,
                Apellido = item.Object.Apellido,
                Correo = item.Object.Correo,
                Password = item.Object.Password,
                VehiculoId = item.Object.VehiculoId,
                AlumnoRut = item.Object.AlumnoRut,
                Id = item.Key
            }).Where(c => c.Nombre.ToLower().Contains(name.ToLower())).ToList(); ;
        }

        public async Task<List<UserModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(UserModel)).OnceAsync<UserModel>()).Select(item => new UserModel
            {
                Nombre = item.Object.Nombre,
                Apellido = item.Object.Apellido,
                Correo = item.Object.Correo,
                Password = item.Object.Password,
                VehiculoId = item.Object.VehiculoId,
                AlumnoRut = item.Object.AlumnoRut,
                Id = item.Key
            }).ToList();
        }

        public async Task<bool> Update(UserModel user)
        {
            await firebaseClient.Child(nameof(UserModel) + "/" + user.Id).PutAsync(JsonConvert.SerializeObject(user));
            return true;
        }

    }
}


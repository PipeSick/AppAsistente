using AppAsistente.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAsistente.Services
{
    public class FirebaseHelper
    {
        public async Task<List<UserModel>> GetAllUsers()
        {
            return (await firebase
              .Child(nameof(UserModel))
              .OnceAsync<UserModel>()).Select(f => new UserModel
              {
                  Nombre = f.Object.Nombre,
                  Apellido = f.Object.Apellido,
                  Correo = f.Object.Correo,
                  Password = f.Object.Password,
                  VehiculoId = f.Object.VehiculoId,
                  AlumnoRut = f.Object.AlumnoRut,
              }).ToList();
        }       

        public async Task AddUser(UserModel _userModel)
        {
            await firebase
            .Child("UserModel")
            .PostAsync(new UserModel()
            {
                Nombre = _userModel.Nombre,
                Apellido = _userModel.Apellido,
                Correo = _userModel.Correo,
                Password = _userModel.Password,
                VehiculoId = _userModel.VehiculoId,
                AlumnoRut = _userModel.AlumnoRut,
            });
        }


        public async Task UpdateUser(UserModel _userModel)
        {
            var toUpdateUser = (await firebase
              .Child("UserModel")
              .OnceAsync<UserModel>()).Where(a => a.Object.Id == _userModel.Id).FirstOrDefault();

            await firebase
              .Child("UserModel")
              .Child(toUpdateUser.Key)
              .PutAsync(new UserModel()
              {
                  Id = _userModel.Id,
                  Nombre = _userModel.Nombre,
                  Apellido = _userModel.Apellido,
                  Correo = _userModel.Correo,
                  Password = _userModel.Password,
                  VehiculoId= _userModel.VehiculoId,
                  AlumnoRut = _userModel.AlumnoRut,
              });
        }

        public async Task DeleteUser(string Id)
        {
            var toDeleteUser = (await firebase
              .Child("UserModel")
              .OnceAsync<UserModel>()).Where(a => a.Object.Id == Id).FirstOrDefault();
            await firebase.Child("UserModel").Child(toDeleteUser.Key).DeleteAsync();

        }

       
        FirebaseClient firebase;
        public FirebaseHelper()
        {
            firebase = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");
        }

    }
}

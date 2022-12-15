using AppAsistente.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAsistente.ViewModel
{
    public class AlumnoViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");

        public async Task<bool> Save(AlumnoModel alumno)
        {
            var data = await firebaseClient.Child(nameof(AlumnoModel)).PostAsync(JsonConvert.SerializeObject(alumno));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(AlumnoModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<AlumnoModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(AlumnoModel) + "/" + id).OnceSingleAsync<AlumnoModel>());
        }

        public async Task<List<AlumnoModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(AlumnoModel)).OnceAsync<AlumnoModel>()).Select(item => new AlumnoModel
            {
                Nombre = item.Object.Nombre,
                Apellidos = item.Object.Apellidos,
                EstableciminetoId = item.Object.EstableciminetoId,
                RutAlu = item.Object.RutAlu,
                Id = item.Key,
            }).ToList();
        }

        public async Task<bool> Update(AlumnoModel alumno)
        {
            await firebaseClient.Child(nameof(AlumnoModel) + "/" + alumno.Id).PutAsync(JsonConvert.SerializeObject(alumno));
            return true;
        }
    }
}

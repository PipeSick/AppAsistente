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
    public class EstablecimientoViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");       


        public async Task<bool> Save(EstablecimientoModel Establecimiento)
        {
            var data = await firebaseClient.Child(nameof(EstablecimientoModel)).PostAsync(JsonConvert.SerializeObject(Establecimiento));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(EstablecimientoModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<EstablecimientoModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(EstablecimientoModel) + "/" + id).OnceSingleAsync<EstablecimientoModel>());
        }        

        public async Task<List<EstablecimientoModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(EstablecimientoModel)).OnceAsync<EstablecimientoModel>()).Select(item => new EstablecimientoModel
            {
                Nombre = item.Object.Nombre,
                Direccion = item.Object.Direccion,
                Contacto = item.Object.Contacto,
                Director = item.Object.Director,
                Id = item.Key,
            }).ToList();
        }

        public async Task<bool> Update(EstablecimientoModel establecimieto)
        {
            await firebaseClient.Child(nameof(EstablecimientoModel) + "/" + establecimieto.Id).PutAsync(JsonConvert.SerializeObject(establecimieto));
            return true;
        }
    }
}

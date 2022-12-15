using AppAsistente.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppAsistente.ViewModel
{
    public class UbicacionViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");

        public async Task<bool> Save(LocationModel Ubicacion)
        {
            var data = await firebaseClient.Child(nameof(LocationModel)).PostAsync(JsonConvert.SerializeObject(Ubicacion));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> Update(LocationModel Ubicacion, string Id)
        {
            await firebaseClient.Child(nameof(LocationModel) + "/" + Id).PutAsync(JsonConvert.SerializeObject(Ubicacion));
            return true;
        }

        public async Task<List<LocationModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(LocationModel)).OnceAsync<LocationModel>()).Select(item => new LocationModel
            {
                Id = item.Key,
                Lat = item.Object.Lat,
                Lng = item.Object.Lng,
                patente = item.Object.patente,
            }).ToList();
        }

       

    }
}


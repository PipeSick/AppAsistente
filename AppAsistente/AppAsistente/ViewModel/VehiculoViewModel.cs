using AppAsistente.Models;
using Firebase.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAsistente.ViewModel
{
    public class VehiculoViewModel : BaseViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");

        public string Id;
        public int AñoFabricacion;
        public string IdConductor;

        public string IdTxt
        {
            get { return this.Id; }
            set { SetValue(ref this.Id, value); }
        }

        public int TxtAñoFabricacion
        {
            get { return this.AñoFabricacion; }
            set { SetValue(ref this.AñoFabricacion, value); }
        }

        public string TxtConductor
        {
            get { return this.IdConductor; }
            set { SetValue(ref this.IdConductor, value); }
        }


        public async Task<bool> SaveVehiculo(VehiculoModel vehiculo)
        {
            var data = await firebaseClient.Child(nameof(VehiculoModel)).PostAsync(JsonConvert.SerializeObject(vehiculo));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }     


        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(VehiculoModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<VehiculoModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(VehiculoModel) + "/" + id).OnceSingleAsync<VehiculoModel>());
        }



        public async Task<List<VehiculoModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(VehiculoModel)).OnceAsync<VehiculoModel>()).Select(item => new VehiculoModel
            {
                Id = item.Key,
                Patente = item.Object.Patente,
                ConductorId = item.Object.ConductorId,
                AñoFabricacion = item.Object.AñoFabricacion,
            }).ToList();
        }

        public async Task<bool> Update(VehiculoModel vehiculo)
        {
            await firebaseClient.Child(nameof(VehiculoModel) + "/" + vehiculo.Id).PutAsync(JsonConvert.SerializeObject(vehiculo));
            return true;
        }
    }
}

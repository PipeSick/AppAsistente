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
    public class ConductorViewModel : BaseViewModel
    {
        FirebaseClient firebaseClient = new FirebaseClient("https://proyecto-a0320-default-rtdb.firebaseio.com/");        
        public string Rut;
        public string Nombres;
        public string apellidos;
        public string NumeroContacto;
        public string Id;

        public string RutTxt
        {
            get { return this.Rut; }
            set { SetValue(ref this.Rut, value); }
        }

        public string Txtnombres
        {
            get { return this.Nombres; }
            set { SetValue(ref this.Nombres, value); }
        }

        public string TxtApellidos
        {
            get { return this.apellidos; }
            set { SetValue(ref this.apellidos, value); }
        }
        public string TxtNContacto
        {
            get { return this.NumeroContacto; }
            set { SetValue(ref this.NumeroContacto, value); }
        }


        public async Task<bool> Save(ConductorModel conductor)
        {
            var data = await firebaseClient.Child(nameof(ConductorModel)).PostAsync(JsonConvert.SerializeObject(conductor));
            if (!string.IsNullOrEmpty(data.Key))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(string id)
        {
            await firebaseClient.Child(nameof(ConductorModel) + "/" + id).DeleteAsync();
            return true;
        }

        public async Task<ConductorModel> GetById(string id)
        {
            return (await firebaseClient.Child(nameof(ConductorModel) + "/" + id).OnceSingleAsync<ConductorModel>());
        }


        public async Task<List<ConductorModel>> GetAllByName(string name)
        {
            return (await firebaseClient.Child(nameof(ConductorModel)).OnceAsync<ConductorModel>()).Select(item => new ConductorModel
            {
                Rut = item.Object.Rut,
                Nombres = item.Object.Nombres,
                apellidos = item.Object.apellidos,
                NumeroContacto = item.Object.NumeroContacto,
                Id = item.Key,
            }).Where(c => c.Nombres.ToLower().Contains(name.ToLower())).ToList(); ;
        }

        public async Task<List<ConductorModel>> GetAll()
        {
            return (await firebaseClient.Child(nameof(ConductorModel)).OnceAsync<ConductorModel>()).Select(item => new ConductorModel
            {
                Rut = item.Object.Rut,
                Nombres = item.Object.Nombres,
                apellidos = item.Object.apellidos,
                NumeroContacto = item.Object.NumeroContacto,
                Id = item.Key,
            }).ToList();
        }

        public async Task<bool> Update(ConductorModel conductor)
        {
            await firebaseClient.Child(nameof(ConductorModel) + "/" + conductor.Id).PutAsync(JsonConvert.SerializeObject(conductor));
            return true;
        }

    }
}

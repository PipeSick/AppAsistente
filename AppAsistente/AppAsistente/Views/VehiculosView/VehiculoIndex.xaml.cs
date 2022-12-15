using AppAsistente.Models;
using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.VehiculosView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehiculoIndex : ContentPage
    {
        VehiculoViewModel repository = new VehiculoViewModel();
        public VehiculoIndex()
        {
            InitializeComponent();
            VehiculoViewList.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var vehiculs = await repository.GetAll();
            VehiculoViewList.ItemsSource = null;
            VehiculoViewList.ItemsSource = vehiculs;
            VehiculoViewList.IsRefreshing = false;

        }
        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new VehiculosCreate());
        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Vehiculo?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Vehiculo ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Vehiculo.", "Ok");
                }
            }
        }
        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Vehiculo?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Vehiculo ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Vehiculo.", "Ok");
                }
            }
        }


        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var vehiculo = await repository.GetById(id);
            if (vehiculo == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            vehiculo.Id = id;
            await Navigation.PushModalAsync(new VehiculosEdit(vehiculo));
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var vehiculo = await repository.GetById(id);
            if (vehiculo == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            vehiculo.Id = id;
            await Navigation.PushModalAsync(new VehiculosEdit(vehiculo));
        }



        private async void DetailsSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var vehiculo = await repository.GetById(id);
            if (vehiculo == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            vehiculo.Id = id;
            await Navigation.PushModalAsync(new VehiculosDetails(vehiculo));
        }

        private async void DetailsTapp_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var vehiculo = await repository.GetById(id);
            if (vehiculo == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            vehiculo.Id = id;
            await Navigation.PushModalAsync(new VehiculosDetails(vehiculo));
        }
    }
}
using AppAsistente.Models;
using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.ConductorView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConductorIndex : ContentPage
    {
        ConductorViewModel repository = new ConductorViewModel();
        public ConductorIndex()
        {
            InitializeComponent();
            ConductorViewList.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var conductor = await repository.GetAll();
            ConductorViewList.ItemsSource = null;
            ConductorViewList.ItemsSource = conductor;
            ConductorViewList.IsRefreshing = false;

        }
        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new ConductorCreate());
        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Conductor?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Conductor ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Conductor.", "Ok");
                }
            }
        }
        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Conductor?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Conductor ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Conductor.", "Ok");
                }
            }
        }


        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var con = await repository.GetById(id);
            if (con == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            con.Id = id;
            await Navigation.PushModalAsync(new ConductorEdit(con));
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var con = await repository.GetById(id);
            if (con == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            con.Id = id;
            await Navigation.PushModalAsync(new ConductorEdit(con));
        }



        private async void DetailsSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var con = await repository.GetById(id);
            if (con == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            con.Id = id;
            await Navigation.PushModalAsync(new ConductorDetails(con));
        }

        private async void DetailsTapp_Tapped(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var con = await repository.GetById(id);
            if (con == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            con.Id = id;
            await Navigation.PushModalAsync(new ConductorDetails(con));
        }
    }
}
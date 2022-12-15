using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.Establecimiento
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EstablecimientoIndex : ContentPage
    {
        EstablecimientoViewModel repository = new EstablecimientoViewModel();
        public EstablecimientoIndex()
        {
            InitializeComponent();
            EstablecimientoListView.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }

        protected override async void OnAppearing()
        {
            var estable = await repository.GetAll();
            EstablecimientoListView.ItemsSource = null;
            EstablecimientoListView.ItemsSource = estable;
            EstablecimientoListView.IsRefreshing = false;

        }
        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new EstablecimientoCreate());
        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Establecimiento?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Establecimiento ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Establecimiento.", "Ok");
                }
            }
        }
        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Establecimiento?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Establecimiento ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Establecimiento.", "Ok");
                }
            }
        }


        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var establecimiento = await repository.GetById(id);
            if (establecimiento == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            establecimiento.Id = id;
            await Navigation.PushModalAsync(new EstablecimientoEdit(establecimiento));
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var establecimiento = await repository.GetById(id);
            if (establecimiento == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            establecimiento.Id = id;
            await Navigation.PushModalAsync(new EstablecimientoEdit(establecimiento));
        }



        private async void DetailsSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var establecimiento = await repository.GetById(id);
            if (establecimiento == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            establecimiento.Id = id;
            await Navigation.PushModalAsync(new EstablecimientoEdit(establecimiento));
        }

        private async void DetailsTapp_Tapped(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var establecimiento = await repository.GetById(id);
            if (establecimiento == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            establecimiento.Id = id;
            await Navigation.PushModalAsync(new EstablecimientoEdit(establecimiento));
        }
    }
}
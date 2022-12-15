using AppAsistente.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.AlumnosView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlumnosIndex : ContentPage
    {
        AlumnoViewModel repository = new AlumnoViewModel();
        public AlumnosIndex()
        {
            InitializeComponent();
            AlumnosViewList.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var alumnos = await repository.GetAll();
            AlumnosViewList.ItemsSource = null;
            AlumnosViewList.ItemsSource = alumnos;
            AlumnosViewList.IsRefreshing = false;

        }
        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new AlumnosCreate());
        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Alumno?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Alumno ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Alumno.", "Ok");
                }
            }
        }
        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este Alumno?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El Alumno ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el Alumno.", "Ok");
                }
            }
        }


        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var Alumno = await repository.GetById(id);
            if (Alumno == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            Alumno.Id = id;
            await Navigation.PushModalAsync(new AlumnosEdit(Alumno));
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var Alumno = await repository.GetById(id);
            if (Alumno == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            Alumno.Id = id;
            await Navigation.PushModalAsync(new AlumnosEdit(Alumno));
        }
    }
}
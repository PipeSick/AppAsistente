using AppAsistente.Models;
using AppAsistente.Services;
using AppAsistente.ViewModel;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.UsersView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersIndex : ContentPage
    {
        UserViewModel repository = new UserViewModel();
        public UsersIndex()
        {
            InitializeComponent();
            UserViewList.RefreshCommand = new Command(() =>
            {
                OnAppearing();
            });
        }
        protected override async void OnAppearing()
        {
            var user = await repository.GetAll();
            UserViewList.ItemsSource = null;
            UserViewList.ItemsSource = user;
            UserViewList.IsRefreshing = false;
        }


        private void AddToolBarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new UserRegister());
        }

        private void UserListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
            {
                return;
            }
            var User = e.Item as UserModel;
            Navigation.PushModalAsync(new UserDetails(User));
            ((ListView)sender).SelectedItem = null;
        }

        private async void DeleteTapp_Tapped(object sender, EventArgs e)
        {

            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este usuario?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El usuario ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el usuario.", "Ok");
                }
            }
        }
        private async void DeleteMenuItem_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Elimnar", "¿Quieres eliminar este usuario?", "Sí", "No");
            if (response)
            {
                string id = ((TappedEventArgs)e).Parameter.ToString();
                bool isDelete = await repository.Delete(id);
                if (isDelete)
                {
                    await DisplayAlert("Information", "El usuario ha sido eliminado correctamente.", "Ok");
                    OnAppearing();
                }
                else
                {
                    await DisplayAlert("Error", "Error eliminando el usuario.", "Ok");
                }
            }
        }      


        private async void EditTap_Tapped(object sender, EventArgs e)
        {
            string id = ((TappedEventArgs)e).Parameter.ToString();
            var User = await repository.GetById(id);
            if (User == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos", "Ok");
            }
            User.Id = id;
            await Navigation.PushModalAsync(new UserEdit(User));
        }

        private async void EditSwipeItem_Invoked(object sender, EventArgs e)
        {
            string id = ((MenuItem)sender).CommandParameter.ToString();
            var user = await repository.GetById(id);
            if (user == null)
            {
                await DisplayAlert("Warning", "No se han encontrado datos.", "Ok");
            }
            user.Id = id;
            await Navigation.PushModalAsync(new UserEdit(user));
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
            await Navigation.PushModalAsync(new UserDetails(con));
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
            await Navigation.PushModalAsync(new UserDetails(con));
        }

    }
}


    

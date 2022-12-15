using AppAsistente.Models;
using AppAsistente.ViewModel;
using AppAsistente.Views.AlumnosView;
using AppAsistente.Views.ConductorView;
using AppAsistente.Views.Establecimiento;
using AppAsistente.Views.UsersView;
using AppAsistente.Views.VehiculosView;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Index : ContentPage
    {
        CancellationTokenSource cts;
        UbicacionViewModel repository = new UbicacionViewModel();
        bool Parar = false;
        LocationModel ubicacion = new LocationModel();
        private UserModel xd;

        public Index(UserModel Usuario)
        {
            InitializeComponent();
            PatenteFurgon.Text = Usuario.VehiculoId;
            NavigationPage.SetHasNavigationBar(this, false);
            xd = Usuario;
        }


        async void Start_Clicked(object sender, System.EventArgs e)
        {
            btnLocationStart.IsVisible = false;
            btnLocationStop.IsVisible = true;
            IconLocationOff.IsVisible = true;
            IconLocationOn.IsVisible = false;
            Parar = false;
            string FurgonId = PatenteFurgon.Text;
            string locationid = "default";
            var location5 = await Geolocation.GetLocationAsync();
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(0));
                cts = new CancellationTokenSource();
                var location = await Geolocation.GetLocationAsync(request, cts.Token);
                if (location != null)
                {
                    while (Parar == false)
                    {
                        var l2 = await repository.GetAll();
                        foreach (var item in l2)
                        {
                            if (item.patente == FurgonId)
                            {
                                locationid = item.Id;
                                break;
                            }
                        }

                        var location2 = await Geolocation.GetLocationAsync(request, cts.Token);
                        string Lat = location2.Latitude.ToString();
                        string Lng = location2.Longitude.ToString();
                        ubicacion.Lat = Lat;
                        ubicacion.Lng = Lng;
                        ubicacion.patente = FurgonId;
                        ubicacion.Id = locationid;                        
                        if (locationid == "default")
                        {
                            var issaved = await repository.Save(ubicacion);       
                        }
                        if (locationid != "default")
                        {
                            var isSaved = await repository.Update(ubicacion, locationid);
                        } 
                    }
                }
                else
                {
                    await DisplayAlert("Falied",
                        "no obtuvo el gps",
                        "ok");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await DisplayAlert("Faild", fnsEx.Message, "OK1");
            }
            catch (PermissionException pEx)
            {
                await DisplayAlert("Faild", pEx.Message, "OK2");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK3");
            }

        }

        void Stop_Clicked(object sender, System.EventArgs e)
        {
            btnLocationStart.IsVisible = true;
            btnLocationStop.IsVisible = false;
            IconLocationOn.IsVisible = true;
            IconLocationOff.IsVisible = false;
            Parar = true;
        }

        void AbrirCrudUsers(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new UsersIndex());
        }

        void AbrirIndexVehiculos(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new VehiculoIndex());
        }
        void AbrirIndexConductores(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new ConductorIndex());
        }
        void AbrirIndexEstablecimientos(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new EstablecimientoIndex());
        }
        void AbrirIndexAlumnos(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new AlumnosIndex());
        }



    }
}
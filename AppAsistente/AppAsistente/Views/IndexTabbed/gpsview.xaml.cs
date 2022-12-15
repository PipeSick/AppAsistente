using AppAsistente.Models;
using AppAsistente.ViewModel;
using AppAsistente.Views.AlumnosView;
using AppAsistente.Views.ConductorView;
using AppAsistente.Views.Establecimiento;
using AppAsistente.Views.UsersView;
using AppAsistente.Views.VehiculosView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views.IndexTabbed
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class gpsview : ContentPage
    {
        CancellationTokenSource cts;
        UbicacionViewModel repository = new UbicacionViewModel();
        bool Parar = false;
        LocationModel ubicacion = new LocationModel();
        public UserModel Usuario = new UserModel();
        private Pin pinRoute = new Pin
        {
            Label = "Ubicación del transporte"
        };
        public gpsview()
        {
            InitializeComponent();
            MyMap.Pins.Add(pinRoute);
            MessagingCenter.Subscribe<Object, UserModel>(this, "UserMessage", (sender, arg) =>
            {
                Usuario = arg;
            });
            PatenteFurgon.Text = Usuario.VehiculoId;
            NavigationPage.SetHasNavigationBar(this, false);
            ponerpins();
        }
        async void Start_Clicked(object sender, System.EventArgs e)
        {
            string FurgonId = Usuario.VehiculoId.ToString();
            btnLocationStart.IsVisible = false;
            btnLocationStop.IsVisible = true;
            IconLocationOff.IsVisible = true;
            IconLocationOn.IsVisible = false;
            Parar = false;
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
                        pinRoute.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(Lat), Convert.ToDouble(Lng));
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

        public async void ponerpins()
        {
                var localizacion = await Geolocation.GetLastKnownLocationAsync();
                Position position = new Position(localizacion.Latitude, localizacion.Longitude);
                MapSpan mapSpan = MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.8));
                MyMap.MoveToRegion(mapSpan);
                pinRoute.Position = new Xamarin.Forms.Maps.Position(Convert.ToDouble(localizacion.Latitude), Convert.ToDouble(localizacion.Longitude));            
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AppAsistente.Views.AccessView
{
    internal class ShowPasswordTriggerAction : TriggerAction<ImageButton>, INotifyPropertyChanged
    {
        public string ShowIcon { get; set; }
        public string HideIcon { get; set; }
        bool _EsconderContraseña = true;
        public bool EsconderContraseña { get=> _EsconderContraseña; set
            {
                if (_EsconderContraseña != value)
                {
                    _EsconderContraseña = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EsconderContraseña)));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected override void Invoke(ImageButton sender)
        {
            sender.Source = EsconderContraseña ? ShowIcon : HideIcon;
            EsconderContraseña = !EsconderContraseña;
        }
    }
}

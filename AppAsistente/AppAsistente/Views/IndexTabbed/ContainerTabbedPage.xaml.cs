﻿using AppAsistente.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace AppAsistente.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContainerTabbedPage : Xamarin.Forms.TabbedPage
    {
        public ContainerTabbedPage(UserModel usuario)
        {
            InitializeComponent();
            MessagingCenter.Send<Object, UserModel>(this, "UserMessage", usuario);
            On<Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Android>().SetIsSmoothScrollEnabled(true);
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
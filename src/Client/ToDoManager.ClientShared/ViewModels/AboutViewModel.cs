﻿using System;
using System.Windows.Input;
using ToDoManager.ClientShared.Tools;

namespace ToDoManager
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            //OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}

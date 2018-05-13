using System;
using System.Collections.Generic;
using ToDoManager.Model;
using ToDoManager.ViewModels;
using Xamarin.Forms;

namespace ToDoManager.Views
{
    public partial class PomodoroPage : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            //TODO: this is bad! 
            _vm.StartButtonTouched.Execute(null);
        }

        PomodoroViewModel _vm;

        //Only used by Xamarin Preview
        public PomodoroPage()
        {
            InitializeComponent();
			_vm = new PomodoroViewModel(new ToDoItemModel(), Navigation)
            {
                Title = "TestTitle",
            };
            BindingContext = _vm;
        }

        public PomodoroPage(PomodoroViewModel vm)
        {
            _vm = vm;
            BindingContext = _vm;
            InitializeComponent();
        }
    }
}

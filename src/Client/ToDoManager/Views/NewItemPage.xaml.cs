using System;
using ToDoManager.Model;
using ToDoManager.Services;
using ToDoManager.ViewModels;
using Xamarin.Forms;

namespace ToDoManager
{
    public partial class NewItemPage : ContentPage
    {
        private NewItemViewModel dataContext;

        public NewItemPage()
        {
			this.dataContext = new NewItemViewModel(new XamarinFormsNavigation(Navigation));
            InitializeComponent();

            this.BindingContext = dataContext;
        }
    }
}

using System;
using ToDoManager.Model;
using ToDoManager.ViewModels;
using Xamarin.Forms;

namespace ToDoManager
{
    public partial class NewItemPage : ContentPage
    {
        private NewItemViewModel dataContext;

        public NewItemPage()
        {
            this.dataContext = new NewItemViewModel(Navigation);
            InitializeComponent();

            this.BindingContext = dataContext;
        }
    }
}

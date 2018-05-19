using System;
using ToDoManager.Model;
using ToDoManager.Services;
using ToDoManager.ViewModels;
using ToDoManager.Views;
using Xamarin.Forms;

namespace ToDoManager
{
    public partial class ItemDetailPage : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
			Navigation.PushAsync(new PomodoroPage(new PomodoroViewModel(this.viewModel.SelectedItem, new XamarinFormsNavigation(Navigation))));
        }

        ItemDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public ItemDetailPage()
        {
            InitializeComponent();

            var item = new ToDoItemModel
            {
                Title = "Item 1",
                Description = "This is an item description."
            };

			viewModel = new ItemDetailViewModel(new XamarinFormsNavigation(Navigation),item);
            BindingContext = viewModel;

			//Xamarin.Forms.MessagingCenter.Subscribe()
        }

        public ItemDetailPage(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = this.viewModel = viewModel;
		}
    }
}

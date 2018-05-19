using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoManager.Model;
using ToDoManager.Services;
using Xamarin.Forms;

namespace ToDoManager
{
    public partial class ItemsPage : ContentPage
    {
        ItemsViewModel viewModel;

        public ItemsPage()
        {
            InitializeComponent();

			BindingContext = viewModel = new ItemsViewModel(new CloudDataStore());
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as ToDoItemModel;
            if (item == null)
                return;

			await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(new XamarinFormsNavigation(Navigation), new CloudDataStore(), item)));

            //Manually deselect item
            ItemsListView.SelectedItem = null;
        }

        async void AddItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewItemPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}

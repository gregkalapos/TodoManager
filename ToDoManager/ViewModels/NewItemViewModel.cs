using System;
using ToDoManager.Model;
using Xamarin.Forms;

namespace ToDoManager.ViewModels
{
    public class NewItemViewModel : BaseViewModel
    {
        public String NewItemTitle { get; set; }
        public String NewItemDescription { get; set;}
        public Boolean IsLoading { get; set; }

        public Command SaveButtonTouched { get; }

        private INavigation navigation;

        public NewItemViewModel(INavigation navigation)
        {
            this.navigation = navigation;

            SaveButtonTouched = new Command(async() =>
            {
                //Show load btn.
                //Add to server
                //Add list
                //back

                try
                {
                    IsLoading = true;

                    var newTodoItem = new ToDoItemModel
                    {
                        Title = NewItemTitle,
                        Description = NewItemDescription,
                        Created = DateTime.UtcNow,
                    };

					try
					{
						var retVal = await DataStore.AddItemAsync(newTodoItem);
						MessagingCenter.Send(this, Consts.AddNewToDoItemStr, retVal); 
						await navigation.PopAsync();
					}
					catch(Exception e)
					{
						//TODO: popup or something like that...
					}
                }
                finally
                {
                    IsLoading = false;
                }
            });
        }
    }
}

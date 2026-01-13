using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.Base;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper
{
    public class UserCollectionViewModel :
        ViewModelSyncCollection<
            UserViewModel,
            User,
            UserCollection>
    {
        public override void NewModelAssigned()
        {
            foreach (var userViewModel in this)
            {
                var modelPropChanged =
                    userViewModel.Model as INotifyPropertyChanged;
                if (modelPropChanged != null)
                {
                    modelPropChanged.PropertyChanged +=
                        userViewModel.OnPropertyChangedInModel;
                }
            }
        }
    }
}
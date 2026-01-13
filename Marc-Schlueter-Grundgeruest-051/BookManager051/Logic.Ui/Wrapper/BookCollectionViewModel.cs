using De.HsFlensburg.ClientApp051.Business.Model.BusinessObjects;
using De.HsFlensburg.ClientApp051.Logic.Ui.Base;
using System.ComponentModel;

namespace De.HsFlensburg.ClientApp051.Logic.Ui.Wrapper
{
	public class BookCollectionViewModel :
		ViewModelSyncCollection<
			BookViewModel,
			Book,
			BookCollection>
	{
		public override void NewModelAssigned()
		{
			foreach (var bookViewModel in this)
			{
				var modelPropChanged =
					bookViewModel.Model as INotifyPropertyChanged;
				if (modelPropChanged != null)
				{
					modelPropChanged.PropertyChanged +=
						bookViewModel.OnPropertyChangedInModel;
				}
			}
		}
	}
}
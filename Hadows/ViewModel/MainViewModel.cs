using GalaSoft.MvvmLight;

namespace Hadows.ViewModel
{
	public class MainViewModel : ViewModelBase
	{
		#region string Test
		private string _Test = "before sunset";
		public string Test
		{
			get
			{
				return _Test;
			}
			set
			{
				_Test = value;
				RaisePropertyChanged("Test");
			}
		}
		#endregion string Test

		public MainViewModel()
		{
			Test = "howard";
		}
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hadows.Component;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Hadows.View.Window
{
	public class WindowPage : Page
	{
		StackPanel _snappedSizePanel;
		Grid _fullSizePanel;
		bool _isFullSizeMode;

		public WindowPage()
		{
			LinkEvents();
		}

		private void LinkEvents()
		{
			this.Loaded += WindowPage_Loaded;
			this.SizeChanged += WindowPage_SizeChanged;
		}

		void WindowPage_SizeChanged(object sender, Windows.UI.Xaml.SizeChangedEventArgs e)
		{
			if (_isLoadedItem == false)
				return;


			if (this.ActualWidth < 500 &&
				this._isFullSizeMode == true)
			{
				for (int i = _fullSizePanel.Children.Count - 1; i >= 0; i--)
				{
					dynamic temp = _fullSizePanel.Children[i];
					temp.Height = (temp.Content as IComponent).SnappedStateHeight;
					_fullSizePanel.Children.RemoveAt(i);
					_snappedSizePanel.Children.Insert(0, temp);
				}
				VisualStateManager.GoToState(this, "SnappedSizeState", false);
				this._isFullSizeMode = false;
			}
			else if (this.ActualWidth > 500 &&
				this._isFullSizeMode == false)
			{
				for (int i = _snappedSizePanel.Children.Count - 1; i >= 0; i--)
				{
					dynamic temp = _snappedSizePanel.Children[i];
					temp.Height = double.NaN;
					_snappedSizePanel.Children.RemoveAt(i);
					_fullSizePanel.Children.Add(temp);

				}
				VisualStateManager.GoToState(this, "FullSizeState", false);
				this._isFullSizeMode = true;
			}

		}
		bool _isLoadedItem = false;
		void WindowPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			_fullSizePanel = (this.Content as Panel).Children[0] as Grid;
			_snappedSizePanel = ((this.Content as Panel).Children[1] as ScrollViewer).Content as StackPanel;
			_isFullSizeMode = true;
			Debug.Assert(_snappedSizePanel != null);
			Debug.Assert(_fullSizePanel != null);
			_isLoadedItem = true;
		}

	}
}

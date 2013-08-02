using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Hadows.Component;
using Hadows.Control;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hadows.View.Window
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	internal sealed partial class Window001 : WindowPage
	{
		internal Window001()
		{
			this.InitializeComponent();
			this.CellSelected += Window001_CellSelected;
		}

		void Window001_CellSelected(object sender, WindowPage.CellInfo e)
		{
			_column = e.Column;
			_row = e.Row;
			
		}
		
		int _column;
		int _row;

		void selector_ItemSelected(object sender, Component.IComponent e)
		{
			FrameworkElement element = e.GetInstance();
			if (element == null)
			{
				Debug.Assert(false, "GetInstance()를 통해서 아이템을 만들어 내지 못했습니다.");
				return;
			}
			element.SetValue(Grid.RowProperty, _row);
			element.SetValue(Grid.ColumnProperty, _column);
			element.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Stretch;
			element.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Stretch;
			this.LayoutRoot.Children.Add(element);
		}

		/// <summary>
		/// Invoked when this page is about to be displayed in a Frame.
		/// </summary>
		/// <param name="e">Event data that describes how this page was reached.  The Parameter
		/// property is typically used to configure the page.</param>
		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
		}
	}
}

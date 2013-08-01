using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Hadows.View.Window
{
	internal class WindowPage : Page
	{
		internal class CellInfo
		{
			public int Row { get; set; }
			public int Column { get; set; }
		}

		#region CellSelected
		public event EventHandler<CellInfo> CellSelected;
		/// <summary>
		/// 
		/// </summary>
		private void InvokeCellSelected(CellInfo info)
		{
			if (CellSelected == null)
				return;

			CellSelected(this, info);
		}
		#endregion CellSelected




		internal WindowPage()
		{
			this.Loaded += WindowPage_Loaded;

		}

		void WindowPage_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			InitWindow();
		}

		private void InitWindow()
		{
			Grid layoutRootGrid = this.Content as Grid;
			if (layoutRootGrid == null)
			{
				Debug.Assert(false, "최상위 객체는 Grid 이어야합니다.");
				return;
			}

			int rows = layoutRootGrid.RowDefinitions.Count;
			int columns = layoutRootGrid.ColumnDefinitions.Count;


			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					Button button = new Button();
					button.SetValue(Grid.RowProperty, i);
					button.SetValue(Grid.ColumnProperty, j);
					button.Content = string.Format("row = {0}, column = {1}", i, j);
					button.VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center;
					button.HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center;
					layoutRootGrid.Children.Add(button);
					button.Click += button_Click;
				}
			}
		}

		void button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
		{
			Button selectedButton = sender as Button;
			if (selectedButton == null)
			{
				Debug.Assert(false, "selected button 이 null 입니다.");
				return;
			}

			InvokeCellSelected(new CellInfo()
			{
				Column = (int)selectedButton.GetValue(Grid.ColumnProperty),
				Row = (int)selectedButton.GetValue(Grid.RowProperty)
			});
		}
	}
}

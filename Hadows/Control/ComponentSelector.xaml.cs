using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Hadows.Component;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace Hadows.Control
{
	public sealed partial class ComponentSelector : UserControl
	{
		#region ItemSelected
		public event EventHandler<IComponent> ItemSelected;
		/// <summary>
		/// 
		/// </summary>
		private void InvokeItemSelected(IComponent component)
		{
			if (ItemSelected == null)
				return;

			ItemSelected(this, component);
		}
		#endregion ItemSelected

		public ComponentSelector()
		{
			this.InitializeComponent();
			LinkEvents();
		}

		private void LinkEvents()
		{
			ComponentListView.SelectionChanged += ComponentListView_SelectionChanged;
		}

		void ComponentListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			IComponent selectedComponent = e.AddedItems[0] as IComponent;
			if (selectedComponent == null)
			{
				Debug.Assert(false, "선택된 아이템이 IComponent가 아닙니다.");
				return;
			}

			InvokeItemSelected(selectedComponent);
		}
	}
}

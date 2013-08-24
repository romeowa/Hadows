using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hadows.Component;
using Hadows.ViewModel;
using Microsoft.Practices.ServiceLocation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Hadows.Control
{
	[TemplatePart(Name = "ComponentListView", Type = typeof(ListView))]
	public class ComponentSelector : Windows.UI.Xaml.Controls.Control
	{
		//-------------------------- ▶ Members
		internal const string ComponentListViewName = "ComponentListView";
		internal ListView ComponentListView;



		//-------------------------- ▶ Events
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


		//-------------------------- ▶ Constructors	
		public ComponentSelector()
		{
			this.DefaultStyleKey = typeof(ComponentSelector);

			Binding dataContextBinding = new Binding()
			{
				Source = App.Current.Resources["Locator"],
				Path = new PropertyPath("Component")
			};
			this.SetBinding(FrameworkElement.DataContextProperty, dataContextBinding);
		}


		//-------------------------- ▶ Methods
		void LinkEvents()
		{
			if (ComponentListView == null)
			{
				Debug.Assert(false, "ComponentListView가 null입니다.");
				return;
			}

			ComponentListView.SelectionChanged += ComponentListView_SelectionChanged;
		}


		//-------------------------- ▶ EventHandlers
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



		//-------------------------- ▶ Overrides
		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			InitilaizeComponents();
			LinkEvents();
		}

		void InitilaizeComponents()
		{
			ComponentListView = (ListView)GetTemplateChild(ComponentListViewName);
		}
	}
}

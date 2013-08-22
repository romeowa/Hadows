using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Hadows.Control
{
	[TemplatePart(Name = "MyContentPresenter", Type = typeof(ContentPresenter))]
	[TemplatePart(Name = "AddButton", Type = typeof(Button))]
	[TemplatePart(Name = "ClearButton", Type = typeof(Button))]
	[TemplatePart(Name = "EmptyState", Type = typeof(VisualState))]
	[TemplatePart(Name = "ContentState", Type = typeof(VisualState))]
	public class ComponentContentControl : ContentControl, INotifyPropertyChanged
	{
		internal const string MyContentPresenterName = "MyContentPresenter";
		internal ContentPresenter MyContentPresenter;

		internal const string AddButtonName = "AddButton";
		internal Button AddButton;

		internal const string ClearButtonName = "ClearButton";
		internal Button ClearButton;

		internal const string EmptyStateName = "EmptyState";
		internal VisualState EmptyState;

		internal const string ContentStateName = "ContentState";
		internal VisualState ContentState;

		Popup _popup;

		#region ContentObject
		private object _contentObject;
		/// <summary>
		/// 
		/// </summary>
		public object ContentObject
		{
			get
			{
				return _contentObject;
			}
			set
			{
				_contentObject = value;
				if (_contentObject == null)
				{
					VisualStateManager.GoToState(this, EmptyState.Name, false);
				}
				else
				{
					VisualStateManager.GoToState(this, ContentState.Name, false);
				}
			}
		}
		#endregion ContentObject


		public ComponentContentControl()
		{
			this.DefaultStyleKey = typeof(ComponentContentControl);
			this.DataContext = this;

		}



		protected override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			InitializeComponents();
			LinkEvents();
		}

		private void LinkEvents()
		{
			AddButton.Click += AddButton_Click;
			ClearButton.Click += ClearButton_Click;
		}

		void AddButton_Click(object sender, RoutedEventArgs e)
		{
			ComponentSelector selector = new ComponentSelector();
			Grid popupGrid = new Grid()
			{
				Background = new SolidColorBrush(Colors.Black),
				Width = Window.Current.Bounds.Width,
				Height = Window.Current.Bounds.Height
			};
			selector.ItemSelected += selector_ItemSelected;
			popupGrid.Children.Add(selector);

			_popup = new Popup()
			{
				Child = popupGrid,
				VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Top,
				HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Left
			};
			_popup.IsOpen = true;
		}

		void selector_ItemSelected(object sender, Component.IComponent e)
		{
			_popup.IsOpen = false;

			FrameworkElement element = e.GetInstance();
			if (element == null)
			{
				Debug.Assert(false, "GetInstance()를 통해서 아이템을 만들어 내지 못했습니다.");
				return;
			}

			this.Content = element;
		}

		void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			this.Content = null;
		}

		private void InitializeComponents()
		{
			MyContentPresenter = (ContentPresenter)GetTemplateChild(MyContentPresenterName);
			AddButton = (Button)GetTemplateChild(AddButtonName);
			ClearButton = (Button)GetTemplateChild(ClearButtonName);
			EmptyState = (VisualState)GetTemplateChild(EmptyStateName);
			ContentState = (VisualState)GetTemplateChild(ContentStateName);

			Debug.Assert(MyContentPresenter != null);
			Debug.Assert(AddButton != null);
			Debug.Assert(ClearButton != null);
			Debug.Assert(EmptyState != null);
			Debug.Assert(ContentState != null);


			Binding contentBinding = new Binding()
			{
				Source = this,
				Mode = BindingMode.TwoWay,
				Path = new PropertyPath("ContentObject")
			};

			MyContentPresenter.SetBinding(ContentPresenter.ContentProperty, contentBinding);
		}

		#region ▶ INotifyPropertyChanged Members
		public event PropertyChangedEventHandler PropertyChanged;
		/// <summary>
		/// 프로퍼티가 변경되면 PropertyChanged 이벤트을 발생시켜주는 메서드
		/// </summary>
		/// <param name="propertyName">변경된 프로퍼티 이름</param>
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler == null)
			{
				return;
			}
			handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		#endregion INotifyPropertyChanged Members


	}
}

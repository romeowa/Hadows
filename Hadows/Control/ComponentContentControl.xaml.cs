using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
	public sealed partial class ComponentContentControl : UserControl
	{

		#region Content
		/// <summary> 
		/// Gets or sets the Content possible Value of the FrameworkElement object.
		/// </summary> 
		public FrameworkElement Content
		{
			get { return (FrameworkElement)GetValue(ContentProperty); }
			set { SetValue(ContentProperty, value); }
		}

		/// <summary> 
		/// Identifies the Content dependency property.
		/// </summary> 
		public static readonly DependencyProperty ContentProperty =
					DependencyProperty.Register(
						  "Content",
						  typeof(FrameworkElement),
						  typeof(ComponentContentControl),
						  new PropertyMetadata(null, OnContentPropertyChanged));

		/// <summary>
		/// ContentProperty property changed handler. 
		/// </summary>
		/// <param name="d">ComponentContentControl that changed its Content.</param>
		/// <param name="e">DependencyPropertyChangedEventArgs.</param> 
		private static void OnContentPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			ComponentContentControl _ComponentContentControl = d as ComponentContentControl;
			if (_ComponentContentControl != null)
			{
				_ComponentContentControl.SetContent((FrameworkElement)e.NewValue);
			}
		}

		private void SetContent(FrameworkElement frameworkElement)
		{
			this.ContentPanel.Children.Clear();

			if (frameworkElement != null)
			{
				this.ContentPanel.Children.Add(frameworkElement);
			}
		}
		#endregion Content




		public ComponentContentControl()
		{
			this.InitializeComponent();
			LinkEvents();
		}

		private void LinkEvents()
		{
			ClearButton.Click += ClearButton_Click;
			AddButton.Click += AddButton_Click;
		}

		void AddButton_Click(object sender, RoutedEventArgs e)
		{

		}

		void ClearButton_Click(object sender, RoutedEventArgs e)
		{
			this.Content = null;
		}
	}
}

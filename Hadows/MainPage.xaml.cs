using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Hadows.Component;
using Hadows.View.Window;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Hadows
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class MainPage : Page
	{
		public MainPage()
		{
			this.InitializeComponent();
			LinkEvents();
		}

		private void LinkEvents()
		{
			Windows002Button.Click += Windows002Button_Click;
			VideoPlayerButton.Click += VideoPlayerButton_Click;
			AudioPlayerButton.Click += AudioPlayerButton_Click;
			WebButton.Click += WebButton_Click;
		}

		void WebButton_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new WebBrowser();
		}

		void AudioPlayerButton_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new AudioPlayer();
		}

		void Windows002Button_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new Window002();
		}

		void VideoPlayerButton_Click(object sender, RoutedEventArgs e)
		{
			this.Content = new VideoPlayer();
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

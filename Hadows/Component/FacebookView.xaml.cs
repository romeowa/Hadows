using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using Facebook;
using Facebook.Client;
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

namespace Hadows.Component
{
	/// <summary>
	/// An empty page that can be used on its own or navigated to within a Frame.
	/// </summary>
	public sealed partial class FacebookView : Page
	{
		public FacebookView()
		{
			this.InitializeComponent();
			LinkEvents();
		}

		private void LinkEvents()
		{
			LoginButton.Click += LoginButton_Click;
		}

		async void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			string accessToken = string.Empty;
			string facebookId = string.Empty;
			FacebookSessionClient client = new FacebookSessionClient("158376391035007");
			FacebookSession session = await client.LoginAsync("user_about_me,read_stream");
			accessToken = session.AccessToken;
			facebookId = session.FacebookId;

			FacebookClient facebookClient = new FacebookClient(accessToken);

			dynamic parameter = new ExpandoObject();
			parameter.access_token = accessToken;
			parameter.fields = "name";

			dynamic result = await facebookClient.GetTaskAsync("me", parameter);
			LoginButton.Content = result.name;

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

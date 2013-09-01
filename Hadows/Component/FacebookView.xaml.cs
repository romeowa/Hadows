using System;
using System.Dynamic;
using Facebook;
using Facebook.Client;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
			//string accessToken = string.Empty;
			//string facebookId = string.Empty;
			//FacebookSessionClient client = new FacebookSessionClient("158376391035007");
			//FacebookSession session = await client.LoginAsync("user_about_me,read_stream, read_friendlists");
			//accessToken = session.AccessToken;
			//facebookId = session.FacebookId;

			//FacebookClient facebookClient = new FacebookClient(accessToken);

			//dynamic parameter = new ExpandoObject();
			//parameter.access_token = accessToken;

			//dynamic result = await facebookClient.GetTaskAsync("FriendList", parameter);
			//LoginButton.Content = result.list_type;

			LoginWebView.LoadCompleted += LoginWebView_LoadCompleted;
			LoginWebView.Source = new Uri(string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&scope={2}&response_type={3}", "158376391035007", "https://www.facebook.com/connect/login_success.html", "user_about_me,read_stream, read_friendlists", "token"));



		}

		void LoginWebView_LoadCompleted(object sender, NavigationEventArgs e)
		{
			if (LoginWebView.Source.AbsoluteUri.StartsWith("https://www.facebook.com/connect/login_success.html") == true)
			{
				string[] spliter = new string[] { "code=" };
				string code = LoginWebView.Source.AbsoluteUri.Split(spliter, StringSplitOptions.RemoveEmptyEntries)[0];

			}
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

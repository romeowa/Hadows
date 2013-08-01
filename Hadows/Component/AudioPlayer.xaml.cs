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

namespace Hadows.Component
{
	public sealed partial class AudioPlayer : UserControl, IComponent
	{
		public AudioPlayer()
		{
			this.InitializeComponent();
			ThumbnailName = "aa.jpg";
			DisplayName = "AudioPlayer";
		}

		public string ThumbnailName { get; set; }
		public string DisplayName { get; set; }


		public FrameworkElement GetInstance()
		{
			return new AudioPlayer();
		}
	}
}

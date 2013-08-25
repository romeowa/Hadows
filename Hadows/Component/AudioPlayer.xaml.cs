using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
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
	public class Song
	{
		public MusicProperties MusicProperties { get; set; }
		public StorageFile StorageFile { get; set; }
	}


	public sealed partial class AudioPlayer : UserControl, IComponent, IDisposable
	{
		//-------------------------- ▶ Properties
		public ObservableCollection<Song> Songs { get; set; }
		public Song CurrentSong { get; set; }


		//-------------------------- ▶ Members
		int _currentIndex;
		DispatcherTimer _updateProgressBarTimer;



		//-------------------------- ▶ Constructors
		public AudioPlayer()
		{
			InitializeComponent();
			InitIComponent();
			LinkEvents();


			_updateProgressBarTimer = new DispatcherTimer()
			{
				Interval = TimeSpan.FromMilliseconds(500)
			};

			_updateProgressBarTimer.Tick += _updateProgressBarTimer_Tick;



			Songs = new ObservableCollection<Song>();
			SongListBox.ItemsSource = Songs;
		}

		void _updateProgressBarTimer_Tick(object sender, object e)
		{
			if (musicMediaElement.CurrentState != MediaElementState.Playing)
			{
				_updateProgressBarTimer.Stop();
				return;
			}

			PlayProgressBar.Value = (musicMediaElement.Position.TotalSeconds / musicMediaElement.NaturalDuration.TimeSpan.TotalSeconds) * 100;
		}


		//-------------------------- ▶ Methods
		void InitIComponent()
		{
			ThumbnailName = "aa.jpg";
			DisplayName = "AudioPlayer";
			SnappedStateHeight = 700;
		}

		void LinkEvents()
		{
			OpenButton.Click += OpenButton_Click;
			PrevButton.Click += PrevButton_Click;
			PlayButton.Checked += PlayButton_Checked;
			PlayButton.Unchecked += PlayButton_Unchecked;
			ForwardButton.Click += ForwardButton_Click;
			VolumnProgressBar.ValueChanged += VolumnProgressBar_ValueChanged;
			PlayProgressBar.ValueChanged += PlayProgressBar_ValueChanged;
			SongListBox.SelectionChanged += SongListBox_SelectionChanged;

			musicMediaElement.CurrentStateChanged += musicMediaElement_CurrentStateChanged;

		}

		async void _PlayMusicAsync()
		{
			var stream = await CurrentSong.StorageFile.OpenAsync(FileAccessMode.Read);
			musicMediaElement.SetSource(stream, CurrentSong.StorageFile.ContentType);
			musicMediaElement.Play();
		}




		//-------------------------- ▶ EventHandlers
		async void OpenButton_Click(object sender, RoutedEventArgs e)
		{
			FolderPicker folderPicker = new FolderPicker()
			{
				ViewMode = PickerViewMode.Thumbnail,
				SuggestedStartLocation = PickerLocationId.MusicLibrary,
				CommitButtonText = "선택",
			};
			folderPicker.FileTypeFilter.Add(".mp3");
			folderPicker.FileTypeFilter.Add(".wmv");
			StorageFolder storageFolder = await folderPicker.PickSingleFolderAsync();

			if (storageFolder == null)
			{
				return;
			}

			IReadOnlyList<StorageFile> allItems = await storageFolder.GetFilesAsync();
			if (allItems.Count < 0)
			{
				return;
			}


			var onlyMusics = allItems.Where(c => c.ContentType == "audio/mpeg");
			if (onlyMusics.Count() <= 0)
			{
				return;
			}

			if (Songs == null)
			{
				Songs = new ObservableCollection<Song>();
			}
			else
			{
				Songs.Clear();
			}

			foreach (var music in onlyMusics)
			{
				Song song = new Song()
				{
					StorageFile = music,
					MusicProperties = await music.Properties.GetMusicPropertiesAsync()
				};

				Songs.Add(song);
			}
		}

		void ForwardButton_Click(object sender, RoutedEventArgs e)
		{
			++_currentIndex;
			if (_currentIndex > SongListBox.Items.Count - 1)
			{
				_currentIndex = 0;
			}

			SongListBox.SelectedIndex = _currentIndex;
		}

		void PrevButton_Click(object sender, RoutedEventArgs e)
		{
			--_currentIndex;
			if (_currentIndex < 0)
			{
				_currentIndex = SongListBox.Items.Count - 1;
			}

			SongListBox.SelectedIndex = _currentIndex;
		}

		void PlayButton_Checked(object sender, RoutedEventArgs e)
		{
			_PlayMusicAsync();
		}

		void PlayButton_Unchecked(object sender, RoutedEventArgs e)
		{
			musicMediaElement.Pause();
		}

		void PlayProgressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			musicMediaElement.Position = TimeSpan.FromSeconds((musicMediaElement.NaturalDuration.TimeSpan.TotalSeconds) * (e.NewValue / 100));
		}

		void musicMediaElement_CurrentStateChanged(object sender, RoutedEventArgs e)
		{
			if (musicMediaElement.CurrentState == MediaElementState.Playing)
			{
				_updateProgressBarTimer.Start();
			}
			else
			{
				_updateProgressBarTimer.Stop();
			}
		}

		void VolumnProgressBar_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
		{
			musicMediaElement.Volume = e.NewValue / 100;
		}

		void SongListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			ListBox senderListBox = sender as ListBox;
			if (senderListBox == null)
				return;

			if (e.AddedItems.Count <= 0)
			{
				return;
			}

			_currentIndex = senderListBox.SelectedIndex;
			CurrentSong = e.AddedItems[0] as Song;

			_PlayMusicAsync();
		}



		#region IComponent
		public string ThumbnailName { get; set; }
		public string DisplayName { get; set; }
		public double SnappedStateHeight { get; set; }
		public FrameworkElement GetInstance()
		{
			return new AudioPlayer();
		}
		#endregion IComponent

		public void Dispose()
		{
			if (_updateProgressBarTimer == null)
				return;

			_updateProgressBarTimer.Stop();
		}
	}
}



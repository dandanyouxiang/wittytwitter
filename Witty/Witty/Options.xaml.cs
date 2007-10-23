﻿using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace Witty
{
	public partial class Options
	{
        // Settings used by the application
        private Properties.Settings AppSettings = Properties.Settings.Default;

		public Options()
		{
			this.InitializeComponent();

            UsernameTextBox.Text = AppSettings.Username;
            PasswordTextBox.Password = AppSettings.Password;

            Binding binding = new Binding();
            binding.Path = new PropertyPath("Topmost");
            binding.Source = this;

            AlwaysOnTopCheckBox.SetBinding(CheckBox.IsCheckedProperty, binding);

            if (!string.IsNullOrEmpty(AppSettings.RefreshInterval))
                RefreshSlider.Value = Double.Parse(AppSettings.RefreshInterval);

            PlaySounds = AppSettings.PlaySounds;
		}

        public bool PlaySounds
        {
            get { return (bool)GetValue(PlaySoundsProperty); }
            set { SetValue(PlaySoundsProperty, value); }
        }

        public static readonly DependencyProperty PlaySoundsProperty =
            DependencyProperty.Register("PlaySounds", typeof(bool), typeof(Options),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnPlaySoundsChanged)));

        private static void OnPlaySoundsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            Properties.Settings.Default.PlaySounds = (bool)args.NewValue;
            Properties.Settings.Default.Save();
        }                

        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.RefreshInterval = SliderValueTextBlock.Text;
            AppSettings.Save();

            DialogResult = true;
            this.Close();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            AppSettings.Username = string.Empty;
            AppSettings.Password = string.Empty;
            AppSettings.LastUpdated = string.Empty;

            AppSettings.Save();

            DialogResult = false;
            this.Close();
        }
	}
}
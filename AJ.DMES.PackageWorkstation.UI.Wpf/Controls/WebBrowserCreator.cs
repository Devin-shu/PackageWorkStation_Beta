using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;

namespace AJ.DMES.PackageWorkstation.UI.Wpf.Controls
{
    public class WebBrowserCreator : Grid {

        #region Dependency Properties
        public static readonly DependencyProperty SourceProperty;
        public static readonly DependencyProperty ShowBrowserProperty;
        static WebBrowserCreator() {
            Type ownerType = typeof(WebBrowserCreator);
            SourceProperty = DependencyProperty.Register("Source", typeof(Uri), ownerType, new PropertyMetadata(null, RaiseSourceChanged));
            ShowBrowserProperty = DependencyProperty.Register("ShowBrowser", typeof(bool), ownerType, new PropertyMetadata(false, RaiseShowBrowserChanged));
        }
        static void RaiseSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((WebBrowserCreator)d).RaiseSourceChanged(e);
        }
        static void RaiseShowBrowserChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            ((WebBrowserCreator)d).RaiseShowBrowserChanged(e);
        }
        #endregion

        WebBrowser browser;

        public WebBrowserCreator() {
            Background = new SolidColorBrush(Colors.White);
        }
        public Uri Source { get { return (Uri)GetValue(SourceProperty); } set { SetValue(SourceProperty, value); } }
        public bool ShowBrowser { get { return (bool)GetValue(ShowBrowserProperty); } set { SetValue(ShowBrowserProperty, value); } }
        void DoShowBrowser() {
            this.browser = new WebBrowser();
            this.browser.Visibility = Visibility.Collapsed;
            Children.Add(this.browser);
            this.browser.LoadCompleted += OnBrowserLoadCompleted;
            UpdateBrowserSource();
        }
        void DoHideBrowser() {
            Children.Remove(this.browser);
            this.browser = null;
        }
        void OnBrowserLoadCompleted(object sender, NavigationEventArgs e) {
            if(this.browser == null) return;
            this.browser.LoadCompleted -= OnBrowserLoadCompleted;
            this.browser.Visibility = Visibility.Visible;
        }
        void RaiseSourceChanged(DependencyPropertyChangedEventArgs e) {
            UpdateBrowserSource();
        }
        void UpdateBrowserSource() {
            if(this.browser != null)
                this.browser.Source = Source;
        }
        void RaiseShowBrowserChanged(DependencyPropertyChangedEventArgs e) {
            bool newValue = (bool)e.NewValue;
            if(newValue)
                DoShowBrowser();
            else
                DoHideBrowser();
        }
    }
}

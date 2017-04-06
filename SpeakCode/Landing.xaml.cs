using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace SpeakCode
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Landing : Page
    {
        public Landing()
        {
            this.InitializeComponent();
            Playground.Navigate(typeof(UserPage));
        }
        private void ReturnHome(object sender, RoutedEventArgs e)
        {
            Playground.Navigate(typeof(UserPage));
        }
        private void OpenPlay(object sender, RoutedEventArgs e)
        {
            Playground.Navigate(typeof(Playground), "()");
        }
        private void SignOut(object sender, RoutedEventArgs e)
        {
            Playground.Navigate(typeof(LoginPage));
        }
    }
}
